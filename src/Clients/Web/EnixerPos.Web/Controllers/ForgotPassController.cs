using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnixerPos.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EnixerPos.Web.Controllers
{
    public class ForgotPassController : Controller
    {
        private readonly IAuthService _authService;

        public ForgotPassController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CallApiForgotPass(string email)
        {
            email = email.ToLower();
            bool isEmail = _authService.ChechEmail(email);
            if (!isEmail)
            {
                return RedirectToAction("Error", "Register", new { message = "E-Mail ไม่มีอยู่ในระบบโปรดลงทะเบียนใหม่" });
            }

            _authService.ForgotPassword(email);

            return RedirectToAction("SuccessPage", "Register", new { message = "โปรดทำการตั้งรหัสผ่านจาก Link ที่ส่งให้ถ้า E-Mail" });
        }
    }
}