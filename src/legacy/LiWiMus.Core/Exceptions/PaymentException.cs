namespace LiWiMus.Core.Exceptions;

public class PaymentException : Exception
{
    public PaymentException(string? message = null) : base(message)
    {
    }
}