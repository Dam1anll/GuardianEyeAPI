using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using GuardianEyeAPI.Models;
using System.Security.Cryptography;

public static class JwtHelper
{
    private const int KeySize = 256; // Tamaño de la clave en bits
    private const string SecretKey = "aquí-va-tu-clave-secreta";

    public static string GenerateToken(UsuarioModel usuario)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = GenerateRandomKey();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id),
                new Claim(ClaimTypes.Name, usuario.Correo)
                // Puedes agregar más claims según tus necesidades
            }),
            Expires = DateTime.UtcNow.AddDays(7), // Cambia esto según tus necesidades
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private static byte[] GenerateRandomKey()
    {
        using (var generator = RandomNumberGenerator.Create())
        {
            var key = new byte[KeySize / 8];
            generator.GetBytes(key);
            return key;
        }
    }
}