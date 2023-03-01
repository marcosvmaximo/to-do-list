using System;
using ListaTarefaCRUD.Api.Infra;
using ListaTarefaCRUD.Api.Interfaces.Repositories;
using ListaTarefaCRUD.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ListaTarefaCRUD.Api.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly DataContext _context;

    public UsuarioRepository(DataContext context)
    {
        _context = context;
    }


    public async Task<Usuario> AddAsync(Usuario entity)
    {
        var result = await _context.Usuarios.AddAsync(entity);
        _context.SaveChanges();
        return result.Entity;
    }

    public async Task ChangeAsync(Usuario entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public async Task DeleteAsync(Usuario entity)
    {
        _context.Usuarios.Remove(entity);
        _context.SaveChanges();
    }

    public async Task<IReadOnlyCollection<Usuario>> GetAll()
    {
        return _context.Usuarios.ToList();
        _context.SaveChanges();
    }

    public async Task<Usuario> GetById(int id)
    {
        return _context.Usuarios.FirstOrDefault(x => x.Id == id) ?? throw new Exception();
        _context.SaveChanges();
    }
}

