

using Application.ServiceAgendamento;
using Domain.Interfaces.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace Presentation.Controllers;

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
       return Ok();
   }

    [HttpPost]
    [ProducesResponseType(typeof(UsuarioModel), 201)]
    public async Task<IActionResult> CadastrarUsuario([FromBody] UsuarioModel agendamento)
    {
        var response = await _usuarioService.AdicionarUsuarioAsync(agendamento);
        if (!response.IsSuccess)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(CadastrarUsuario), new { id = response.Value.Id }, response.Value);
    }

    /*

   [HttpGet("{id}")]
   public async Task<IActionResult> Get(int id)
   {
       var response = await _agendamentoService.BuscarAgendamentosPorMedicoResponsavelAsync(id);
       if (!response.IsSuccess && response.ErrorMessage == "UmErroAqui")
       {
           return UnprocessableEntity();
       }
       else if (!response.IsSuccess && string.IsNullOrWhiteSpace(response.ErrorMessage))
       {
           return NoContent();
       }

       return Ok();
   }

 


   [HttpPut("AtualizarData/{id}")]
   public async Task<IActionResult> AtualizarData([Required][FromQuery] string data, long id)
   {
       var response = await _agendamentoService.AtualizarDataAgendamentoAsync(data, id);
       if (!response.IsSuccess)
       {
           return BadRequest();
       }
       return Ok(response.Value);
   }

   [HttpDelete("{id}")]
   public async Task<IActionResult> Delete(int id)
   {
       return Ok();
   }

    */


}
