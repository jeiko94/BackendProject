namespace BackendProject.Dominio.Usuarios
{
    //Representa un usuario en el sistema
    public class Usuario
    {
        public int Id { get; set; }

        public string? Nombre { get; set; }

        public string? Email { get; set; }
        
        public string? PasswordHash { get; set; }

        public bool Activo { get; set; } = true;

        //Relacion con los roles del usuario (muchos a muchos)
        public List<UsuarioRol> UsuarioRoles { get; set; } = new List<UsuarioRol>();
    }
}
