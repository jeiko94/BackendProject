using BackendProject.Aplicacion.Usuarios.Interfaces;
using BackendProject.Dominio.Usuarios;
using BackendProject.Infraestructura.Usuarios.Data;
using Microsoft.EntityFrameworkCore;

namespace BackendProject.Infraestructura.Usuarios.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BackendDbContext _context;

        public UsuarioRepositorio(BackendDbContext context)
        {
            _context = context;
        }

        public async Task CrearAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<Usuario?> ObtenerPorEmailAsync(string email)
        {
            return await _context.Usuarios
                .Include(u => u.UsuarioRoles)
                .ThenInclude(ur => ur.Rol)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Usuario?> ObtenerPorIdAsync(int id)
        {
            return await _context.Usuarios
                .Include(u => u.UsuarioRoles)
                .ThenInclude(ur => ur.Rol)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task ActualizarAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var usuario = await ObtenerPorIdAsync(id);
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }
    }
}
