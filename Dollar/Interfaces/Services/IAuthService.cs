using RestSharp;
using SharedLibrary.Models;
using SharedLibrary.ViewModels;
using System.Security.Claims;

namespace Dollar.Interfaces.Services
{
    public interface IAuthService
    {
        RestResponse Regist(RegistModel data);
        RestResponse Login(LoginModel data);

        ClaimsPrincipal GetPrincipal(User user);
    }
}
