namespace Eurabank_Restfull_SOAP_G04.ec.edu.monster.modelo
{
    public class Menu
    {
        public int IdMenu { get; set; }
        public string MenuNombre { get; set; } = string.Empty;
        public string MenuUrl { get; set; } = string.Empty;
        public string MenuIcono { get; set; } = string.Empty;
        public int? MenuPadreId { get; set; }
        public int MenuOrden { get; set; }
        
        // Relaciones
        public ICollection<PerfilMenu>? PerfilesMenus { get; set; }
        public ICollection<UsuarioPerfilMenu>? UsuariosPerfilesMenus { get; set; }
    }
}
