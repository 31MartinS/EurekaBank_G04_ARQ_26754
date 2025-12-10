using Eurabank_Restfull_SOAP_G04.ec.edu.monster.ws;

namespace Eurabank_Restfull_SOAP_G04.ec.edu.monster.service
{
    public interface ISucursalService
    {
        Task<IEnumerable<SucursalDto>> ObtenerTodasLasSucursalesAsync();
        Task<SucursalDto?> ObtenerSucursalPorCodigoAsync(string codigo);
    }
}
