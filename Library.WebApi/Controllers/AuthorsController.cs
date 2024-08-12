using Microsoft.AspNetCore.Mvc;

namespace Library.WebApi.Controllers
{
    public class AuthorsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
