using Microsoft.AspNetCore.Mvc;
using GuardianEyeAPI.Models;
using GuardianEyeAPI.Services;
using Drivers.Api.Controllers;

namespace GuardianEyeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificacionController : ControllerBase
    {
        private readonly ILogger<NotificacionController> _logger;

        private readonly NotificacionServices _notificacionServices;

        public NotificacionController(ILogger<NotificacionController> logger, NotificacionServices notificacionServices)
        {
            _logger = logger;
            _notificacionServices = notificacionServices;
        }

        ///Obtener todos los Usuarios/
        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            var notificacion = await _notificacionServices.GetAsync();
            return Ok(notificacion);
        }

        ///Obtener Usuario por Id
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetUsuariosById(string Id)
        {
            return Ok(await _notificacionServices.GetDriverById(Id));
        }

        ///Crear Usuario
        [HttpPost]
        public async Task<IActionResult> CreateUsuario([FromBody] NotificacionModel notificacion)
        {
            if (notificacion == null)
                return BadRequest();
            if (notificacion.Mensaje == string.Empty)
                ModelState.AddModelError("Name", "El usuario no debe estar vacio");

            await _notificacionServices.InsertDriver(notificacion);

            return Created("Created", true);
        }

        ///Actualizar Usuario
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateDriver([FromBody] NotificacionModel notificacion, string Id)
        {
            if (notificacion == null)
                return BadRequest();
            if (notificacion.Mensaje == string.Empty)
                ModelState.AddModelError("Name", "El usuario no debe estar vacio");
            notificacion.Id = Id;

            await _notificacionServices.UpdateDriver(notificacion);
            return Created("Created", true);
        }

        ///Eliminar Usuario
        [HttpDelete]
        public async Task<IActionResult> DeleteUsuario(string Id)
        {
            await _notificacionServices.DeleteDriver(Id);
            return NoContent();
        }
    }
}
