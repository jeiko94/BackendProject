using BackendProject.Aplicacion.Usuarios.Interfaces;
using BackendProject.Dominio.Usuarios;
using System.Security.Cryptography;
using System.Text;

namespace BackendProject.Aplicacion.Usuarios.Servicios
{
    public class AuthServices
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IRolRepositorio _rolRepositorio;

        public AuthServices(IUsuarioRepositorio usuarioRepositorio, IRolRepositorio rolRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _rolRepositorio = rolRepositorio;
        }

        //Registrar un nuevo usuario con los roles especificados
        public async Task<int> SignUpAsync(string nombre, string email, string passwordClaro, List<string> nombresRoles)
        {
            //1.Validar que el email no exista en la base de datos
            var existe = await _usuarioRepositorio.ObtenerPorEmailAsync(email);

            if(existe != null)
                throw new InvalidOperationException("El email ya está registrado");

            //2. Hashear la contraseña
            string passwordHash = HashedPassword(passwordClaro);

            //3. Crear usuario
            var usuario = new Usuario
            {
                Nombre = nombre,
                Email = email,
                PasswordHash = passwordHash,
                Activo = true
            };

            //4. Asignar roles
            var rolesValidos = new HashSet<string> { "Proveedor", "Distribuidor", "Vendedor" };

            foreach (var nomRol in nombresRoles)
            {
                if (!rolesValidos.Contains(nomRol))
                    throw new InvalidOperationException($"El rol '{nomRol}' no está permitido.");

                var rol = await _rolRepositorio.ObtenerPorNombreAsync(nomRol);
                if (rol == null)
                    throw new InvalidOperationException($"El rol '{nomRol}' no existe en la BD.");

                usuario.UsuarioRoles.Add(new UsuarioRol
                {
                    Usuario = usuario,
                    Rol = rol
                });
            }

            //5. Guardar usuario en BD
            await _usuarioRepositorio.CrearAsync(usuario);

            return usuario.Id;
        }

        //Autenticas usuario, retorna el Usuario si la contraseña es correcta
        public async Task<Usuario> SignInAsync(string email, string passwordClaro)
        {
            var usuario = await _usuarioRepositorio.ObtenerPorEmailAsync(email);

            if (usuario == null)
                return null; //Validar ya que no es bueno que se retorne un null

            var hashIngresado = HashedPassword(passwordClaro); //== usuario.PasswordHash;

            if (hashIngresado == usuario.PasswordHash)
            {
                //Retornamos el usuario con sus roles
                return usuario;
            }

            return null; //Validar ya que no es bueno que se retorne un null
        }

        private string HashedPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));            
            return Convert.ToBase64String(bytes);
        }
    }
}
