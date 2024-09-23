using Domain.Models;

namespace Domain.Interfaces.Services
{
    public interface IUsuarioService
    {
        Task<ValueResult<List<UsuarioModel>>> BuscarTodosUsuariosAsync();
        Task<ValueResult<UsuarioModel>> BuscarUsuarioPorIdAsync(long id);
        Task<ValueResult<UsuarioModel>> AdicionarUsuarioAsync(UsuarioModel usuario);
        Task<ValueResult<UsuarioModel>> AtualizarUsuarioAsync(long id, UsuarioModel usuarioAtualizado);
        Task<ValueResult> ApagarUsuarioAsync(long id);
        Task<ValueResult<List<UsuarioModel>>> BuscarTodosUsuariosPorEmailPacienteAsync(string email);
        Task<ValueResult<List<UsuarioModel>>> BuscarUsuariosPorMedicoResponsavelAsync(string emailMedico);
    }
}
