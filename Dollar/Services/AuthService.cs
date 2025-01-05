using Dollar.Interfaces.Services;
using SharedLibrary.Models;
using System.Security.Claims;

namespace Dollar.Services
{
    public class AuthService : IAuthService
    {
        public ClaimsPrincipal GetPrincipal(User user)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Username),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
                };
            var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            return claimsPrincipal;
        }
    }
}
