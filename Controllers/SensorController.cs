using Microsoft.AspNetCore.Mvc;
using GuardianEyeAPI.Models;
using GuardianEyeAPI.Services;
using Drivers.Api.Controllers;

namespace GuardianEyeAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SensorController : Controller
{
    private readonly ILogger<SensorController> _logger;

    private readonly SensorServices _sensorServices;

    public SensorController(ILogger<SensorController> logger, SensorServices sensorServices)
    {
        _logger = logger;
        _sensorServices = sensorServices;
    }

    ///Obtener todos los Usuarios
    [HttpGet]
    public async Task<IActionResult> GetUsuarios()
    {
        var usuarios = await _sensorServices.GetAsync();
        return Ok(usuarios);
    }

    ///Obtener Usuario por Id
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetUsuariosById(string Id)
    {
        return Ok(await _sensorServices.GetDriverById(Id));
    }

    ///Crear Usuario
    [HttpPost]
    public async Task<IActionResult> CreateUsuario([FromBody] SensorModel sensor)
    {
        if (sensor == null)
            return BadRequest();
        if (sensor.Modelo == string.Empty)
            ModelState.AddModelError("Name", "El usuario no debe estar vacio");

        await _sensorServices.InsertDriver(sensor);

        return Created("Created", true);
    }

    ///Actualizar Usuario
    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateDriver([FromBody] SensorModel sensor, string Id)
    {
        if (sensor == null)
            return BadRequest();
        if (sensor.Modelo == string.Empty)
            ModelState.AddModelError("Name", "El usuario no debe estar vacio");
        sensor.Id = Id;

        await _sensorServices.UpdateDriver(sensor);
        return Created("Created", true);
    }

    ///Eliminar Usuario
    [HttpDelete]
    public async Task<IActionResult> DeleteUsuario(string Id)
    {
        await _sensorServices.DeleteDriver(Id);
        return NoContent();
    }
}
