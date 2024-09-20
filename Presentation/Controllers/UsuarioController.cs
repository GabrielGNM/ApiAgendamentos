

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
        var response = await _usuarioService.BuscarTodosUsuariosAsync();
        if (!response.IsSuccess)
        {
            return BadRequest();
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

 


   [HttpPut("{id}")]
   public async Task<IActionResult> AtualizarUsuario(long id)
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

        var response = await _usuarioService.AtualizarUsuarioAsync(responseModel.Value.Id);
        if (!response.IsSuccess)
        {
            return BadRequest();
        }

        return Ok(response);
   }



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

   

}
