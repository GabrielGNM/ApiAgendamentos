﻿using Domain.Models;

namespace Domain.Interfaces.Repositorys;

public interface IUsuarioRepository
{
    Task<ValueResult<List<UsuarioModel>>> BuscarTodosUsuariosAsync();
    Task<ValueResult<List<UsuarioModel>>> BuscarTodosUsuariosPorEmailPacienteAsync(string email);
    Task<ValueResult<List<UsuarioModel>>> BuscarUsuariosPorMedicoResponsavelAsync(string emailMedico);
    Task<ValueResult<UsuarioModel>> BuscarUsuarioPorIdAsync(long id);
    Task<ValueResult<UsuarioModel>> BuscarUsuarioPorEmailAsync(string email);
    Task<ValueResult<UsuarioModel>> AdicionarUsuarioAsync(UsuarioDto Usuario);
    Task<ValueResult> AtualizarUsuarioAsync(UsuarioModel Usuario);
    Task<ValueResult> ApagarUsuarioAsync(UsuarioModel Usuario);
    
}
