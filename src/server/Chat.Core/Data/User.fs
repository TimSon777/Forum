namespace Chat.Core.Data

open System
open Chat.Core.Exception

type User =
    
    val Name : string
    
    new(iPv4: int, port: int) =
        if iPv4 < 0 || port < 0 || port > 65535 then
            raiseValidationException()
        { Name = String.Join(":", iPv4, port) }
        
    override this.ToString() =
        "User name: " + this.Name