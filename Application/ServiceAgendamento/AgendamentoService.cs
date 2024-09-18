using Domain;
using Domain.Interfaces.Repositorys;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Application.ServiceAgendamento;
public class AgendamentoService : IAgendamentoService
{
    private readonly IAgendamentoRepository _repositoryAgendamento;

    public AgendamentoService(IAgendamentoRepository repositoryAgendamento)
    {
        _repositoryAgendamento = repositoryAgendamento;
    }

    public async Task<ValueResult<AgendamentoModel>> AdicionarAgendamentoAsync(AgendamentoModel agendamento)
    {
        var response = await _repositoryAgendamento.AdicionarAgendamentoAsync(agendamento);

        if (!response.IsSuccess)
        {
            return ValueResult<AgendamentoModel>.Failure("Falha ao Cadastrar Agendamento");
        }
        return ValueResult<AgendamentoModel>.Success(response.Value);
    }

    public Task<ValueResult> ApagarAgendamentoAsync(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<ValueResult<AgendamentoModel>> AtualizarDataAgendamentoAsync(string data, long id)
    {
        var responseObjetoExistente = await _repositoryAgendamento.BuscarAgendamentoPorIdAsync(id);

        if (!responseObjetoExistente.IsSuccess)
        {
            return ValueResult<AgendamentoModel>.Failure("Falha ao acessar Banco de Dados");
        }
        if (responseObjetoExistente.Value == null)
        {
            return ValueResult<AgendamentoModel>.Failure("Id Inexistente");
        }

        var valorAtualizar = responseObjetoExistente.Value;

        if (!DateTime.TryParse(data, out DateTime dataAtendimentoFormatada))
        {
            return ValueResult<AgendamentoModel>.Failure("Data Incorreta");
        }

        valorAtualizar.DataAtendimento = dataAtendimentoFormatada;

        var responseAtualizar = await _repositoryAgendamento.AtualizarAgendamentoAsync(valorAtualizar);

        if (!responseAtualizar.IsSuccess)
        {
            return ValueResult<AgendamentoModel>.Failure(responseAtualizar.ErrorMessage);
        }

        return ValueResult<AgendamentoModel>.Success(valorAtualizar);
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
