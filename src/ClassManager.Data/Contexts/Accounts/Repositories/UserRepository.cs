using ClassManager.Data.Contexts.shared.Repositories;
using ClassManager.Data.Data;
using ClassManager.Domain.Contexts.Accounts.Entities;
using ClassManager.Domain.Contexts.Accounts.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ClassManager.Data.Contexts.Accounts.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
  public UserRepository(AppDbContext context) : base(context) { }

  public async Task<bool> DocumentAlreadyExistsAsync(string document, CancellationToken cancellationToken)
  {
    return await DbSet.AsNoTracking().AnyAsync(x => x.Document.Number == document, cancellationToken);
  }

  public async Task<bool> EmailAlreadyExtstsAsync(string email, CancellationToken cancellationToken)
  {
    return await DbSet.AsNoTracking().AnyAsync(x => x.Email.Address == email.ToLower(), cancellationToken);
  }

  public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
  {
    return await DbSet
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.Email.Address == email, cancellationToken);
  }

  public async Task<User?> VerifyUserRoleAsync(Guid userId, Guid tenantId, string roleName, CancellationToken cancellationToken)
  {
    return await DbSet
    .Include(u => u.UsersRoles.Where(ur => ur.Role.Name == roleName && ur.TenantId == tenantId))
    .AsNoTracking()
    .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
  }
}
