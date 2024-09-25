using BCrypt.Net;
using Azure;
using Domain;
using Domain.Interfaces.Repositorys;
using Domain.Interfaces.Services;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.ServiceAgendamento
{
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

        public async Task<ValueResult<UsuarioModel>> AtualizarUsuarioAsync(long id, UsuarioModel usuarioAtualizado)
        {
            var usuarioExistente = await _repositoryUsuario.BuscarUsuarioPorIdAsync(id);

            if (!usuarioExistente.IsSuccess || usuarioExistente.Value == null)
            {
                return ValueResult<UsuarioModel>.Failure("Usuário não encontrado para o ID fornecido.");
            }

            var usuario = usuarioExistente.Value;

            usuario.NomeUsuario = usuarioAtualizado.NomeUsuario ?? usuario.NomeUsuario;
            usuario.Email = usuarioAtualizado.Email ?? usuario.Email;
            usuario.Telefone = usuarioAtualizado.Telefone ?? usuario.Telefone;
            usuario.Tipo = usuarioAtualizado.Tipo;
            usuario.Perfil = usuarioAtualizado.Perfil;

            if (!string.IsNullOrEmpty(usuarioAtualizado.Password))
            {
                usuario.Password = usuarioAtualizado.Password;
            }

            var response = await _repositoryUsuario.AtualizarUsuarioAsync(usuario);

            if (!response.IsSuccess)
            {
                return ValueResult<UsuarioModel>.Failure("Falha ao atualizar o usuário.");
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
            if (id <= 0)
            {
                return ValueResult<UsuarioModel>.Failure("ID deve ser um número positivo.");
            }
            try
            {
                var usuario = await _repositoryUsuario.BuscarUsuarioPorIdAsync(id);

                if (!usuario.IsSuccess)
                {
                    return ValueResult<UsuarioModel>.Failure("Usuário não encontrado."); 
                }

                return ValueResult<UsuarioModel>.Success(usuario.Value);
            }
            catch (Exception ex)
            {
                return ValueResult<UsuarioModel>.Failure("Erro ao acessar a base de dados: " + ex.Message);
            }
        }




        public async Task<ValueResult<List<UsuarioModel>>> BuscarUsuariosPorMedicoResponsavelAsync(string emailMedico)
        {
            var response = await _repositoryUsuario.BuscarUsuariosPorMedicoResponsavelAsync(emailMedico);

            if (!response.IsSuccess)
            {
                return ValueResult<List<UsuarioModel>>.Failure("Falha ao Buscar Usuários pelo Médico Responsável");
            }

            return ValueResult<List<UsuarioModel>>.Success(response.Value);
        }

        public async Task<ValueResult<List<UsuarioModel>>> BuscarTodosUsuariosPorEmailPacienteAsync(string email)
        {
            var response = await _repositoryUsuario.BuscarTodosUsuariosPorEmailPacienteAsync(email);

            if (!response.IsSuccess)
            {
                return ValueResult<List<UsuarioModel>>.Failure("Falha ao Buscar Usuários por Email do Paciente");
            }

            return ValueResult<List<UsuarioModel>>.Success(response.Value);
        }

        public async Task<ValueResult> ApagarUsuarioAsync(long id)
        {
            var response = await _repositoryUsuario.ApagarUsuarioAsync(id);

            if (!response.IsSuccess)
            {
                return ValueResult.Failure("Falha ao apagar o usuário.");
            }

            return ValueResult.Success();
        }


    }
}