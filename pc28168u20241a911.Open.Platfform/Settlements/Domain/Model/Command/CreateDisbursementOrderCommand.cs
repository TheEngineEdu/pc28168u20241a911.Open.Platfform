using pc28168u20241a911.Open.Platfform.Settlements.Domain.Model.ValueObjects;

namespace pc28168u20241a911.Open.Platfform.Settlements.Domain.Model.Command;

public record CreateDisbursementOrderCommand(
    FinancialAmount TransferCap,
    DisbursementOrderId Id,
    OpenpayReference OrderTrackingCode,
    DisbursementStatus Status);
    
