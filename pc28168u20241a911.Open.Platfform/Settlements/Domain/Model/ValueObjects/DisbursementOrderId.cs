namespace pc28168u20241a911.Open.Platfform.Settlements.Domain.Model.ValueObjects;

public sealed record DisbursementOrderId
{
    private const int MaxLength = 255;

    public DisbursementOrderId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("DisbursementOrderId cannot be null, empty, or whitespace.", nameof(value));

        if (value.Length > MaxLength)
            throw new ArgumentException($"DisbursementOrderId cannot be longer than {MaxLength} characters.", nameof(value));

        Value = value;
    }

    public string Value { get; }

    public override string ToString() => Value;
}


