using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnixerPos.Api.ViewModels.Helpers;
using EnixerPos.Domain.DtoModels.Auth;
using EnixerPos.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
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
            try
            {
                string email = HttpContext.Session.GetString("Email");
                if (String.IsNullOrEmpty(email))
                {
                    return RedirectToAction("Index", "Register");
                }
                ViewBag.Email = email;
                return View();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : " + e.Message);
                return RedirectToAction("Index", "Register");
            }
        }

        public IActionResult Login()
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
                return RedirectToAction("Error", "Register", new { message = "E-Mail นี้ถูกใช้งานแล้ว" });
            }
            bool isStroe = _authService.CheckStore(storeName);
            if (isStroe)
            {
                return RedirectToAction("Error", "Register", new { message = "ชื่อร้านนี้ถูกใช้งานแล้ว" });
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
                return RedirectToAction("Error", "Register", new { message = "ไม่สามารถลงทะเบียนได้" });
            }
        }
        public IActionResult CallApiLogin(string email, string password)
        {
            email = email.ToLower();
            bool isEmail = _authService.ChechEmail(email);
            if (!isEmail)
            {
                return RedirectToAction("Error", "Register", new { message = "E-Mail ไม่มีอยู่ในระบบโปรดลงทะเบียนใหม่" });
            }

            var loginData = _authService.LoginMerchant(email, password);
            if(loginData == null)
            {
                return RedirectToAction("Error", "Register", new { message = "E-Mail ไม่มีอยู่ในระบบโปรดลงทะเบียนใหม่" });
            }
            HttpContext.Session.SetString("Email", email);
            return RedirectToAction("Index", "Management");
        }

        public IActionResult CallAPIRegisUser(string email, string nameUser, string pin)
        {
            email = email.ToLower();
            bool isEmail = _authService.ChechEmail(email);
            if (!isEmail)
            {
                return RedirectToAction("Error", "Management", new { message = "E-Mail ไม่มีอยู่ในระบบโปรดลงทะเบียนใหม่" });
            }


            //bool isPin = _authService.CheckPin(email, pin);
            //if (isPin)
            //{
            //    return RedirectToAction("Error", "Management", new { message = "Pin นี้ถูกใช้งานในร้านค้านี้เรียบร้อย" });
            //}

            RegisterUserInStoreDtoCommand command = new RegisterUserInStoreDtoCommand()
            {
                Email = email,
                NameUser = nameUser,
                Pin = pin
            };
            bool isRegisUser = _authService.RegisterUserInStore(command);
            if (!isRegisUser)
            {
                return RedirectToAction("Error", "Management", new { message = "ไม่สามารถเพิ่มพนักงานได้" });
            }
            return RedirectToAction("SuccessPage", "Management", new { message = "ไม่สามารถเพิ่มพนักงานได้" });
        }
    }
}