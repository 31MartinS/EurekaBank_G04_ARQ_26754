namespace Eurabank_Restfull_SOAP_G04.ec.edu.monster.modelo
{
    public class Telefono
    {
        public int IdTelefono { get; set; }
        public int IdCliente { get; set; }
        public string TelNumero { get; set; } = string.Empty;
        public string TelTipo { get; set; } = string.Empty; // MOVIL, FIJO, TRABAJO
        
        // Relaciones
        public Cliente? Cliente { get; set; }
    }
}
