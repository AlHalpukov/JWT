namespace JWT.WebAPI.Providers;

public class JWTOptions
{ 
    public string SecretKey { get; set; }

    public int ExpiresHours { get; set; }
}