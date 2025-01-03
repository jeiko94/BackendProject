namespace BackendProject.Dominio.Usuarios
{
    //Tabla intermedio para la relacion muchos a muchos enre usuarios y roles
    public class UsuarioRol
    {
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        public int RolId { get; set; }
        public Rol? Rol { get; set; }
    }

    //Con estas tres clases, representamos la relación muchos-a-muchos:
    //un Usuario puede tener múltiples roles, y un Rol puede pertenecer a múltiples usuarios.
}
