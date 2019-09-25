using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnixerPos.Domain.DtoModels.Auth;
using EnixerPos.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EnixerPos.Web.Controllers
{
    public class SetPasswordController : Controller
    {
        private readonly IAuthService _authService;

        public SetPasswordController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Index(string otp, string email)
        {
            ViewBag.OTP = otp;
            ViewBag.Email = email;
            return View();
        }

        public IActionResult CallApiSetPass(string otp, string email, string password)
        {
            email = email.ToLower();
            bool isEmail = _authService.ChechEmail(email);
            if (!isEmail)
            {
                return RedirectToAction("Error(", "Register", new { message = "E-Mail นี้ไม่มีในระบบ" });
            }
            SetPasswordDtoCommand command = new SetPasswordDtoCommand()
            {
                OTP = otp,
                Email = email,
                Password = password
            };
            bool isSetPass = _authService.SetPassword(command);
            if (!isSetPass)
            {
                return RedirectToAction("Error(", "Register", new { message = "ไม่สามารถตั้งรหัสได้โปรดกด Forgot password?\nเพื่อรับ Link ตั้งรหัสผ่านใหม่ใน Email" });
            }

            return RedirectToAction("SuccessPage", "Register", new { message = "ตั้งรหัสสำเร็จเรียบร้อย" });
        }
    }
}