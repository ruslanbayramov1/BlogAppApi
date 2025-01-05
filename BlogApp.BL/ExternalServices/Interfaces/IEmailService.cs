namespace BlogApp.BL.ExternalServices.Interfaces;

public interface IEmailService
{
    Task SendEmailVerificationAsync(string email, string user, string code);
}
