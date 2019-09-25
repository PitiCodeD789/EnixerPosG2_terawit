using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnixerPos.Web.Controllers
{
    public class ManagementController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                string token = HttpContext.Session.GetString("Email");
                if (String.IsNullOrEmpty(token))
                {
                    return RedirectToAction("Index", "Register");
                }
                return View();
            }
            catch(Exception e)
            {
                Console.WriteLine("Error : " + e.Message);
                return RedirectToAction("Index", "Register");
            }
        }

        public IActionResult SuccessPage(string message)
        {
            ViewBag.Message = message;
            return View();
        }

        public IActionResult Error(string message)
        {
            ViewBag.Message = message;
            return View();
        }
    }
}