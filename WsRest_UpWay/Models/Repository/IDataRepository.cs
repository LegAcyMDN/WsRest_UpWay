﻿using Microsoft.AspNetCore.Mvc;

namespace WsRest_UpWay.Models.Repository;

public interface IDataRepository<TEntity>
{
    Task<ActionResult<IEnumerable<TEntity>>> GetAllAsync(int page = 0);
    Task<ActionResult<int>> GetCountAsync();
    Task<ActionResult<TEntity>> GetByIdAsync(int id);
    Task<ActionResult<TEntity>> GetByStringAsync(string str);
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entityToUpdate, TEntity entity);
    Task DeleteAsync(TEntity entity);
}