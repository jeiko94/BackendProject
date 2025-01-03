using BackendProject.Dominio.Usuarios;

namespace BackendProject.Aplicacion.Usuarios.Interfaces
{
    public interface IRolRepositorio
    {
        Task<Rol> ObtenerPorNombreAsync(string nombre);
        // Podrías tener un CrearRolAsync si deseas administrar roles dinámicamente
    }
}
