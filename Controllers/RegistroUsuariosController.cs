using Microsoft.AspNetCore.Mvc;
using GuardianEyeAPI.Models;
using GuardianEyeAPI.Services;

namespace GuardianEyeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistroUsuariosController : ControllerBase
    {
        private readonly ILogger<RegistroUsuariosController> _logger;

        private readonly RegistroUsuariosServices _registroUsuariosServices;

        public RegistroUsuariosController(ILogger<RegistroUsuariosController> logger, RegistroUsuariosServices registroUsuariosServices)
        {
            _logger = logger;
            _registroUsuariosServices = registroUsuariosServices;
        }

        ///Obtener todos los Usuarios
        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            var Registro = await _registroUsuariosServices.GetAsync();
            return Ok(Registro);
        }

        ///Obtener Usuario por Id
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetUsuariosById(string Id)
        {
            return Ok(await _registroUsuariosServices.GetDriverById(Id));
        }

        ///Crear Usuario
        [HttpPost]
        public async Task<IActionResult> CreateUsuario([FromBody] RegistroUsuariosModel registroUsuarios)
        {
            if (registroUsuarios == null)
                return BadRequest();
            if (registroUsuarios.Correo == string.Empty)
                ModelState.AddModelError("Name", "El usuario no debe estar vacio");

            await _registroUsuariosServices.InsertDriver(registroUsuarios);

            return Created("Created", true);
        }

        ///Actualizar Usuario
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateDriver([FromBody] RegistroUsuariosModel registroUsuarios, string Id)
        {
            if (registroUsuarios == null)
                return BadRequest();
            if (registroUsuarios.Correo == string.Empty)
                ModelState.AddModelError("Name", "El usuario no debe estar vacio");
            registroUsuarios.Id = Id;

            await _registroUsuariosServices.UpdateDriver(registroUsuarios);
            return Created("Created", true);
        }

        ///Eliminar Usuario
        [HttpDelete]
        public async Task<IActionResult> DeleteUsuario(string Id)
        {
            await _registroUsuariosServices.DeleteDriver(Id);
            return NoContent();
        }
    }
}
