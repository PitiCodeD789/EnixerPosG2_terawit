using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EnixerPos.Web.Controllers
{
    public class ForgotPassController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}