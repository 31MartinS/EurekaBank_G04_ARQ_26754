using Microsoft.EntityFrameworkCore;
using Eurabank_Restfull_SOAP_G04.Data;
using Eurabank_Restfull_SOAP_G04.ec.edu.monster.modelo;
using Eurabank_Restfull_SOAP_G04.ec.edu.monster.ws;

namespace Eurabank_Restfull_SOAP_G04.ec.edu.monster.service
{
    public class ClienteService : IClienteService
    {
        private readonly CalculatorDbContext _context;
        private readonly IContadorService _contadorService;

        public ClienteService(CalculatorDbContext context, IContadorService contadorService)
        {
            _context = context;
            _contadorService = contadorService;
        }

        public async Task<IEnumerable<ClienteDto>> ObtenerTodosLosClientesAsync()
        {
            var clientesDesdeDb = await _context.Clientes.ToListAsync();
            return clientesDesdeDb.Select(c => MapearAClienteDto(c));
        }

        public async Task<ClienteDto?> ObtenerClientePorIdAsync(string codigo)
        {
            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(c => c.ChrCliecodigo == codigo);

            return cliente != null ? MapearAClienteDto(cliente) : null;
        }

        public async Task<ClienteDto> CrearClienteAsync(CrearClienteDto dto)
        {
            var codigoCliente = await _contadorService.GenerarCodigoAsync("cliente");

            var cliente = new Cliente
            {
                ChrCliecodigo = codigoCliente,
                VchClienombre = dto.Nombre,
                VchCliepaterno = dto.Apellido ?? string.Empty,
                VchCliematerno = string.Empty,
                ChrCliedni = dto.Identificacion,
                VchClieciudad = string.Empty,
                VchCliedireccion = dto.Direccion ?? string.Empty,
                VchClietelefono = dto.Telefono,
                VchClieemail = dto.Email
            };

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return (await ObtenerClientePorIdAsync(cliente.ChrCliecodigo))!;
        }

        public async Task<bool> ActualizarClienteAsync(string codigo, ActualizarClienteDto dto)
        {
            var cliente = await _context.Clientes.FindAsync(codigo);
            if (cliente == null) return false;

            cliente.VchClienombre = dto.Nombre;
            cliente.VchCliepaterno = dto.Apellido ?? string.Empty;
            cliente.VchCliedireccion = dto.Direccion ?? string.Empty;
            cliente.VchClietelefono = dto.Telefono;
            cliente.VchClieemail = dto.Email;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarClienteAsync(string codigo)
        {
            var cliente = await _context.Clientes.FindAsync(codigo);
            if (cliente == null) return false;

            // Simplemente eliminar o marcar como inactivo si la tabla tuviera estado
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return true;
        }

        private ClienteDto MapearAClienteDto(Cliente c)
        {
            return new ClienteDto
            {
                Codigo = c.ChrCliecodigo,
                Nombre = c.VchClienombre,
                Apellido = c.VchCliepaterno + " " + c.VchCliematerno,
                Identificacion = c.ChrCliedni,
                Direccion = c.VchCliedireccion ?? string.Empty,
                Telefono = c.VchClietelefono ?? string.Empty,
                Email = c.VchClieemail ?? string.Empty,
                FechaRegistro = DateTime.Now,
                Estado = "ACTIVO"
            };
        }
    }
}