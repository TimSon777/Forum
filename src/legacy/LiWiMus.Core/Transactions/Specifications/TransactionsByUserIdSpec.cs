using Ardalis.Specification;

namespace LiWiMus.Core.Transactions.Specifications;

public sealed class TransactionsByUserIdSpec : Specification<Transaction>
{
    public TransactionsByUserIdSpec(int userId)
    {
        Query.Where(transaction => transaction.User.Id == userId);
    }
}