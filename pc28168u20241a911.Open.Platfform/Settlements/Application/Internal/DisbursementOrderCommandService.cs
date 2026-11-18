using Acme.CatchUp.Platform.Shared.Application.Patterns;
using Acme.CatchUp.Platform.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using pc28168u20241a911.Open.Platfform.Settlements.Application.Errors;
using pc28168u20241a911.Open.Platfform.Settlements.Application.Services;
using pc28168u20241a911.Open.Platfform.Settlements.Domain.Model.Aggregates;
using pc28168u20241a911.Open.Platfform.Settlements.Domain.Model.Command;
using pc28168u20241a911.Open.Platfform.Settlements.Domain.Repositories;

namespace pc28168u20241a911.Open.Platfform.Settlements.Application.Internal.CommandServices;

public class DisbursementOrderCommandService(
    IDisbursementRepository disbursementRepository,
    IUnitOfWork unitOfWork,
    ILogger<DisbursementOrderCommandService> logger)
    : IDisbursementOrderCommandService
{
    public async Task<Result<DisbursementOrder, CreateDisbursementError>> Handle(
        CreateDisbursementOrderCommand command,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command);

        if (command.Id is null || command.TransferCap is null || command.OrderTrackingCode is null)
            return new Result<DisbursementOrder, CreateDisbursementError>.Failure(
                CreateDisbursementError.InvalidDisbursementOrder);

        if (!Enum.IsDefined(command.Status))
            return new Result<DisbursementOrder, CreateDisbursementError>.Failure(
                CreateDisbursementError.InvalidStatus);

        try
        {
            var existingOrder = await disbursementRepository.FindByIdAsync(command.Id, cancellationToken);
            if (existingOrder is not null)
            {
                logger.LogWarning("Duplicate disbursement order rejected.");
                return new Result<DisbursementOrder, CreateDisbursementError>.Failure(
                    CreateDisbursementError.DuplicateDisbursementOrder);
            }

            var disbursementOrder = new DisbursementOrder(command);
            await disbursementRepository.AddAsync(disbursementOrder, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);

            return new Result<DisbursementOrder, CreateDisbursementError>.Success(disbursementOrder);
        }
        catch (ArgumentException exception)
        {
            logger.LogWarning(exception, "Invalid data while creating a disbursement order.");
            return new Result<DisbursementOrder, CreateDisbursementError>.Failure(
                CreateDisbursementError.InvalidDisbursementOrder);
        }
        catch (DbUpdateException exception) when (IsDuplicateKeyViolation(exception))
        {
            logger.LogWarning(exception, "Duplicate disbursement order rejected by the database.");
            return new Result<DisbursementOrder, CreateDisbursementError>.Failure(
                CreateDisbursementError.DuplicateDisbursementOrder);
        }
        catch (DbUpdateException exception)
        {
            logger.LogError(exception, "Database update failed while creating a disbursement order.");
            return new Result<DisbursementOrder, CreateDisbursementError>.Failure(
                CreateDisbursementError.UnexpectedError);
        }
    }

    private static bool IsDuplicateKeyViolation(DbUpdateException exception)
    {
        for (Exception? current = exception; current is not null; current = current.InnerException)
        {
            if (!string.Equals(current.GetType().Name, "MySqlException", StringComparison.Ordinal))
                continue;

            var numberProperty = current.GetType().GetProperty("Number");
            if (numberProperty?.PropertyType == typeof(int) &&
                numberProperty.GetValue(current) is int errorCode &&
                errorCode == 1062)
                return true;
        }

        return false;
    }
}
