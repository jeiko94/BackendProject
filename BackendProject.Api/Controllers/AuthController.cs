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
                //var roles = dto.Roles; //Proveedor, Distribuidor Vendedor
                var userId = await _authServices.SignUpAsync(dto.Nombre,dto.Email, dto.Password, dto.Roles);
                
                //Retornar el JSON con exito y mensaje
                return Ok(new
                {
                    exito = true,
                    mensaje = "Usuario registrado con éxito",
                });
            }
            catch (InvalidOperationException ex)
            {
                //Ejmplo: email repetido. Devolvemos un JSON indicando el error
                return Conflict(new
                {
                    exito = false,
                    mensaje = ex.Message
                });
            }
            catch (Exception ex)
            {
                // Manejo de excepciones generales si deseas
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    exito = false,
                    mensaje = ex.Message
                });
            }
        }

        //Inicio de sesion
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInDto dto)
        {
            var usuario = await _authServices.SignInAsync(dto.Email, dto.Password);
            if (usuario == null)
            {
                // Credenciales inválidas
                return BadRequest(new
                {
                    exito = false,
                    mensaje = "Credenciales inválidas."
                });
            }

            // Si es exitoso
            // Ejemplo: "Bienvenido, Carmen!"
            return Ok(new
            {
                exito = true,
                mensaje = $"Bienvenido, {usuario.Nombre}!",
                usuarioId = usuario.Id
            });
        }

    }
}
