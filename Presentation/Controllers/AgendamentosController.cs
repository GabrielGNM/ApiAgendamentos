using Domain.Interfaces.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Controllers;

[Route("[controller]")]
[ApiController]
public class AgendamentosController : ControllerBase
{
    private readonly IAgendamentoService _agendamentoService;
    public AgendamentosController(IAgendamentoService agendamentoService)
    {
        _agendamentoService = agendamentoService;
    }

    [HttpGet]
    public async Task<IActionResult> BuscarTodosAgendamentosAsync()
    {
        return Ok();
    }

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

    [HttpPost]
    [ProducesResponseType(typeof(AgendamentoModel), 201)]
    public async Task<IActionResult> CadastrarAgendamento([FromBody] AgendamentoModel agendamento)
    {
        var response = await _agendamentoService.AdicionarAgendamentoAsync(agendamento);
        if (!response.IsSuccess)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(CadastrarAgendamento),new { id = response.Value.Id}, response.Value);
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
}
