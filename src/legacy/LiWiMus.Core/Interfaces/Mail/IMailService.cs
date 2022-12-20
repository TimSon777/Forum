using Refit;

namespace LiWiMus.Core.Interfaces.Mail;

public interface IMailService
{
    [Post("/mail/resetPassword")]
    Task<IApiResponse> SendMailToResetPasswordAsync([Body] ResetPasswordRequest request);
    
    [Post("/mail/confirmAccount")]
    Task<IApiResponse> SendMailToConfirmAccountAsync([Body] ConfirmAccountRequest request);
}