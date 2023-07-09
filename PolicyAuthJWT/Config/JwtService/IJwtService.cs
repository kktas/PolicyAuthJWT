namespace PolicyAuthJWT.Config.Auth.JwtService
{
    public interface IJwtService
    {
        string GenerateToken(string userId, string username, string[] roles, bool keepLoggedIn);
    }
}
