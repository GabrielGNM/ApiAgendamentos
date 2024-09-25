using BCrypt.Net;
using Domain;
using Domain.Interfaces.Repositorys;
using Domain.Models;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AgendamentoContext _context;

        public UsuarioRepository(AgendamentoContext context)
        {
            _context = context;
        }

        public async Task<ValueResult<List<UsuarioModel>>> BuscarTodosUsuariosAsync()
        {
            try
            {
                var usuarios = await _context.Usuarios.ToListAsync();
                return ValueResult<List<UsuarioModel>>.Success(usuarios);
            }
            catch
            {
                return ValueResult<List<UsuarioModel>>.Failure("Falha ao acessar base de dados");
            }
        }

        public async Task<ValueResult<UsuarioModel>> BuscarUsuarioPorIdAsync(int id)
        {
            if (id <= 0)
            {
                return ValueResult<UsuarioModel>.Failure("ID deve ser um número positivo.");
            }

            try
            {
                var usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (usuario == null)
                {
                    return ValueResult<UsuarioModel>.Failure("Usuário não encontrado.");
                }

                return ValueResult<UsuarioModel>.Success(usuario);
            }
            catch (Exception ex)
            {
                return ValueResult<UsuarioModel>.Failure("Falha ao acessar a base de dados: " + ex.Message);
            }
        }


        public async Task<ValueResult<List<UsuarioModel>>> BuscarTodosUsuariosPorEmailPacienteAsync(string email)
        { 
            if (string.IsNullOrWhiteSpace(email))
            {
                return ValueResult<List<UsuarioModel>>.Failure("Email não pode ser nulo ou vazio.");
            }

            try
            {
                var usuarios = await _context.Usuarios
                    .Where(x => x.Tipo == Tipo.Cliente && x.Email == email)
                    .OrderBy(x => x.Perfil)
                    .ToListAsync();

                return ValueResult<List<UsuarioModel>>.Success(usuarios);
            }
            catch (Exception ex)
            {
                return ValueResult<List<UsuarioModel>>.Failure("Falha ao acessar base de dados: " + ex.Message);
            }
        }


        public async Task<ValueResult<List<UsuarioModel>>> BuscarUsuariosPorMedicoResponsavelAsync(string emailMedico)
        {
            try
            {
                var usuarios = await _context.Usuarios
                    .Where(x => x.Tipo == Tipo.Profissional && x.Email == emailMedico)
                    .OrderBy(x => x.Perfil)
                    .ToListAsync();
                return ValueResult<List<UsuarioModel>>.Success(usuarios);
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
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);

                if (usuario == null)
                {
                    return ValueResult<UsuarioModel>.Failure("Usuário não encontrado");
                }

                return ValueResult<UsuarioModel>.Success(usuario);
            }
            catch
            {
                return ValueResult<UsuarioModel>.Failure("Falha ao acessar base de dados");
            }
        }

        public async Task<ValueResult<UsuarioModel>> AdicionarUsuarioAsync(UsuarioModel usuario)
        {
            try
            {
                await _context.Usuarios.AddAsync(usuario);
                await _context.SaveChangesAsync();
                return ValueResult<UsuarioModel>.Success(usuario);
            }
            catch
            {
                return ValueResult<UsuarioModel>.Failure("Falha ao acessar base de dados");
            }
        }

        public async Task<ValueResult<UsuarioModel>> AtualizarUsuarioAsync(UsuarioModel usuario)
        {
            try
            {
                _context.Usuarios.Update(usuario);
                await _context.SaveChangesAsync();
                return ValueResult<UsuarioModel>.Success(usuario); 
            }
            catch
            {
                return ValueResult<UsuarioModel>.Failure("Falha ao acessar base de dados");
            }
        }

        public async Task<ValueResult> ApagarUsuarioAsync(long id)
        {
            try
            {
                var usuario = await _context.Usuarios.FindAsync(id);
                if (usuario == null)
                {
                    return ValueResult.Failure("Usuário não encontrado");
                }

                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
                return ValueResult.Success();
            }
            catch
            {
                return ValueResult.Failure("Falha ao acessar base de dados");
            }
        }

    }
}
