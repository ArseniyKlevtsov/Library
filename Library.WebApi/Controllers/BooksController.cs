using Microsoft.AspNetCore.Mvc;

namespace Library.WebApi.Controllers
{
    public class BooksController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
