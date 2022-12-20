using Ardalis.GuardClauses;
using LiWiMus.Core.Exceptions;
using LiWiMus.Core.Payments;
using LiWiMus.Core.Transactions;
using LiWiMus.Core.Users;
using LiWiMus.SharedKernel.Extensions;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Infrastructure.Services;

public class PaymentService : IPaymentService
{
    private readonly IRepository<Transaction> _transactionRepository;

    public PaymentService(IRepository<Transaction> transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task PayAsync(User user, CardInfo card, decimal amount, string? reason = null)
    {
        var transaction = new Transaction
        {
            User = Guard.Against.Null(user, nameof(user)),
            Amount = Guard.Against.Zero(amount, nameof(amount)),
            Description = reason ?? "Not specified",
        };

        if (Random.Shared.TryProbability(25))
        {
            throw new PaymentException();
        }

        await _transactionRepository.AddAsync(transaction);
    }
}