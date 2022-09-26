namespace Chat.Core.Data

open System

type User =
    
    val PersonName : string
    
    new(iPv4: int, port: int) =
        { PersonName = String.Join(":", iPv4, port) }
        
    override this.ToString() =
        "User name: " + this.PersonName