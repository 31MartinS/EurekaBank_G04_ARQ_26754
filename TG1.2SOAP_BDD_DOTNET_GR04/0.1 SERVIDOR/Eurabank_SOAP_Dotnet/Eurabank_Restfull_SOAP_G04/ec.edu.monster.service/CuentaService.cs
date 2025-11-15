using Microsoft.EntityFrameworkCore;
using Eurabank_Restfull_SOAP_G04.Data;
using Eurabank_Restfull_SOAP_G04.ec.edu.monster.modelo;
using Eurabank_Restfull_SOAP_G04.ec.edu.monster.ws;

namespace Eurabank_Restfull_SOAP_G04.ec.edu.monster.service
{
    public class CuentaService : ICuentaService
    {
        private readonly CalculatorDbContext _context;
        private readonly IContadorService _contadorService;

        public CuentaService(CalculatorDbContext context, IContadorService contadorService)
        {
            _context = context;
            _contadorService = contadorService;
        }

        public async Task<IEnumerable<CuentaDto>> ObtenerTodasLasCuentasAsync()
        {
            var cuentas = await _context.Cuentas
                .Include(c => c.Modena)
                .Include(c => c.Sucursal)
                .Include(c => c.Cliente)
                .Include(c => c.EmpleadoCreador)
                .ToListAsync();

            return cuentas.Select(c => MapearACuentaDto(c));
        }

        public async Task<CuentaDto?> ObtenerCuentaPorCodigoAsync(string codigo)
        {
            var cuenta = await _context.Cuentas
                .Include(c => c.Modena)
                .Include(c => c.Sucursal)
                .Include(c => c.Cliente)
                .Include(c => c.EmpleadoCreador)
                .FirstOrDefaultAsync(c => c.ChrCuencodigo == codigo);

            return cuenta != null ? MapearACuentaDto(cuenta) : null;
        }

        public async Task<CuentaDto> CrearCuentaAsync(CrearCuentaDto dto)
        {
            var codigoCuenta = await _contadorService.GenerarCodigoAsync("cuenta");

            var cuenta = new Cuenta
            {
                ChrCuencodigo = codigoCuenta,
                ChrMonecodigo = dto.CodigoMoneda,
                ChrSucucodigo = dto.CodigoSucursal,
                ChrEmplcreacuenta = dto.CodigoEmpleado,
                ChrCliecodigo = dto.CodigoCliente,
                DecCuensaldo = 0,
                DttCuenfechacreacion = DateTime.Now,
                VchCuenestado = "ACTIVO",
                IntCuencontmov = 0,
                ChrCuenclave = "000000"
            };

            _context.Cuentas.Add(cuenta);
            await _context.SaveChangesAsync();

            return (await ObtenerCuentaPorCodigoAsync(cuenta.ChrCuencodigo))!;
        }

        public async Task<bool> ActualizarEstadoCuentaAsync(string codigo, string estado)
        {
            var cuenta = await _context.Cuentas.FindAsync(codigo);
            if (cuenta == null) return false;

            cuenta.VchCuenestado = estado;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<decimal> ConsultarSaldoAsync(string codigo)
        {
            var cuenta = await _context.Cuentas.FindAsync(codigo);
            return cuenta?.DecCuensaldo ?? 0;
        }

        private CuentaDto MapearACuentaDto(Cuenta c)
        {
            return new CuentaDto
            {
                Codigo = c.ChrCuencodigo,
                CodigoMoneda = c.ChrMonecodigo,
                NombreMoneda = c.Modena?.VchMonedescripcion ?? string.Empty,
                CodigoSucursal = c.ChrSucucodigo,
                NombreSucursal = c.Sucursal?.VchSucunombre ?? string.Empty,
                CodigoCliente = c.ChrCliecodigo,
                NombreCliente = c.Cliente?.VchClienombre ?? string.Empty,
                Saldo = c.DecCuensaldo,
                FechaCreacion = c.DttCuenfechacreacion,
                Estado = c.VchCuenestado
            };
        }
    }
}
