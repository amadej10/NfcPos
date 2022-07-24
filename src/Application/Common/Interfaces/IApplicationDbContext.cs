using Microsoft.EntityFrameworkCore;

namespace NfcPos.Application.Common.Interfaces;

public interface IApplicationDbContext
{

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
