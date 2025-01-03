using BackendProject.Dominio.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace BackendProject.Infraestructura.Usuarios.Data
{
    //Representa un usuario en el sistema, que puede tener multiples roles
    public class BackendDbContext : DbContext
    {
        public BackendDbContext(DbContextOptions<BackendDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<UsuarioRol> UsuarioRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //UsuarioRol como primary key compuesta
            modelBuilder.Entity<UsuarioRol>()
                .HasKey(ur => new { ur.UsuarioId, ur.RolId });

            //Relacion muchos a muchos
            modelBuilder.Entity<UsuarioRol>()
                .HasOne(ur => ur.Usuario)
                .WithMany(u => u.UsuarioRoles)
                .HasForeignKey(ur => ur.UsuarioId);

            modelBuilder.Entity<UsuarioRol>()
                .HasOne(ur => ur.Rol)
                .WithMany(r => r.UsuarioRoles)
                .HasForeignKey(ur => ur.RolId);

            //Indice unico en email
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique(true);
        }
    }
}
