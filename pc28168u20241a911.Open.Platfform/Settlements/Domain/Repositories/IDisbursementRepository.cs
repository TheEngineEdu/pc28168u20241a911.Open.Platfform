using pc28168u20241a911.Open.Platfform.Settlements.Domain.Model.Aggregates;
using pc28168u20241a911.Open.Platfform.Settlements.Domain.Model.ValueObjects;
using Acme.CatchUp.Platform.Shared.Domain.Repositories;

namespace pc28168u20241a911.Open.Platfform.Settlements.Domain.Repositories;

public interface IDisbursementRepository : IBaseRepository<DisbursementOrder>
{
    Task<DisbursementOrder?> FindByIdAsync(
        DisbursementOrderId id,
        CancellationToken cancellationToken = default);

    Task<DisbursementOrder?> FindByOrderTrackingCodeAsync(
        OpenpayReference orderTrackingCode,
        CancellationToken cancellationToken = default);
}