using BCrypt.Net;
using Azure;
using Domain;
using Domain.Interfaces.Repositorys;
using Domain.Interfaces.Services;
using Domain.Models;
using Infrastructure.Migrations;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Application.ServiceAgendamento;
public class TokenService : ITokenService
{
    private readonly IConfiguration _configurationToken;
    public TokenService(IConfiguration configuration)
    {
        _configurationToken = configuration;
    }

    public string GenerateJwtToken(UsuarioModel usuario)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configurationToken["tokenJWT"]);
        var claims = new[] 
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Name, usuario.NomeUsuario.ToString()),
            new Claim(ClaimTypes.Email, usuario.Email.ToString()),
            new Claim(ClaimTypes.Role, usuario.Perfil.ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }


}


