using SharedLibrary.Models;
using System.Security.Claims;

namespace Dollar.Interfaces.Services
{
    public interface IAuthService
    {
        ClaimsPrincipal GetPrincipal(User user);
    }
}
