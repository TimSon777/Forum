module Chat.Core.Exception

exception ValidationException

let raiseValidationException() =
    ValidationException |> raise |> ignore
