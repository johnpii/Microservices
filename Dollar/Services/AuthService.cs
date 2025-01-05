using Dollar.Interfaces.Services;
using RestSharp;
using SharedLibrary.Helpers;
using SharedLibrary.Models;
using SharedLibrary.ViewModels;
using System.Security.Claims;

namespace Dollar.Services
{
    public class AuthService : IAuthService
    {
        readonly RestClient _client;
        public AuthService()
        {
            RestClientOptions options = new RestClientOptions(ConfigurationHelper.config.GetSection("BaseURL").Value)
            {
                UseDefaultCredentials = true
            };
            _client = new RestClient(options);
        }

        public RestResponse Regist(RegistModel data)
        {
            var request = new RestRequest("Auth/Regist")
                    .AddBody(data);

            return _client.ExecutePost(request);
        }

        public RestResponse Login(LoginModel data)
        {
            var request = new RestRequest("Auth/Login")
                    .AddBody(data);

            return _client.ExecutePost(request);
        }

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
