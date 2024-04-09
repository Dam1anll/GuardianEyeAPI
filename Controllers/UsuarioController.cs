using Microsoft.AspNetCore.Mvc;
using GuardianEyeAPI.Models;
using GuardianEyeAPI.Services;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;

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

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UsuarioModel usuario)
    {
        if (usuario == null)
            return BadRequest("Datos de usuario inválidos.");

        // Puedes realizar aquí la validación adicional, por ejemplo, si el usuario ya existe, etc.

        await _usuarioServices.InsertDriver(usuario);

        return Ok("Usuario registrado exitosamente.");
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel login)
    {
        if (login == null || string.IsNullOrEmpty(login.Correo) || string.IsNullOrEmpty(login.Contraseña))
            return BadRequest("Credenciales inválidas.");

        var usuario = await _usuarioServices.GetByEmailAndPassword(login.Correo, login.Contraseña);

        if (usuario == null)
            return Unauthorized("Correo o contraseña incorrectos.");

        // Aquí puedes generar el token JWT y devolverlo en la respuesta
        var token = JwtHelper.GenerateToken(usuario);
        return Ok(new { Token = token });
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
