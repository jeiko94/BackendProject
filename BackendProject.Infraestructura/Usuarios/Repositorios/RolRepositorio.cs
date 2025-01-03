using BackendProject.Aplicacion.Usuarios.Interfaces;
using BackendProject.Dominio.Usuarios;
using BackendProject.Infraestructura.Usuarios.Data;
using Microsoft.EntityFrameworkCore;

namespace BackendProject.Infraestructura.Usuarios.Repositorios
{
    public class RolRepositorio : IRolRepositorio
    {
        private readonly BackendDbContext _context;

        public RolRepositorio(BackendDbContext context)
        {
            _context = context;
        }

        public async Task<Rol?> ObtenerPorNombreAsync(string nombre)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(r => r.Nombre == nombre);
        }
    }
}
