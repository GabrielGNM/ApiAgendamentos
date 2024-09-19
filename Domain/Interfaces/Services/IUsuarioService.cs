using Domain.Models;

namespace Domain.Interfaces.Services;

public interface IUsuarioService
{
    Task<ValueResult<List<UsuarioModel>>> BuscarTodosUsuariosAsync();
    Task<ValueResult<UsuarioModel>> AdicionarUsuarioAsync(UsuarioModel usuario);
    /*Task<ValueResult<UsuarioModel>> AtualizarUsuarioAsync(long id);
    Task<ValueResult<UsuarioModel>> BuscarUsuarioAsync(long id);
    Task<ValueResult> ApagarUsuarioAsync(long id);*/
}
