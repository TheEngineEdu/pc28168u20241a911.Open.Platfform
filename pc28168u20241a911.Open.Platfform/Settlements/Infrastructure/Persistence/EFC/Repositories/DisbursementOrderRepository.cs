using pc28168u20241a911.Open.Platfform.Settlements.Domain.Model.Aggregates;
using pc28168u20241a911.Open.Platfform.Settlements.Domain.Model.ValueObjects;
using pc28168u20241a911.Open.Platfform.Settlements.Domain.Repositories;
using Acme.CatchUp.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Acme.CatchUp.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace pc28168u20241a911.Open.Platfform.Settlements.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class DisbursementOrderRepository(AppDbContext context)
    : BaseRepository<DisbursementOrder>(context), IDisbursementRepository
{
    public async Task<DisbursementOrder?> FindByIdAsync(
        DisbursementOrderId id,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(id);

        return await Context.Set<DisbursementOrder>()
            .FirstOrDefaultAsync(order => order.DisbursementOrderId == id, cancellationToken);
    }

    public async Task<DisbursementOrder?> FindByOrderTrackingCodeAsync(
        OpenpayReference orderTrackingCode,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(orderTrackingCode);

        return await Context.Set<DisbursementOrder>()
            .FirstOrDefaultAsync(order => order.OrderTrackingCode == orderTrackingCode, cancellationToken);
    }
}
