using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json.Serialization;
using WebApplicationEmailService.Data;
using WebApplicationEmailService.Models;

namespace WebApplicationEmailService.Controllers
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
            return View();
        }

        public IActionResult LogIn()
        {
            int userId = 0;
            string name = Request.Form["name_"];
            User user = new User() { UserName = name };
            EmailService service = new EmailService();
            var users = service.GetAllUsers().Where(x => x.UserName == name).ToList();
            if (users.Count() == 0)
            {
                userId = service.Add(user);               
            }
            else
            {
                userId = users.FirstOrDefault().Id;
            }
            var h = service.GetAllMessagesByUserId(userId).ToList();

            HttpContext.Session.SetString("userId", userId.ToString());

            return RedirectToAction("Index", "Email", new { messages = JsonConvert.SerializeObject(h) });
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}