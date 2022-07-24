using Microsoft.EntityFrameworkCore;
using NfcPos.Domain.Entities;

namespace NfcPos.Application.Common.Interfaces;

public interface IApplicationDbContext
{

    DbSet<User> Users { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
