namespace FinancialKrisis.Application.DTOs;

public readonly struct Optional<T>
{
    public bool IsDefined { get; }
    public T? Value => IsDefined
        ? field
        : throw new InvalidOperationException("Optional value is not defined.");

    private Optional(T? pValue, bool pIsDefined)
    {
        Value = pValue;
        IsDefined = pIsDefined;
    }

    public static Optional<T> Undefined()
    {
        return new Optional<T>(default, false);
    }

    public static Optional<T> From(T? pValue)
    {
        return new Optional<T>(pValue, true);
    }

    public static Optional<T> Remove()
    {
        return new Optional<T>(default, true);
    }

    public static implicit operator Optional<T>(T? pValue)
    {
        return From(pValue);
    }
}
