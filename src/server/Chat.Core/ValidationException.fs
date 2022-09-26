module Chat.Core.Exception

exception ValidationException

let raiseValidationException() =
    raise ValidationException |> ignore
