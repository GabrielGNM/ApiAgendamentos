using Domain.Models;

namespace Domain.Interfaces.Services;

public interface IAgendamentoService
{
    Task<ValueResult<List<AgendamentoModel>>> BuscarTodosAgendamentosAsync();
    Task<ValueResult<AgendamentoModel>> BuscarAgendamentosPorMedicoResponsavelAsync(long id);
    Task<ValueResult<AgendamentoModel>> AdicionarAgendamentoAsync(AgendamentoModel paciente);
    Task<ValueResult<AgendamentoModel>> AtualizarDataAgendamentoAsync(string data, long id);
    Task<ValueResult> ApagarAgendamentoAsync(long id);
}
