using Application.ServiceAgendamento;
using Domain.Interfaces.Services;
using Domain.Models;
using Infrastructure.Migrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;

namespace Presentation.Controllers;

[Authorize(Roles = "Administrador")]
[Route("[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{


   private readonly IUsuarioService _usuarioService;

   private readonly ITokenService _tokenService;

    public UsuarioController(IUsuarioService usuarioService, ITokenService tokenService)
   {
       _usuarioService = usuarioService;
       _tokenService = tokenService;
    }



    [AllowAnonymous]
    [HttpGet]
   public async Task<IActionResult> BuscarTodosUsuariosAsync()
   {
        var response = await _usuarioService.BuscarTodosUsuariosAsync();
        if (!response.IsSuccess)
        {
            return BadRequest();
        }    
        return Ok(response.Value);
   }

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(typeof(UsuarioDto), 201)]
    public async Task<IActionResult> AdicionarUsuario([FromBody] UsuarioDto usuario)
    {       
        var response = await _usuarioService.AdicionarUsuarioAsync(usuario);
        if (!response.IsSuccess)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(AdicionarUsuario), new { id = response.Value.Id }, response.Value);
    }


    [AllowAnonymous]
    [HttpGet("{id}")]
   public async Task<IActionResult> BuscarUsuarioPorId(int id)
   {
       var response = await _usuarioService.BuscarUsuarioPorIdAsync(id);
       if (!response.IsSuccess && response.ErrorMessage == "UmErroAqui")
       {
           return UnprocessableEntity();
       }
       else if (!response.IsSuccess && string.IsNullOrWhiteSpace(response.ErrorMessage))
       {
           return NoContent();
       }

       return Ok(response.Value);
   }


    [AllowAnonymous]
    [HttpPut("{id}")]
   public async Task<IActionResult> AtualizarUsuario(long id, UsuarioDto usuario)
   {
       

        var responseModel = await _usuarioService.BuscarUsuarioPorIdAsync(id);

        if (!responseModel.IsSuccess && responseModel.ErrorMessage == "UmErroAqui")
        {
            return UnprocessableEntity();
        }
        else if (!responseModel.IsSuccess && string.IsNullOrWhiteSpace(responseModel.ErrorMessage))
        {
            return NoContent();
        }

        var response = await _usuarioService.AtualizarUsuarioAsync(id, usuario);
        if (!response.IsSuccess)
        {
            return BadRequest();
        }

        return Ok(response);
   }


    [AllowAnonymous]
    [HttpDelete("{id}")]
   public async Task<IActionResult> Delete(int id)
   {
        var response = await _usuarioService.ApagarUsuarioAsync(id);
        if (!response.IsSuccess)
        {
            return BadRequest();
        }
        return NoContent();
    }


    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetUsuarioAutenticado()
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var nomeUsuario = User.FindFirstValue(ClaimTypes.Name);
        var email = User.FindFirstValue(ClaimTypes.Email);

        if (id ==null || nomeUsuario == null || email == null)
        {
            return BadRequest("Claims não encontrados no token JWT");
        }

        return Ok(new
        {
            Id = id,
            User = nomeUsuario,
            Email = email
        });
           
    }
    


    [AllowAnonymous]
    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate(AuthenticateDto credenciais)
    {
        var response = await _usuarioService.AutenticarUsuarioAsync(credenciais);

        if (!response.IsSuccess)
        {
            return Unauthorized();
        }    
        
        var jwt = _tokenService.GenerateJwtToken(response.Value);

        return Ok(new { jwtToken = jwt, user = new
        {
            id = response.Value.Id,
            user = response.Value.NomeUsuario,
            email = response.Value.Email
        }

        });

    }






}
