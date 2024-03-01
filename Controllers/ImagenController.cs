using Microsoft.AspNetCore.Mvc;
using GuardianEyeAPI.Models;
using GuardianEyeAPI.Services;
using static System.Net.Mime.MediaTypeNames;

namespace GuardianEyeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagenController : ControllerBase
    {
        private readonly ILogger<ImagenController> _logger;

        private readonly ImagenServices _imagenServices;

        public ImagenController(ILogger<ImagenController> logger, ImagenServices imagenServices)
        {
            _logger = logger;
            _imagenServices = imagenServices;
        }

        ///Obtener todos los Usuarios
        [HttpGet]
        public async Task<IActionResult> GetImagen()
        {
            var imagen = await _imagenServices.GetAsync();
            return Ok(imagen);
        }

        ///Obtener Usuario por Id
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetImagenById(string Id)
        {
            return Ok(await _imagenServices.GetImagenById(Id));
        }

        ///Crear Usuario
        [HttpPost]
        public async Task<IActionResult> CreateImagen([FromBody] ImagenModel imagen)
        {
            if (imagen == null)
                return BadRequest();
            if (imagen.Camara == string.Empty)
                ModelState.AddModelError("Ubicacion", "El usuario no debe estar vacio");

            await _imagenServices.InsertImagen(imagen);

            return Created("Created", true);
        }

        ///Actualizar Usuario
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateImagen([FromBody] ImagenModel imagen, string Id)
        {
            if (imagen == null)
                return BadRequest();
            if (imagen.Camara == string.Empty)
                ModelState.AddModelError("Name", "El usuario no debe estar vacio");
            imagen.Id = Id;

            await _imagenServices.UpdateImagen(imagen);
            return Created("Created", true);
        }

        ///Eliminar Usuario
        [HttpDelete]
        public async Task<IActionResult> DeleteImagen(string Id)
        {
            await _imagenServices.DeleteImagen(Id);
            return NoContent();
        }
    }
}
