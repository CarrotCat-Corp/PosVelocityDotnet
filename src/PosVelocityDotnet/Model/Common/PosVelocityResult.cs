
using PosVelocityDotnet.Model.Error;

namespace PosVelocityDotnet.Model.Common;

/// <summary>
/// PosVelocityResult is a very basic result pattern implementation designed to encapsulate the result of an operation within
/// the PosVelocity semi-integration client. It indicates whether the operation was successful or
/// encountered an error.
/// </summary>
/// <typeparam name="TValue">The type of the value returned if the operation was successful.</typeparam>
public readonly struct PosVelocityResult<TValue>
{
    public bool IsError { get; }
    public bool IsSuccess => !IsError;
    public TransactionError? Error { get; }
    public TValue? Value { get; }

    private PosVelocityResult(TValue value)
    {
        IsError = false;
        Value = value;
        Error = default;
    }

    private PosVelocityResult(TransactionError error)
    {
        IsError = true;
        Value = default;
        Error = error;
    }

    public static implicit operator PosVelocityResult<TValue>(TValue value) => new(value);

    public static implicit operator PosVelocityResult<TValue>(TransactionError error) => new(error);

    public TResult Match<TResult>(Func<TValue, TResult> success, Func<TransactionError,TResult> failure)
        => !IsError?success(Value!) : failure(Error!);

}