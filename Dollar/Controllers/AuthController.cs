using Dollar.Interfaces.Services;
using MassTransit;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Models;
using SharedLibrary.ViewModels;

namespace Dollar.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        IRequestClient<RegistModel> _registClient;
        IRequestClient<LoginModel> _loginClient;
        public AuthController(IAuthService authService, IRequestClient<RegistModel> registClient, IRequestClient<LoginModel> loginClient)
        {
            _authService = authService;
            _registClient = registClient;
            _loginClient = loginClient;
        }
        public IActionResult Regist()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Regist(RegistModel data)
        {
            if (ModelState.IsValid)
            {
                var registResponse = await _registClient.GetResponse<AuthResponse>(data);

                if (!registResponse.Message.IsSuccessful)
                {
                    ViewBag.Error = registResponse.Message.Error;
                    return View();
                }
                return RedirectToAction("Login", "Auth");
            }
            else
            {
                ViewBag.Error = "Некорректные данные ! ";
                return View();
            }
        }

        public IActionResult Login(string? returnUrl)
        {
            if (returnUrl != null)
            {
                var options = new CookieOptions
                {
                    MaxAge = TimeSpan.FromMinutes(5),
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                };

                Response.Cookies.Append("returnUrl", returnUrl, options);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel data)
        {
            if (ModelState.IsValid)
            {
                var loginResponse = await _loginClient.GetResponse<AuthResponse>(data);

                if (!loginResponse.Message.IsSuccessful && loginResponse != null)
                {
                    ViewBag.Error = loginResponse.Message.Error;
                    return View();
                }

                await HttpContext.SignInAsync(_authService.GetPrincipal(loginResponse.Message.User));

                var returnUrl = Request.Cookies["returnUrl"];
                Response.Cookies.Delete("returnUrl");

                return Redirect(returnUrl ?? "/");
            }
            else
            {
                ViewBag.Error = "Некорректные данные ! ";
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
    }
}
