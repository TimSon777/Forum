namespace LiWiMus.Core.Interfaces.Mail;

public record ConfirmAccountRequest(string UserName, string UserEmail, string ConfirmUrl);