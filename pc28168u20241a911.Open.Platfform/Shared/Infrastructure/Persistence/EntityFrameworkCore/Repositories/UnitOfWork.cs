using Acme.CatchUp.Platform.Shared.Domain.Repositories;
using Acme.CatchUp.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

namespace Acme.CatchUp.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task CompleteAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}