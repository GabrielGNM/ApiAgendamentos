using Domain.Models;

namespace Domain.Interfaces.Services;

public interface ITokenService
{
    string GenerateJwtToken(UsuarioModel usuario);
}
