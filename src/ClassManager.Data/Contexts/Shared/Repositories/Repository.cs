using System.Linq.Expressions;
using ClassManager.Domain.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClassManager.Data.Contexts.shared.Repositories;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
{

  protected readonly DbContext DbContext;
  protected readonly DbSet<TEntity> DbSet;

  protected Repository(DbContext dbContext)
  {
    DbContext = dbContext;
    DbSet = DbContext.Set<TEntity>();
  }
  public async virtual Task CreateAsync(TEntity entity, CancellationToken cancellationToken)
  {
    DbSet.Add(entity);
    await SaveChangesAsync(cancellationToken);
  }

  public async virtual Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken)
  {
    return await DbSet.ToListAsync(cancellationToken);
  }

  public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
  {
    return await DbSet.AsNoTracking().Where(predicate).ToListAsync(cancellationToken);
  }

  public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
  {
    return await DbSet.FindAsync(id, cancellationToken);
  }

  public async virtual Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
  {
    DbSet.Update(entity);
    await SaveChangesAsync(cancellationToken);
  }

  public virtual async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
  {
    var user = await DbSet.FindAsync(id, cancellationToken);
    if (user != null)
    {
      DbSet.Remove(user);
      await SaveChangesAsync(cancellationToken);
    }
  }

  public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
  {
    return await DbContext.SaveChangesAsync(cancellationToken);
  }
}