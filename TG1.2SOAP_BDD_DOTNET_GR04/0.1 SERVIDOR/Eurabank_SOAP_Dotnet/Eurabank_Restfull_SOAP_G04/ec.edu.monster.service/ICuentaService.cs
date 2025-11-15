using Eurabank_Restfull_SOAP_G04.ec.edu.monster.ws;

namespace Eurabank_Restfull_SOAP_G04.ec.edu.monster.service
{
    public interface ICuentaService
    {
        Task<IEnumerable<CuentaDto>> ObtenerTodasLasCuentasAsync();
        Task<CuentaDto?> ObtenerCuentaPorCodigoAsync(string codigo);
        Task<CuentaDto> CrearCuentaAsync(CrearCuentaDto dto);
        Task<bool> ActualizarEstadoCuentaAsync(string codigo, string estado);
        Task<decimal> ConsultarSaldoAsync(string codigo);
    }
}
