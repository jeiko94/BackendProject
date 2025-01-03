using BackendProject.Dominio.Usuarios;

namespace BackendProject.Aplicacion.Usuarios.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Task CrearAsync(Usuario usuario);
        Task<Usuario> ObtenerPorEmailAsync(string email);
        Task<Usuario> ObtenerPorIdAsync(int id);
        Task ActualizarAsync(Usuario usuario);
        Task EliminarAsync(int id);
    }
}
