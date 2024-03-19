using Microsoft.AspNetCore.Mvc;
using GuardianEyeAPI.Models;
using GuardianEyeAPI.Services;


namespace GuardianEyeAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CamaraController : ControllerBase
{
    private readonly ILogger<CamaraController> _logger;

    private readonly CamaraServices _camaraServices;

    public CamaraController(ILogger<CamaraController> logger, CamaraServices camaraServices)
    {
        _logger = logger;
        _camaraServices = camaraServices;
    }

    ///Obtener todos los Usuarios
    [HttpGet]
    public async Task<IActionResult> GetCamara()
    {
        var camara = await _camaraServices.GetAsync();
        return Ok(camara);
    }

    ///Obtener Usuario por Id
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetCamaraById(string Id)
    {
        return Ok(await _camaraServices.GetCamaraById(Id));
    }

    ///Crear Usuario
    [HttpPost]
    public async Task<IActionResult> CreateCamara([FromBody] CamaraModel camara)
    {
        if (camara == null)
            return BadRequest();
        if (camara.Ubicacion == string.Empty)
            ModelState.AddModelError("Ubicacion", "La Camara no debe estar vacio");

        await _camaraServices.InsertCamara(camara);

        return Created("Created", true);
    }

    ///Actualizar Usuario
    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateCamara([FromBody] CamaraModel camara, string Id) 
    {
        CamaraModel camaraModel = new CamaraModel()
        {
            Id = Id,
            Ubicacion = camara.Ubicacion,
            Estado = camara.Estado,
            Modelo = camara.Modelo
        };

        await _camaraServices.UpdateCamara(camaraModel);
        return Created("Created", true);
    }
    //public async Task<IActionResult> UpdateCamara([FromBody] CamaraModel camara, string Id)
    //{
    //    if (camara == null)
    //        return BadRequest();
    //    if (camara.Ubicacion == string.Empty)
    //        ModelState.AddModelError("Name", "El usuario no debe estar vacio");
    //    camara.Id = Id;

    //    await _camaraServices.UpdateCamara(camara);
    //    return Created("Created", true);
    //}

    ///Eliminar Usuario
    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteCamara(string Id)
    {
        await _camaraServices.DeleteCamara(Id);
        return NoContent();
    }
}



