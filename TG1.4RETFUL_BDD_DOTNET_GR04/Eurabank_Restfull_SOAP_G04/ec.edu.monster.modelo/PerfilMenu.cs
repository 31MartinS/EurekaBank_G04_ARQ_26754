namespace Eurabank_Restfull_SOAP_G04.ec.edu.monster.modelo
{
    // Tabla intermedia: Perfil - Menu
    public class PerfilMenu
    {
        public int IdPerfilmenu { get; set; }
        public int IdPerfil { get; set; }
        public int IdMenu { get; set; }
        public bool PmLectura { get; set; } = true;
        public bool PmEscritura { get; set; } = false;
        public bool PmEliminacion { get; set; } = false;
        
        // Relaciones
        public Perfil? Perfil { get; set; }
        public Menu? Menu { get; set; }
    }
}
