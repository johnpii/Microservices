using Dollar.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Models;
using System.Diagnostics;

namespace Dollar.Controllers
{
    public class DollarController : Controller
    {
        private readonly IDollarService _dollarService;

        public DollarController(IDollarService dollarService)
        {
            _dollarService = dollarService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _dollarService.GetDollarExchangeRate());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
