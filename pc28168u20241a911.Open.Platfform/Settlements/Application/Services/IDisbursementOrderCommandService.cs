using pc28168u20241a911.Open.Platfform.Settlements.Application.Errors;
using pc28168u20241a911.Open.Platfform.Settlements.Domain.Model.Aggregates;
using pc28168u20241a911.Open.Platfform.Settlements.Domain.Model.Command;
using Acme.CatchUp.Platform.Shared.Application.Patterns;

namespace pc28168u20241a911.Open.Platfform.Settlements.Application.Services;

public interface IDisbursementOrderCommandService
{
    Task<Result<DisbursementOrder, CreateDisbursementError>> Handle(
        CreateDisbursementOrderCommand command,
        CancellationToken cancellationToken = default);
}