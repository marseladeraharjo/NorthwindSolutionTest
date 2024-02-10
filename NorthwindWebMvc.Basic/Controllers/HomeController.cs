using Microsoft.AspNetCore.Mvc;
using NorthwindWebMvc.Basic.Models;
using NorthwindWebMvc.Basic.Models.Dto;
using System.Diagnostics;

namespace NorthwindWebMvc.Basic.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //viewData
            ViewData["Message"] = "Welcome to bootcamp";
            ViewData["Categories"] = new CategoryDto()
            {
                CategoryName = "Fruit",
                Description = "Buah segar"
            };

            ViewBag.MyCategory = new CategoryDto()
            {
                CategoryName = "Song",
                Description = "Bollywood Songs"
            };

            return View();
        }

        public IActionResult MyPage()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}