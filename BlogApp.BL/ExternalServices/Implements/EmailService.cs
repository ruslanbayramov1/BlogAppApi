using BlogApp.BL.Constants;
using BlogApp.BL.DTOs.Options;
using BlogApp.BL.ExternalServices.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace BlogApp.BL.ExternalServices.Implements;

public class EmailService : IEmailService
{
    private readonly SmtpClient _smtpClient;
    private readonly MailAddress _from;
    private readonly HttpContext _httpContext;
    public EmailService(IOptions<EmailOptions> options, IHttpContextAccessor httpContext)
    {
        EmailOptions opt = options.Value;
        _from = new MailAddress(opt.Sender, "Ruslan Bayramov");

        _smtpClient = new SmtpClient(opt.Host, opt.Port);
        _smtpClient.EnableSsl = true;
        _smtpClient.Credentials = new NetworkCredential(opt.Sender, opt.Password);

        _httpContext = httpContext.HttpContext!;
    }

    public async Task SendEmailVerificationAsync(string email, string user, string code)
    {
        MailAddress to = new MailAddress(email);
        
        MailMessage message = new MailMessage(_from, to);
        message.Subject = "Email Verification";
        message.IsBodyHtml = true;

        string url = _httpContext.Request.Scheme + "://" + _httpContext.Request.Host + "/api/Users/VerifyEmail" + $"?user={user}&code={code}";
        message.Body = EmailTemplates.ConfirmTemplate.Replace("__$appName", "Blog App").Replace("__$verifyLink", url).Replace("__$userName", user);

        await _smtpClient.SendMailAsync(message);
    }
}
