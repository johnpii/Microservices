using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers
{
    public class AuthController : Controller
    {

        public IActionResult Index()
        {
            return Ok();
        }
    }
}
