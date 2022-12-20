namespace LiWiMus.Core.Payments;

public interface IPaymentService
{
    Task PayAsync(User user, CardInfo card, decimal amount, string? reason = null);
}