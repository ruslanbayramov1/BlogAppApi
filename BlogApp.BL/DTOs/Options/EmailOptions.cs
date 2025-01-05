namespace BlogApp.BL.DTOs.Options;

public class EmailOptions
{
    public const string location = "EmailOptions";
    public string Host { get; set; }
    public int Port { get; set; }
    public string Sender { get; set; }
    public string Password { get; set; }
}
