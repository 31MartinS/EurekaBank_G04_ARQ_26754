namespace Eurabank_Restfull_SOAP_G04.ec.edu.monster.modelo
{
    // Tabla intermedia: Usuario - Perfil - Menu
    public class UsuarioPerfilMenu
    {
        public int IdUsuarioperfilmenu { get; set; }
        public int IdUsuario { get; set; }
        public int IdPerfil { get; set; }
        public int IdMenu { get; set; }
        public DateTime UpmFechaAsignacion { get; set; } = DateTime.Now;
        
        // Relaciones
        public Usuario? Usuario { get; set; }
        public Perfil? Perfil { get; set; }
        public Menu? Menu { get; set; }
    }
}
