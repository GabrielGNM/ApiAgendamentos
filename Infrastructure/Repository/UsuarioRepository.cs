﻿using BCrypt.Net;
using Domain;
using Domain.Interfaces.Repositorys;
using Domain.Models;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class UsuarioRepository(UsuariosContext context) : IUsuarioRepository
{
    private readonly UsuariosContext _context = context;
    
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

    public async Task<ValueResult<UsuarioModel>> BuscarUsuarioPorEmailAsync(string email)
    {
        try
        {
            return ValueResult<UsuarioModel>.Success(
                await _context.Usuarios
                .Where(x => x.Email == email)
                .FirstOrDefaultAsync());
        }
        catch
        {
            return ValueResult<UsuarioModel>.Failure("Falha ao acessar base de dados");
        }
    }

    public async Task<ValueResult<UsuarioModel>> AdicionarUsuarioAsync(UsuarioDto Usuario)
    {

        try
        {
            var emailExistente = await _context.Usuarios
                .AnyAsync(u => u.Email == Usuario.Email);

            if (emailExistente)
            {
                return ValueResult<UsuarioModel>.Failure("Email já cadastrado");
            }



            UsuarioModel novoUsuario = new UsuarioModel()
            {
                NomeUsuario = Usuario.NomeUsuario,
                Password = BCrypt.Net.BCrypt.HashPassword(Usuario.Password),
                Email = Usuario.Email,
                Telefone = Usuario.Telefone,
                Tipo = Usuario.Tipo
            };


            await _context.Usuarios.AddAsync(novoUsuario);
            await _context.SaveChangesAsync();
            return ValueResult<UsuarioModel>.Success(novoUsuario);
        }
        catch (Exception ex) 
        {
            return ValueResult<UsuarioModel>.Failure("Falha ao acessar base de dados: { ex.Message}");
        }
    }


    public async Task<ValueResult> AtualizarUsuarioAsync(UsuarioModel Usuario)
    {

        try
        {
            _context.Usuarios.Update(Usuario);
            await _context.SaveChangesAsync();
            return ValueResult.Success();
        }
        catch
        {
            return ValueResult.Failure("Falha ao acessar base de dados");
        }
    }

    public async Task<ValueResult> ApagarUsuarioAsync(UsuarioModel Usuario)
    {
        try
        {
            _context.Usuarios.Remove(Usuario);
            await _context.SaveChangesAsync();
            return ValueResult.Success();
        }
        catch
        {
            return ValueResult.Failure("Falha ao acessar base de dados");
        }
    }
}
