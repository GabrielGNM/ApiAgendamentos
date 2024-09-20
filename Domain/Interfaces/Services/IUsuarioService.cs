using Domain.Models;

namespace Domain.Interfaces.Services;

public interface IUsuarioService
{
    Task<ValueResult<List<UsuarioModel>>> BuscarTodosUsuariosAsync();
    Task<ValueResult<UsuarioModel>> AdicionarUsuarioAsync(UsuarioDto usuario);
    Task<ValueResult> AtualizarUsuarioAsync(long id);
    Task<ValueResult<UsuarioModel>> BuscarUsuarioPorIdAsync(long id);
    Task<ValueResult> ApagarUsuarioAsync(long id);
}
