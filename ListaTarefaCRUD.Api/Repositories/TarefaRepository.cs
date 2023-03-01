using System;
using ListaTarefaCRUD.Api.Infra;
using ListaTarefaCRUD.Api.Interfaces.Repositories;
using ListaTarefaCRUD.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ListaTarefaCRUD.Api.Repositories;

public class TarefaRepository : ITarefaRepository
{
    private readonly DataContext _context;

    public TarefaRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Tarefa> AddAsync(Tarefa entity)
    {
        var result = await _context.Tarefas.AddAsync(entity);
        _context.SaveChanges();
        return _context.Tarefas
            .Include(x => x.Usuario)
            .FirstOrDefault(x => x.Id == result.Entity.Id);
    }

    public async Task ChangeAsync(Tarefa entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        _context.SaveChanges();;
    }

    public async Task DeleteAsync(Tarefa entity)
    {
        _context.Tarefas.Remove(entity);
        _context.SaveChanges();;
    }

    public async Task<IReadOnlyCollection<Tarefa>> GetAll()
    {
        return _context.Tarefas.ToList();
    }

    public async Task<IEnumerable<Tarefa>> GetByCheckStatus(bool status)
    {
        return _context.Tarefas.Where(x => x.Check == status).ToList();
    }

    public async Task<Tarefa> GetById(int id)
    {
        return _context.Tarefas.FirstOrDefault(x => x.Id == id);
        _context.SaveChanges();;
    }
}

