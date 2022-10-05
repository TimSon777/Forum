﻿using Chat.DAL.Abstractions.Chat.Data;
using Chat.Infrastructure.MessageHandlers.Data;

namespace Chat.Infrastructure.Mapping;

public static class DatabaseDataMapper
{
    public static AddMessageStorageItem ToAddMessageStorageItem(this GetMessageConsumerItem item)
    {
        Guard.Against.Null(item);

        return new AddMessageStorageItem
        {
            Text = item.Text,
            User = item.User.ToAddUserStorageItem()
        };
    }

    public static AddUserStorageItem ToAddUserStorageItem(this GetUserConsumerItem item)
    {
        Guard.Against.Null(item);

        return new AddUserStorageItem
        {
            Name = item.Name
        };
    }
}