using Domain.Interfaces.Services;
using Domain.Models.Controller.Requests;
using Domain.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Controllers;

[Route("[controller]")]
[ApiController]
public class AgendamentosController(IAgendamentoService agendamentoService) : ControllerBase
{
    private readonly IAgendamentoService _agendamentoService = agendamentoService;

    [HttpGet]
    [ProducesResponseType(typeof(AgendamentoModel), 200)]
    [ProducesResponseType(503)]
    [ProducesResponseType(204)]
    public async Task<IActionResult> BuscarTodosAgendamentosAsync()
    {
        var response = await _agendamentoService.BuscarTodosAgendamentosAsync();

        if (response.Value == null)
        {
            return NoContent();
        }
        else if (!response.IsSuccess)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable, response.ErrorMessage);
        }

        return Ok(response);
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(AgendamentoModel), 200)]
    [ProducesResponseType(503)]
    [ProducesResponseType(204)]
    public async Task<IActionResult> BuscarAgendamentosPorIdAsync(long id)
    {
        var response = await _agendamentoService.BuscarAgendamentosPorIdAsync(id);

        if (response.Value == null)
        {
            return NoContent();
        }
        else if (!response.IsSuccess)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable, response.ErrorMessage);
        }

        return Ok(response);
    }

    [HttpGet("BuscarPorMedico/{email}")]
    [ProducesResponseType(typeof(AgendamentoModel), 200)]
    [ProducesResponseType(503)]
    [ProducesResponseType(422)]
    [ProducesResponseType(204)]
    public async Task<IActionResult> BuscarAgendamentosPorMédico(string email)
    {
        var response = await _agendamentoService.BuscarAgendamentosPorMedicoResponsavelAsync(email);
        if (!response.IsSuccess && response.ErrorMessage == "Email não encontrado.")
        {
            return NoContent();
        }
        else if (!response.IsSuccess && response.ErrorMessage == "Falha ao acessar base de dados")
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable, response.ErrorMessage);
        }
        else if (!response.IsSuccess)
        {
            return BadRequest(response.ErrorMessage);
        }

        return Ok(response);
    }
    
    [HttpGet("BuscarPorPaciente/{email}")]
    [ProducesResponseType(typeof(AgendamentoModel), 200)]
    [ProducesResponseType(503)]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    public async Task<IActionResult> BuscarAgendamentosPorPaciente(string email)
    {
        var response = await _agendamentoService.BuscarAgendamentosPorPacienteAsync(email);

        if (!response.IsSuccess && response.ErrorMessage == "Email não encontrado.")
        {
            return NoContent();
        }
        else if (!response.IsSuccess && response.ErrorMessage == "Falha ao acessar base de dados")
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable, response.ErrorMessage);
        }
        else if (!response.IsSuccess)
        {
            return BadRequest(response.ErrorMessage);
        }

        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(AgendamentoModel), 201)]
    [ProducesResponseType(503)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CadastrarAgendamento([FromBody] AgendamentoDto agendamento)
    {
        var response = await _agendamentoService.AdicionarAgendamentoAsync(agendamento);

        if (!response.IsSuccess && response.ErrorMessage == "Falha ao acessar base de dados")
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable, response.ErrorMessage);
        }
        else if (!response.IsSuccess)
        {
            return StatusCode(StatusCodes.Status422UnprocessableEntity, response.ErrorMessage);
        }
        return CreatedAtAction(nameof(CadastrarAgendamento),new { id = response.Value.Id}, response.Value);
    }

    [HttpPut("AtualizarData/{id}")]
    [ProducesResponseType(typeof(AgendamentoModel), 200)]
    [ProducesResponseType(503)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> AtualizarData([Required][FromQuery] string data, long id)
    {
        var response = await _agendamentoService.AtualizarDataAgendamentoAsync(data, id);

        if (!response.IsSuccess && response.ErrorMessage == "Falha ao acessar base de dados")
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable, response.ErrorMessage);
        }
        else if (!response.IsSuccess)
        {
            return StatusCode(StatusCodes.Status422UnprocessableEntity, response.ErrorMessage);
        }

        return Ok(response.Value);
    }
    
    
    [HttpPut("CadastrarEmailMedico/{id}")]
    [ProducesResponseType(typeof(AgendamentoModel), 200)]
    [ProducesResponseType(503)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CadastrarEmailMedico([Required][FromQuery] string email, long id)
    {
        var request = new AdicionarEmailMedicoDto { Email = email, Id = id };

        var response = await _agendamentoService.AdicionarEmailMedicoAsync(request);

        if (!response.IsSuccess && response.ErrorMessage == "Falha ao acessar base de dados")
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable, response.ErrorMessage);
        }
        else if (!response.IsSuccess)
        {
            return StatusCode(StatusCodes.Status422UnprocessableEntity, response.ErrorMessage);
        }

        return Ok(response.Value);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(201)]
    [ProducesResponseType(503)]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _agendamentoService.ApagarAgendamentoAsync(id);

        if (!response.IsSuccess && response.ErrorMessage == "Id Inexistente")
        {
            return BadRequest(response.ErrorMessage);
        }
        else if (!response.IsSuccess) 
        { 
            return StatusCode(StatusCodes.Status503ServiceUnavailable, response.ErrorMessage);
        }
        return Ok();
    }
}
