using Microsoft.EntityFrameworkCore;
using Eurabank_Restfull_SOAP_G04.Data;
using Eurabank_Restfull_SOAP_G04.ec.edu.monster.modelo;
using Eurabank_Restfull_SOAP_G04.ec.edu.monster.ws;

namespace Eurabank_Restfull_SOAP_G04.ec.edu.monster.service
{
    public class MovimientoService : IMovimientoService
    {
        private readonly CalculatorDbContext _context;

        public MovimientoService(CalculatorDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MovimientoDto>> ObtenerMovimientosPorCuentaAsync(string codigoCuenta)
        {
            var movimientos = await _context.Movimientos
                .Include(m => m.TipoMovimiento)
                .Include(m => m.Empleado)
                .Where(m => m.ChrCuencodigo == codigoCuenta)
                .OrderByDescending(m => m.DttMovifecha)
                .ToListAsync();

            return movimientos.Select(m => MapearAMovimientoDto(m));
        }

        public async Task<MovimientoDto> RealizarDepositoAsync(RealizarMovimientoDto dto)
        {
            var cuenta = await _context.Cuentas.FindAsync(dto.CodigoCuenta);
            if (cuenta == null)
                throw new Exception("Cuenta no encontrada");

            if (cuenta.VchCuenestado != "ACTIVO")
                throw new Exception("La cuenta no está activa");

            // Obtener siguiente número de movimiento
            var ultimoMovimiento = await _context.Movimientos
                .Where(m => m.ChrCuencodigo == dto.CodigoCuenta)
                .OrderByDescending(m => m.IntMovinumero)
                .FirstOrDefaultAsync();

            int siguienteNumero = (ultimoMovimiento?.IntMovinumero ?? 0) + 1;

            var movimiento = new Movimiento
            {
                ChrCuencodigo = dto.CodigoCuenta,
                IntMovinumero = siguienteNumero,
                DttMovifecha = DateTime.Now,
                ChrEmplcodigo = dto.CodigoEmpleado,
                ChrTipocodigo = dto.CodigoTipo,
                DecMoviimporte = dto.Importe,
                ChrCuenreferencia = dto.CuentaReferencia
            };

            // Actualizar saldo
            cuenta.DecCuensaldo += dto.Importe;

            _context.Movimientos.Add(movimiento);
            await _context.SaveChangesAsync();

            return MapearAMovimientoDto(movimiento);
        }

        public async Task<MovimientoDto> RealizarRetiroAsync(RealizarMovimientoDto dto)
        {
            var cuenta = await _context.Cuentas.FindAsync(dto.CodigoCuenta);
            if (cuenta == null)
                throw new Exception("Cuenta no encontrada");

            if (cuenta.VchCuenestado != "ACTIVO")
                throw new Exception("La cuenta no está activa");

            if (cuenta.DecCuensaldo < dto.Importe)
                throw new Exception("Saldo insuficiente");

            var ultimoMovimiento = await _context.Movimientos
                .Where(m => m.ChrCuencodigo == dto.CodigoCuenta)
                .OrderByDescending(m => m.IntMovinumero)
                .FirstOrDefaultAsync();

            int siguienteNumero = (ultimoMovimiento?.IntMovinumero ?? 0) + 1;

            var movimiento = new Movimiento
            {
                ChrCuencodigo = dto.CodigoCuenta,
                IntMovinumero = siguienteNumero,
                DttMovifecha = DateTime.Now,
                ChrEmplcodigo = dto.CodigoEmpleado,
                ChrTipocodigo = dto.CodigoTipo,
                DecMoviimporte = dto.Importe,
                ChrCuenreferencia = dto.CuentaReferencia
            };

            // Actualizar saldo
            cuenta.DecCuensaldo -= dto.Importe;

            _context.Movimientos.Add(movimiento);
            await _context.SaveChangesAsync();

            return MapearAMovimientoDto(movimiento);
        }

        public async Task<MovimientoDto> RealizarTransferenciaAsync(RealizarTransferenciaDto dto)
        {
            // Log para debugging
            Console.WriteLine($"[TRANSFERENCIA] Origen: {dto.CodigoCuentaOrigen}, Destino: {dto.CodigoCuentaDestino}, Importe: {dto.Importe}");
            
            var cuentaOrigen = await _context.Cuentas.FindAsync(dto.CodigoCuentaOrigen);
            var cuentaDestino = await _context.Cuentas.FindAsync(dto.CodigoCuentaDestino);

            if (cuentaOrigen == null)
                throw new Exception($"Cuenta origen '{dto.CodigoCuentaOrigen}' no encontrada");
            
            if (cuentaDestino == null)
                throw new Exception($"Cuenta destino '{dto.CodigoCuentaDestino}' no encontrada");

            if (cuentaOrigen.VchCuenestado != "ACTIVO")
                throw new Exception($"La cuenta origen está {cuentaOrigen.VchCuenestado}, debe estar ACTIVA");
            
            if (cuentaDestino.VchCuenestado != "ACTIVO")
                throw new Exception($"La cuenta destino está {cuentaDestino.VchCuenestado}, debe estar ACTIVA");

            if (cuentaOrigen.DecCuensaldo < dto.Importe)
                throw new Exception($"Saldo insuficiente en cuenta origen. Saldo: {cuentaOrigen.DecCuensaldo:C}, Requerido: {dto.Importe:C}");

            // Movimiento de salida (cuenta origen)
            var ultimoMovOrigen = await _context.Movimientos
                .Where(m => m.ChrCuencodigo == dto.CodigoCuentaOrigen)
                .OrderByDescending(m => m.IntMovinumero)
                .FirstOrDefaultAsync();

            int siguienteNumOrigen = (ultimoMovOrigen?.IntMovinumero ?? 0) + 1;

            var movimientoSalida = new Movimiento
            {
                ChrCuencodigo = dto.CodigoCuentaOrigen,
                IntMovinumero = siguienteNumOrigen,
                DttMovifecha = DateTime.Now,
                ChrEmplcodigo = dto.CodigoEmpleado,
                ChrTipocodigo = dto.CodigoTipo,
                DecMoviimporte = dto.Importe,
                ChrCuenreferencia = dto.CodigoCuentaDestino
            };

            cuentaOrigen.DecCuensaldo -= dto.Importe;
            _context.Movimientos.Add(movimientoSalida);

            // Movimiento de entrada (cuenta destino)
            var ultimoMovDestino = await _context.Movimientos
                .Where(m => m.ChrCuencodigo == dto.CodigoCuentaDestino)
                .OrderByDescending(m => m.IntMovinumero)
                .FirstOrDefaultAsync();

            int siguienteNumDestino = (ultimoMovDestino?.IntMovinumero ?? 0) + 1;

            var movimientoEntrada = new Movimiento
            {
                ChrCuencodigo = dto.CodigoCuentaDestino,
                IntMovinumero = siguienteNumDestino,
                DttMovifecha = DateTime.Now,
                ChrEmplcodigo = dto.CodigoEmpleado,
                ChrTipocodigo = "008", // Código de transferencia ingreso
                DecMoviimporte = dto.Importe,
                ChrCuenreferencia = dto.CodigoCuentaOrigen
            };

            cuentaDestino.DecCuensaldo += dto.Importe;
            _context.Movimientos.Add(movimientoEntrada);

            await _context.SaveChangesAsync();

            return MapearAMovimientoDto(movimientoSalida);
        }

        private MovimientoDto MapearAMovimientoDto(Movimiento m)
        {
            return new MovimientoDto
            {
                CodigoCuenta = m.ChrCuencodigo,
                NumeroMovimiento = m.IntMovinumero,
                Fecha = m.DttMovifecha,
                CodigoTipo = m.ChrTipocodigo,
                NombreTipo = m.TipoMovimiento?.VchTipodescripcion ?? string.Empty,
                Importe = m.DecMoviimporte,
                CuentaReferencia = m.ChrCuenreferencia
            };
        }
    }
}
