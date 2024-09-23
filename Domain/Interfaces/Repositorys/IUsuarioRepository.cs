using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces.Repositorys
{
    public interface IUsuarioRepository
    {
        Task<ValueResult<List<UsuarioModel>>> BuscarTodosUsuariosAsync();
        Task<ValueResult<UsuarioModel>> BuscarUsuarioPorIdAsync(long id);
        Task<ValueResult<List<UsuarioModel>>> BuscarTodosUsuariosPorEmailPacienteAsync(string email);
        Task<ValueResult<List<UsuarioModel>>> BuscarUsuariosPorMedicoResponsavelAsync(string emailMedico);
        Task<ValueResult<UsuarioModel>> AdicionarUsuarioAsync(UsuarioModel usuario);
        Task<ValueResult<UsuarioModel>> AtualizarUsuarioAsync(UsuarioModel usuario);
        Task<ValueResult> ApagarUsuarioAsync(long id); 
    }
}
