using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Service;

namespace Web.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsApiClient _newsApiClient;

        public NewsController(INewsApiClient newsApiClient)
        {
            _newsApiClient = newsApiClient;
        }

        public async Task<IActionResult> Index()
        {
            var session = HttpContext.Session.GetString("Token");
            if(session == null)
            {
                return RedirectToAction("Login", "User");
            }
            var data = await _newsApiClient.GetAll(session);
            
            return View(data);
        }
    }
}