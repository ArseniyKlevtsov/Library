﻿using Microsoft.AspNetCore.Mvc;

namespace Library.WebApi.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
