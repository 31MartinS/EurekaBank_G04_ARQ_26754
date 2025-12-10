namespace Eurabank_Restfull_SOAP_G04.ec.edu.monster.modelo
{
    public class Perfil
    {
        public int IdPerfil { get; set; }
        public string PerNombre { get; set; } = string.Empty;
        public string PerDescripcion { get; set; } = string.Empty;
        public string PerEstado { get; set; } = "ACTIVO";
        
        // Relaciones
        public ICollection<PerfilMenu>? PerfilesMenus { get; set; }
        public ICollection<UsuarioPerfilMenu>? UsuariosPerfilesMenus { get; set; }
    }
}
