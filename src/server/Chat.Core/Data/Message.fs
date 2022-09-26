namespace Chat.Core.Data

open System

type Message(User: User, Text: string) =
    override this.ToString() =
        User.ToString()
            + Environment.NewLine
            + "Message: " + Text
