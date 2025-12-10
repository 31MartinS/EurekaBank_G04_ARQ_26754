using Eurabank_Restfull_SOAP_G04.ec.edu.monster.ws;

namespace Eurabank_Restfull_SOAP_G04.ec.edu.monster.service
{
    public interface IMovimientoService
    {
        Task<IEnumerable<MovimientoDto>> ObtenerMovimientosPorCuentaAsync(string codigoCuenta);
        Task<MovimientoDto> RealizarDepositoAsync(RealizarMovimientoDto dto);
        Task<MovimientoDto> RealizarRetiroAsync(RealizarMovimientoDto dto);
        Task<MovimientoDto> RealizarTransferenciaAsync(RealizarTransferenciaDto dto);
    }
}
