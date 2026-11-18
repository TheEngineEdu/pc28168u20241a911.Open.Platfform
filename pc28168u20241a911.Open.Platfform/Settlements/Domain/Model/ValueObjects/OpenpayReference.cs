namespace pc28168u20241a911.Open.Platfform.Settlements.Domain.Model.ValueObjects;

public sealed record OpenpayReference
{
    private const int MaxLength = 255;

    public OpenpayReference(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("OpenpayReference cannot be null, empty, or whitespace.", nameof(value));

        if (value.Length > MaxLength)
            throw new ArgumentException($"OpenpayReference cannot be longer than {MaxLength} characters.", nameof(value));

        Value = value;
    }

    public string Value { get; }

    public override string ToString() => Value;
}