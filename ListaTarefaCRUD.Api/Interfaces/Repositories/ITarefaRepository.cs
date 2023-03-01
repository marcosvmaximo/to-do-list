using System;
using ListaTarefaCRUD.Api.Models;

namespace ListaTarefaCRUD.Api.Interfaces.Repositories;

public interface ITarefaRepository : IRepositoryBase<Tarefa, int>
{
    Task<IEnumerable<Tarefa>> GetByCheckStatus(bool status);
}

