using Domain.Models.Controller.Requests;
using Domain.Models.Repository;

namespace Domain.Interfaces.Services;

public interface IAgendamentoService
{
    Task<ValueResult<List<AgendamentoModel>>> BuscarTodosAgendamentosAsync();
    Task<ValueResult<AgendamentoModel>> BuscarAgendamentosPorIdAsync(long id);
    Task<ValueResult<List<AgendamentoModel>>> BuscarAgendamentosPorMedicoResponsavelAsync(string email);
    Task<ValueResult<List<AgendamentoModel>>> BuscarAgendamentosPorPacienteAsync(string email);
    Task<ValueResult<AgendamentoModel>> AdicionarAgendamentoAsync(AgendamentoDto paciente);
    Task<ValueResult<AgendamentoModel>> AtualizarDataAgendamentoAsync(string data, long id);
    Task<ValueResult<AgendamentoModel>> AdicionarEmailMedicoAsync(AdicionarEmailMedicoDto request);
    Task<ValueResult> ApagarAgendamentoAsync(long id);
}
