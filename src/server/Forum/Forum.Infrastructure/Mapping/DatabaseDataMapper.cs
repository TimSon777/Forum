using Broker.Contracts.Forum;
using Forum.DAL.Abstractions.Chat.Data;

namespace Forum.Infrastructure.Mapping;

public static class DatabaseDataMapper
{
    public static AddUserStorageItem ToAddUserStorageItem(this GetUserConsumerItem item)
    {
        Guard.Against.Null(item);

        return new AddUserStorageItem
        {
            Name = item.Name
        };
    }
}