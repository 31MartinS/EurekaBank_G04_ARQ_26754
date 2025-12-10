using Eurabank_Restfull_SOAP_G04.ec.edu.monster.ws; 

namespace Eurabank_Restfull_SOAP_G04.ec.edu.monster.service
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteDto>> ObtenerTodosLosClientesAsync();
        Task<ClienteDto?> ObtenerClientePorIdAsync(string codigo);
        Task<ClienteDto> CrearClienteAsync(CrearClienteDto dto);
        Task<bool> ActualizarClienteAsync(string codigo, ActualizarClienteDto dto);
        Task<bool> EliminarClienteAsync(string codigo);
    }
}