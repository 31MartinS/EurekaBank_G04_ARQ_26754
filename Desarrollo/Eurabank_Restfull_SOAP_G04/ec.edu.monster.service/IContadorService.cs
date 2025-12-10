namespace Eurabank_Restfull_SOAP_G04.ec.edu.monster.service
{
    public interface IContadorService
    {
        Task<string> GenerarCodigoAsync(string tipo);
    }
}
