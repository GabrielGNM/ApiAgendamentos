using Domain;
using Domain.Interfaces.Repositorys;
using Domain.Interfaces.Services;
using Domain.Models.Controller.Requests;
using Domain.Models.Controller.Validators;
using Domain.Models.Repository;
using FluentValidation.Results;
using System.Globalization;

namespace Application.ServiceAgendamento;
public class AgendamentoService : IAgendamentoService
{
    private readonly IAgendamentoRepository _repositoryAgendamento;

    public AgendamentoService(IAgendamentoRepository repositoryAgendamento)
    {
        _repositoryAgendamento = repositoryAgendamento;
    }

    public async Task<ValueResult<AgendamentoModel>> AdicionarAgendamentoAsync(AgendamentoDto agendamento)
    {
        var validator = new AgendamentoValidator();
        ValidationResult result = validator.Validate(agendamento);

        if (!result.IsValid)
        {
            string validationErros = "";
            foreach (var failure in result.Errors)
            {
                validationErros += $"Erro na propriedade {failure.PropertyName}: {failure.ErrorMessage}" + "\n";
            }
            return ValueResult<AgendamentoModel>.Failure(validationErros);
        }

        var dataAtendimentoFormatada = DateTime.ParseExact(agendamento.DataAtendimento, "dd-MM-yyyy-HH-mm", CultureInfo.InvariantCulture);

        var agendamentoModel = new AgendamentoModel
        {
            NomePaciente = agendamento.NomePaciente,
            DataAtendimento = dataAtendimentoFormatada,
            Email = agendamento.Email,
            EmailMedicoResponsavel = agendamento.EmailMedicoResponsavel,
        };

        var response = await _repositoryAgendamento.AdicionarAgendamentoAsync(agendamentoModel);

        if (!response.IsSuccess)
        {
            return ValueResult<AgendamentoModel>.Failure("Falha ao Cadastrar Agendamento");
        }
        return ValueResult<AgendamentoModel>.Success(response.Value);
    }

    public async Task<ValueResult<AgendamentoModel>> AdicionarEmailMedicoAsync(AdicionarEmailMedicoDto request)
    {
        var validator = new AdicionarEmailMedicoValidator();
        ValidationResult result = validator.Validate(request);

        if (!result.IsValid)
        {
            string validationErros = "";
            foreach (var failure in result.Errors)
            {
                validationErros += $"Erro na propriedade {failure.PropertyName}: {failure.ErrorMessage}" + "\n";
            }
            return ValueResult<AgendamentoModel>.Failure(validationErros);
        }

        var responseObjetoExistente = await _repositoryAgendamento.BuscarAgendamentoPorIdAsync(request.Id);

        if (!responseObjetoExistente.IsSuccess)
        {
            return ValueResult<AgendamentoModel>.Failure("Falha ao acessar Banco de Dados");
        }
        if (responseObjetoExistente.Value == null)
        {
            return ValueResult<AgendamentoModel>.Failure("Id Inexistente");
        }

        var valorAtualizar = responseObjetoExistente.Value;

        valorAtualizar.EmailMedicoResponsavel = request.Email;

        var responseAtualizar = await _repositoryAgendamento.AtualizarAgendamentoAsync(valorAtualizar);

        if (!responseAtualizar.IsSuccess)
        {
            return ValueResult<AgendamentoModel>.Failure(responseAtualizar.ErrorMessage);
        }

        return ValueResult<AgendamentoModel>.Success(valorAtualizar);
    }

    public async Task<ValueResult> ApagarAgendamentoAsync(long id)
    {
        var responseObjetoExistente = await _repositoryAgendamento.BuscarAgendamentoPorIdAsync(id);
        if (!responseObjetoExistente.IsSuccess)
        {
            return ValueResult.Failure("Falha ao acessar Banco de Dados");
        }
        if (responseObjetoExistente.Value == null)
        {
            return ValueResult.Failure("Id Inexistente");
        }
        var responseApagarAgendamento = await _repositoryAgendamento.ApagarAgendamentoAsync(responseObjetoExistente.Value);
        if (!responseApagarAgendamento.IsSuccess)
        {
            return ValueResult.Failure("Falha ao acessar Banco de Dados ");
        }

        return ValueResult.Success();

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
        
        if (!DateTime.TryParseExact(data, "dd-MM-yyyy-HH-mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dataAtendimentoFormatada))
        {
            return ValueResult<AgendamentoModel>.Failure("Formato da Data do Atendimento Incorreto");
        }
        if(dataAtendimentoFormatada <= DateTime.Now)
        {
            return ValueResult<AgendamentoModel>.Failure("Data Menor que a Data Atual");
        }

        valorAtualizar.DataAtendimento = dataAtendimentoFormatada;

        var responseAtualizar = await _repositoryAgendamento.AtualizarAgendamentoAsync(valorAtualizar);

        if (!responseAtualizar.IsSuccess)
        {
            return ValueResult<AgendamentoModel>.Failure(responseAtualizar.ErrorMessage);
        }

        return ValueResult<AgendamentoModel>.Success(valorAtualizar);
    }

    public async Task<ValueResult<AgendamentoModel>> BuscarAgendamentosPorIdAsync(long id)
    {
        var responseRepository = await _repositoryAgendamento.BuscarAgendamentoPorIdAsync(id);

        if (!responseRepository.IsSuccess)
        {
            return ValueResult<AgendamentoModel>.Failure("Falha ao acessar Banco de Dados");
        }
        if (responseRepository.Value == null)
        {
            return ValueResult<AgendamentoModel>.Failure("Id Inexistente");
        }

        return ValueResult<AgendamentoModel>.Success(responseRepository.Value);
    }

    public async Task<ValueResult<List<AgendamentoModel>>> BuscarAgendamentosPorMedicoResponsavelAsync(string email)
    {
        var responseRepository = await _repositoryAgendamento.BuscarAgendamentosPorMedicoResponsavelAsync(email);

        if (!responseRepository.IsSuccess)
        {
            return ValueResult<List<AgendamentoModel>>.Failure(responseRepository.ErrorMessage);
        }
        if (responseRepository.Value == null)
        {
            return ValueResult<List<AgendamentoModel>>.Failure("Email não encontrado.");
        }

        return ValueResult<List<AgendamentoModel>>.Success(responseRepository.Value);
    }

    public async Task<ValueResult<List<AgendamentoModel>>> BuscarAgendamentosPorPacienteAsync(string email)
    {
        var responseRepository = await _repositoryAgendamento.BuscarTodosAgendamentosPorEmailPacienteAsync(email);

        if (!responseRepository.IsSuccess)
        {
            return ValueResult<List<AgendamentoModel>>.Failure(responseRepository.ErrorMessage);
        }
        if (responseRepository.Value == null)
        {
            return ValueResult<List<AgendamentoModel>>.Failure("Email não encontrado.");
        }

        return ValueResult<List<AgendamentoModel>>.Success(responseRepository.Value);
    }

    public async Task<ValueResult<List<AgendamentoModel>>> BuscarTodosAgendamentosAsync()
    {
        var responseBuscarTodosAgendamento = await _repositoryAgendamento.BuscarTodosAgendamentosAsync();

        if (!responseBuscarTodosAgendamento.IsSuccess)
        {
            return ValueResult<List<AgendamentoModel>>.Failure("Falha ao carregar agendamento");
        }

        return ValueResult<List<AgendamentoModel>>.Success(responseBuscarTodosAgendamento.Value);

        throw new NotImplementedException();
    }
}


