using System;
namespace ListaTarefaCRUD.Api.Interfaces.Repositories;

public interface IRepositoryBase<TEntity, TKey>
    where TEntity : class
    where TKey : struct
{
    Task<TEntity> AddAsync(TEntity entity);
    Task ChangeAsync(TEntity entityz);
    Task DeleteAsync(TEntity entity);
    Task<IReadOnlyCollection<TEntity>> GetAll();
    Task<TEntity> GetById(TKey id);
}

