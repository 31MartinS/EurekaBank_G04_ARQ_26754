using Microsoft.EntityFrameworkCore;
using Eurabank_Restfull_SOAP_G04.Data;
using Eurabank_Restfull_SOAP_G04.ec.edu.monster.modelo;
using Eurabank_Restfull_SOAP_G04.ec.edu.monster.ws;

namespace Eurabank_Restfull_SOAP_G04.ec.edu.monster.service
{
    public class SucursalService : ISucursalService
    {
        private readonly CalculatorDbContext _context;

        public SucursalService(CalculatorDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SucursalDto>> ObtenerTodasLasSucursalesAsync()
        {
            var sucursales = await _context.Sucursales.ToListAsync();
            return sucursales.Select(s => MapearASucursalDto(s));
        }

        public async Task<SucursalDto?> ObtenerSucursalPorCodigoAsync(string codigo)
        {
            var sucursal = await _context.Sucursales.FindAsync(codigo);
            return sucursal != null ? MapearASucursalDto(sucursal) : null;
        }

        private SucursalDto MapearASucursalDto(Sucursal s)
        {
            return new SucursalDto
            {
                Codigo = s.ChrSucucodigo,
                Nombre = s.VchSucunombre,
                Direccion = s.VchSucudireccion ?? string.Empty,
                Telefono = string.Empty,
                Estado = "ACTIVO"
            };
        }
    }
}
