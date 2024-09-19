using Domain;
using Domain.Interfaces.Repositorys;
using Domain.Models;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class UsuarioRepository(AgendamentoContext context) : IUsuarioRepository
{
    private readonly AgendamentoContext _context = context;

    public async Task<ValueResult<List<UsuarioModel>>> BuscarTodosUsuariosAsync()
    {
        try
        {
            return ValueResult<List<UsuarioModel>>.Success(
                await _context.Usuarios
                .ToListAsync());
        }
        catch
        {
            return ValueResult<List<UsuarioModel>>.Failure("Falha ao acessar base de dados");
        }
    }

    public async Task<ValueResult<List<UsuarioModel>>> BuscarTodosUsuariosPorEmailPacienteAsync(string email)
    {
        try
        {
            return ValueResult<List<UsuarioModel>>.Success(
                await _context.Usuarios
                .Where(x => x.Tipo == Tipo.Cliente) 
                .ToListAsync());
        }
        catch
        {
            return ValueResult<List<UsuarioModel>>.Failure("Falha ao acessar base de dados");
        }
    }


    public async Task<ValueResult<List<UsuarioModel>>> BuscarUsuariosPorMedicoResponsavelAsync(string email)
    {
        try
        {
            return ValueResult<List<UsuarioModel>>.Success(
                await _context.Usuarios
                .Where(x => x.Tipo == Tipo.Profissional)
                .OrderBy(x => x.Perfil)
                .ToListAsync());
        }
        catch
        {
            return ValueResult<List<UsuarioModel>>.Failure("Falha ao acessar base de dados");
        }
    }

    public async Task<ValueResult<UsuarioModel>> BuscarUsuarioPorIdAsync(long id)
    {
        try
        {
            return ValueResult<UsuarioModel>.Success(
                await _context.Usuarios
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync());
        }
        catch
        {
            return ValueResult<UsuarioModel>.Failure("Falha ao acessar base de dados");
        }
    }

    public async Task<ValueResult<UsuarioModel>> AdicionarUsuarioAsync(UsuarioModel Usuario)
    {
        try
        {
            await _context.Usuarios.AddAsync(Usuario);
            await _context.SaveChangesAsync();
            return ValueResult<UsuarioModel>.Success(Usuario);
        }
        catch
        {
            return ValueResult<UsuarioModel>.Failure("Falha ao acessar base de dados");
        }
    }

    public Task<ValueResult<AgendamentoModel>> AdicionarAgendamentoAsync(AgendamentoModel agendamento)
    {
        throw new NotImplementedException();
    }

    //public async Task<ValueResult> AtualizarUsuarioAsync(UsuarioModel Usuario)
    //{
    //    try
    //    {
    //        _context.Usuarios.Update(Usuario);
    //        await _context.SaveChangesAsync();
    //        return ValueResult.Success();
    //    }
    //    catch
    //    {
    //        return ValueResult.Failure("Falha ao acessar base de dados");
    //    }
    //}

    //public async Task<ValueResult> ApagarUsuarioAsync(UsuarioModel Usuario)
    //{
    //    try
    //    {
    //        _context.Usuarios.Remove(Usuario);
    //        await _context.SaveChangesAsync();
    //        return ValueResult.Success();
    //    }
    //    catch
    //    {
    //        return ValueResult.Failure("Falha ao acessar base de dados");
    //    }
    //}
}
