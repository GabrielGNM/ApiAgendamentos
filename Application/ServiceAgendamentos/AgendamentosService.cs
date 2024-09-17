using Domain;
using Domain.Interfaces.Repositorys;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Application.ServiceAgendamentos;
internal class AgendamentosService : IAgendamentoService
{
    private readonly IAgendamentoRepository _repositoryAgendamento;

    public AgendamentosService(IAgendamentoRepository repositoryAgendamento)
    {
        _repositoryAgendamento = repositoryAgendamento;
    }

    public async Task<ValueResult> AdicionarAgendamentoAsync(AgendamentoModel agendamento)
    {
        var response = await _repositoryAgendamento.AdicionarAgendamentoAsync(agendamento);

        if (!response.IsSuccess)
        {
            return ValueResult.Failure("Falha ao Cadastrar Agendamento");
        }
        return ValueResult.Success();
    }

    public Task<ValueResult> ApagarAgendamentoAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<ValueResult<AgendamentoModel>> AtualizarAgendamentoAsync(AgendamentoModel paciente)
    {
        throw new NotImplementedException();
    }

    public Task<ValueResult<AgendamentoModel>> BuscarAgendamentosPorMedicoResponsavelAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<ValueResult<List<AgendamentoModel>>> BuscarTodosAgendamentosAsync()
    {
        throw new NotImplementedException();
    }
}
