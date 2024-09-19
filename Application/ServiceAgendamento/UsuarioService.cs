using Azure;
using Domain;
using Domain.Interfaces.Repositorys;
using Domain.Interfaces.Services;
using Domain.Models;
using Infrastructure.Migrations;

namespace Application.ServiceAgendamento;
public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _repositoryUsuario;

    public UsuarioService(IUsuarioRepository repositoryUsuario)
    {
        _repositoryUsuario = repositoryUsuario;
    }
    
    public async Task<ValueResult<UsuarioModel>> AdicionarUsuarioAsync(UsuarioModel usuario)
    {
        var response = await _repositoryUsuario.AdicionarUsuarioAsync(usuario);

        if (!response.IsSuccess)
        {
            return ValueResult<UsuarioModel>.Failure("Falha ao Cadastrar Usuário");
        }
        return ValueResult<UsuarioModel>.Success(response.Value);
    }

    public Task<ValueResult<List<UsuarioModel>>> BuscarTodosUsuariosAsync()
    {
        var response = await _repositoryUsuario.BuscarTodosUsuariosAsync(usuario);

        if (!response.IsSuccess)
        {
            return ValueResult<List<UsuarioModel>>.Failure("Falha ao Carregar Usuário");
        }
        return ValueResult<List<UsuarioModel>>.Success(response.Value);
    }
}
/*
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

*/

