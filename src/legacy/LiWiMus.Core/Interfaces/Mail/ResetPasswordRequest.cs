namespace LiWiMus.Core.Interfaces.Mail;

public record ResetPasswordRequest(string UserName, string UserEmail, string ResetUrl);