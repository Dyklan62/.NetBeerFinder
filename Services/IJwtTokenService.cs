namespace JwtAuthentification.server.Interface
{
    public interface IJwtTokenService
    {
        string BuildToken(string email);
    }
}