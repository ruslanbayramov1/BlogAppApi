namespace BlogApp.BL.DTOs.Options;

public class JwtOptions
{
    public const string position = "JwtOptions";
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string SecretKey { get; set; }
}
