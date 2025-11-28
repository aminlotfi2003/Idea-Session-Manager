namespace ISM.WebApp.Options;

public class JwtCookieOptions
{
    public const string SectionName = "JwtCookie";

    public string CookieName { get; set; } = "ISM.Auth";
    public string CookiePath { get; set; } = "/";
    public bool Secure { get; set; } = true;
    public bool HttpOnly { get; set; } = true;
    public int ExpirationMinutes { get; set; } = 60;
}
