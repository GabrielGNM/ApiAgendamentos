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
   public UsuarioController(IUsuarioService usuarioService)
   {
       _usuarioService = usuarioService;
   }


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
    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarUsuarioPorIdAsync(long id)
    {
        var response = await _usuarioService.BuscarUsuarioPorIdAsync(id);

        if (!response.IsSuccess)
        {
            return NotFound("Usuário não encontrado.");
        }

        return Ok(response.Value);
    }



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


    [HttpGet("BuscarPorEmailPaciente/{email}")]
    [ProducesResponseType(typeof(List<UsuarioModel>), 201)]

    public async Task<IActionResult> BuscarTodosUsuariosPorEmailPacienteAsync(string email)
    {

        if (string.IsNullOrWhiteSpace(email))
        {
            return BadRequest("Email não pode ser nulo ou vazio.");
        }


        var response = await _usuarioService.BuscarTodosUsuariosPorEmailPacienteAsync(email);


        if (!response.IsSuccess)
        {
            return NotFound("Nenhum usuário encontrado com o email fornecido.");
        }

        return Ok(response.Value);
    }


    [HttpGet("BuscarPorMedicoResponsavel/{emailMedico}")]
    [ProducesResponseType(typeof(List<UsuarioModel>), 201)] 

    public async Task<IActionResult> BuscarUsuariosPorMedicoResponsavelAsync(string emailMedico)
    {
        if (string.IsNullOrWhiteSpace(emailMedico))
        {
            return BadRequest("Email do médico não pode ser nulo ou vazio.");
        }

        var response = await _usuarioService.BuscarUsuariosPorMedicoResponsavelAsync(emailMedico);
        if (!response.IsSuccess)
        {
            return NotFound("Nenhum usuário encontrado para o médico informado.");
        }

        return Ok(response.Value);
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(204)] 
    public async Task<IActionResult> ApagarUsuarioAsync(int id)
    {
        if (id <= 0)
        {
            return BadRequest("ID do usuário deve ser maior que zero.");
        }

        var response = await _usuarioService.ApagarUsuarioAsync(id);
        if (!response.IsSuccess)
        {
            return NotFound("Usuário não encontrado.");
        }

        return NoContent(); 
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(UsuarioModel), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)] 
    public async Task<IActionResult> AtualizarUsuarioAsync(int id, [FromBody] UsuarioModel usuario)
    {
        if (id <= 0) 
        {
            return BadRequest("ID do usuário deve ser maior que zero.");
        }

        if (usuario == null)
        {
            return BadRequest("Dados do usuário não podem ser nulos.");
        }

        if (usuario.Id != id)
        {
            return BadRequest("O ID do usuário não corresponde ao ID fornecido na URL.");
        }

        var response = await _usuarioService.AtualizarUsuarioAsync(id, usuario);
        if (!response.IsSuccess)
        {
            return NotFound("Usuário não encontrado.");
        }

        return Ok(response.Value); 
    }


}
