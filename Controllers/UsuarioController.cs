using Microsoft.AspNetCore.Mvc;
using GuardianEyeAPI.Models;
using GuardianEyeAPI.Services;

namespace Drivers.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly ILogger<UsuarioController> _logger;

    private readonly UsuarioServices _usuarioServices;

    public UsuarioController(ILogger<UsuarioController> logger, UsuarioServices usuarioServices)
    {
        _logger = logger;
        _usuarioServices = usuarioServices;
    }

    ///Obtener todos los Usuarios
    [HttpGet]
    public async Task<IActionResult> GetUsuarios()
    {
        var usuarios = await _usuarioServices.GetAsync();
        return Ok(usuarios);
    }

    ///Obtener Usuario por Id
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetUsuariosById(string Id)
    {
        return Ok(await _usuarioServices.GetDriverById(Id));
    }

    ///Crear Usuario
    [HttpPost]
    public async Task<IActionResult> CreateUsuario([FromBody] UsuarioModel usuario)
    {
        if (usuario == null)
            return BadRequest();
        if (usuario.Nombre == string.Empty)
            ModelState.AddModelError("Name", "El usuario no debe estar vacio");

        await _usuarioServices.InsertDriver(usuario);

        return Created("Created", true);
    }

    ///Actualizar Usuario
    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateDriver([FromBody] UsuarioModel usuario, string Id)
    {
        if (usuario == null)
            return BadRequest();
        if (usuario.Nombre == string.Empty)
            ModelState.AddModelError("Name", "El usuario no debe estar vacio");
        usuario.Id = Id;

        await _usuarioServices.UpdateDriver(usuario);
        return Created("Created", true);
    }

    ///Eliminar Usuario
    [HttpDelete]
    public async Task<IActionResult> DeleteUsuario(string Id)
    {
        await _usuarioServices.DeleteDriver(Id);
        return NoContent();
    }
}
