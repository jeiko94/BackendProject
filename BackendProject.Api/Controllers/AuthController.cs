using BackendProject.Api.DTOs.Usuarios;
using BackendProject.Aplicacion.Usuarios.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace BackendProject.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthServices _authServices;

        public AuthController(AuthServices authServices)
        {
            _authServices = authServices;
        }

        //Registro de usuario signUp con roles
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpDto dto)
        {
            try
            {
                var roles = dto.Roles; //Proveedor, Distribuidor Vendedor
                var userId = await _authServices.SignUpAsync(dto.Nombre,dto.Email, dto.Password, roles);
                return Ok($"Usuario creado con Id = {userId}");
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        //Inicio de sesion
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInDto dto)
        {
            var usuario = await _authServices.SignInAsync(dto.Email, dto.Password);
            if(usuario == null)
                return Unauthorized("Email o contraseña incorrectos");

            // P.ej. retornar info básica, o un token JWT
            // Por simplicidad devolvemos roles y un pseudo token
            var roles = usuario.UsuarioRoles.Select(ur => ur.Rol.Nombre).ToList();
            return Ok(new
            {
                UsuarioId = usuario.Id,
                Nombre = usuario.Nombre,
                Roles = roles
                // Falta un token real si queremos JWT
            });
        }
    }
}
