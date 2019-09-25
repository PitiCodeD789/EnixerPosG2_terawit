using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnixerPos.Domain.DtoModels.Auth;
using EnixerPos.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EnixerPos.Web.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IAuthService _authService;

        public RegisterController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RegisStore()
        {
            return View();
        }

        public IActionResult RegisUser()
        {
            return View();
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

        public IActionResult CallAPIRegisStoer(string email, string storeName, string eWalletAccNo)
        {
            email = email.ToLower();
            bool isEmail = _authService.ChechEmail(email);
            if (isEmail)
            {
                return RedirectToAction("Error(", "Register", new { message = "E-Mail นี้ถูกใช้งานแล้ว" });
            }
            bool isStroe = _authService.CheckStore(storeName);
            if (isStroe)
            {
                return RedirectToAction("Error(", "Register", new { message = "ชื่อร้านนี้ถูกใช้งานแล้ว" });
            }

            RegisterStoreDtoCommand command = new RegisterStoreDtoCommand()
            {
                Email = email,
                StoreName = storeName,
                EWalletAccNo = eWalletAccNo
            };

            bool isRegis = _authService.RegisterStore(command);
            if (isRegis)
            {
                return RedirectToAction("SuccessPage", "Register", new { message = "ลงทะเบียนสำเร็จแล้ว\nโปรดทำการตั้งรหัสผ่านจาก Link ที่ส่งให้ถ้า E-Mail" });
            }
            else
            {
                return RedirectToAction("Error(", "Register", new { message = "ไม่สามารถลงทะเบียนได้" });
            }
        }

        public IActionResult CallAPIRegisUser(string nameUser, string pin)
        {
            return RedirectToAction("SuccessPage", "Register", "OK");
        }
    }
}