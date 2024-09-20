using BCrypt.Net;
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
    
    public async Task<ValueResult<UsuarioModel>> AdicionarUsuarioAsync(UsuarioDto usuario)
    {
        var response = await _repositoryUsuario.AdicionarUsuarioAsync(usuario);

        if (!response.IsSuccess)
        {
            return ValueResult<UsuarioModel>.Failure("Falha ao Adicionar Usuário");
        }
        return ValueResult<UsuarioModel>.Success(response.Value);
    }

    public async Task<ValueResult<List<UsuarioModel>>> BuscarTodosUsuariosAsync()
    {
        var response = await _repositoryUsuario.BuscarTodosUsuariosAsync();

        if (!response.IsSuccess)
        {
            return ValueResult<List<UsuarioModel>>.Failure("Falha ao Carregar Usuários");
        }
        return ValueResult<List<UsuarioModel>>.Success(response.Value);
    }


    public async Task<ValueResult<UsuarioModel>> BuscarUsuarioPorIdAsync(long id)
    {
        var response = await _repositoryUsuario.BuscarUsuarioPorIdAsync(id);

        if (!response.IsSuccess)
        {
            return ValueResult<UsuarioModel>.Failure("Falha ao Carregar Usuário");
        }
        return ValueResult<UsuarioModel>.Success(response.Value);
    }

    public async Task<ValueResult> AtualizarUsuarioAsync(long id, UsuarioDto usuario)
    {
        var responseModel = await _repositoryUsuario.BuscarUsuarioPorIdAsync(id);
        if (!responseModel.IsSuccess || responseModel.Value == null)
        {
            return ValueResult.Failure("Falha ao Carregar Usuário");
        }


        responseModel.Value.NomeUsuario = usuario.NomeUsuario;
        responseModel.Value.Password = BCrypt.Net.BCrypt.HashPassword(usuario.Password);
        responseModel.Value.Email = usuario.Email;
        responseModel.Value.Telefone = usuario.Telefone;
        responseModel.Value.Tipo = usuario.Tipo;


        var response = await _repositoryUsuario.AtualizarUsuarioAsync(responseModel.Value);

        if (!response.IsSuccess)
        {
            return ValueResult.Failure("Falha ao Atualizar Usuário");
        }
        
        return ValueResult.Success();
    }

    public async Task<ValueResult> ApagarUsuarioAsync(long id)
    {
        var responseModel = await _repositoryUsuario.BuscarUsuarioPorIdAsync(id);

        if (!responseModel.IsSuccess || responseModel.Value == null)
        {
            return ValueResult.Failure("Falha ao buscar Usuário");
        }

        var response = await _repositoryUsuario.ApagarUsuarioAsync(responseModel.Value);

        if (!responseModel.IsSuccess)
        {
            return ValueResult.Failure("Falha ao apagar Usuário");
        }

        return ValueResult.Success();
    }


    
}


