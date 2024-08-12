using Microsoft.AspNetCore.Mvc;

namespace Library.WebApi.Controllers
{
    public class GenresController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
