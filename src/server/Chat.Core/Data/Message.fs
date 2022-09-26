namespace Chat.Core.Data

open System
open Chat.Core.Exception

type Message(user: User, text: string) =
    
    do if text.Length > 500 then raiseValidationException()
    
    member val User = user
    member val Text = text
    
    override this.ToString() =
        this.User.ToString()
            + Environment.NewLine
            + "Message: " + this.Text
