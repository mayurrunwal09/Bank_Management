using Domain_Library.Models;

namespace Web_Library.Middleware.Auth
{
    public interface IJWTAuthManager
    {
        string GenerateJWT(User user);
    }
}
