using pc28168u20241a911.Open.Platfform.Settlements.Domain.Model.Command;
using pc28168u20241a911.Open.Platfform.Settlements.Domain.Model.ValueObjects;

namespace pc28168u20241a911.Open.Platfform.Settlements.Domain.Model.Aggregates;

public partial class DisbursementOrder
{
    protected DisbursementOrder()
    {
        TransferCap = null!;
        DisbursementOrderId = null!;
        OrderTrackingCode = null!;
    }

    public DisbursementOrder(CreateDisbursementOrderCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        TransferCap = command.TransferCap;
        DisbursementOrderId = command.Id;
        OrderTrackingCode = command.OrderTrackingCode;
        Status = command.Status;
    }

    public int Id { get; private set; }

    public DisbursementOrderId DisbursementOrderId { get; private set; }

    public FinancialAmount TransferCap { get; private set; }

    public OpenpayReference OrderTrackingCode { get; private set; }

    public DisbursementStatus Status { get; private set; }
}
