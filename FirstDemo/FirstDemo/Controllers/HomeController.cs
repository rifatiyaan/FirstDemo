using FirstDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FirstDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVideoConverter _videoConverter;

        public HomeController(ILogger<HomeController> logger,IVideoConverter videoConverter)
        {
            _logger = logger;
            _videoConverter = videoConverter;

        }

        public IActionResult Index()
        {
            _logger.LogInformation("I am in index page");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Test()
        {
            var model = new TestModel();
            model.Email= "mdbrifat@gmail.com";
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Test(TestModel model)
        {
            if(ModelState.IsValid)
            {

            }
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}