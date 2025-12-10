namespace Eurabank_Restfull_SOAP_G04.ec.edu.monster.ws
{
    public class ClienteDto
    {
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Identificacion { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }
        public string Estado { get; set; } = string.Empty;
    }

    public class CrearClienteDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string? Apellido { get; set; }
        public string Identificacion { get; set; } = string.Empty;
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
    }

    public class ActualizarClienteDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string? Apellido { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string Estado { get; set; } = "ACTIVO";
    }
}
