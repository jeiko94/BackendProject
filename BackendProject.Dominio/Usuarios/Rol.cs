namespace BackendProject.Dominio.Usuarios
{
    //Reol del sistema (Proveedor, Distribuidor, Vendedor)
    public class Rol
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }

        //Relacion con los usuarios que tienen este rol
        public List<UsuarioRol> UsuarioRoles { get; set; } = new List<UsuarioRol>();
    }
}
