using Domain.Models;

namespace Domain.Interfaces.Repositorys;

public interface IUsuarioRepository
{
    Task<ValueResult<List<UsuarioModel>>> BuscarTodosUsuariosAsync();
    Task<ValueResult<List<UsuarioModel>>> BuscarTodosUsuariosPorEmailPacienteAsync(string email);
    Task<ValueResult<List<UsuarioModel>>> BuscarUsuariosPorMedicoResponsavelAsync(string emailMedico);
    Task<ValueResult<UsuarioModel>> BuscarUsuarioPorIdAsync(long id);
    //Task<ValueResult<AgendamentoModel>> AdicionarAgendamentoAsync(AgendamentoModel agendamento);
    //Task<ValueResult> AtualizarAgendamentoAsync(AgendamentoModel agendamento);
    //Task<ValueResult> ApagarAgendamentoAsync(AgendamentoModel agendamento);
}
