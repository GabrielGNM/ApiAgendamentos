using Domain.Interfaces.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

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
        var response = await _agendamentoService.BuscarAgendamentosPorMedicoResponsavelAsync(id)
        if (!response.IsSuccess && response.ErrorMessage == "")
        {
            return UnprocessableEntity();
        }
        return Created();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AgendamentoModel agendamento)
    {
        var response = await _agendamentoService.AdicionarAgendamentoAsync(agendamento);
        if (!response.IsSuccess && response.ErrorMessage == "")
        {
            return UnprocessableEntity();
        }
        return Created();

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] string value)
    {
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok();
    }
}
