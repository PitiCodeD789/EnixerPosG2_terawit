using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EnixerPos.Api.ViewModels.Helpers;
using EnixerPos.Domain.DtoModels.Auth;
using EnixerPos.Domain.Interfaces;
using EnixerPos.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

            if (!CheckEWalletAccNo(eWalletAccNo))
            {
                return RedirectToAction("Error", "Register", new { message = "Account Number ของ Application EnixerWallet ไม่มีอยู่ในระบบ" });
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

        private bool CheckEWalletAccNo(string eWalletAccNo)
        {
            string url = $"http://192.168.1.18:20000/api/user/userbyaccno/{eWalletAccNo}";
            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(20);
            var result = client.GetAsync(url).Result;
            if (result.IsSuccessStatusCode)
            {
                var json_result = result.Content.ReadAsStringAsync().Result;
                MerchantbyAccountViewModel checkAcc = JsonConvert.DeserializeObject<MerchantbyAccountViewModel>(json_result);
                return checkAcc.CheckMerchant;
            }
            else
            {
                return false;
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
                return RedirectToAction("Error", "Register", new { message = "ลงทะเบียนไม่สำเร็จ" });
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

            CheckPinDtoCommand checkPinDto = new CheckPinDtoCommand()
            {
                Email = email,
                Pin = pin
            };
            bool isPin = _authService.CheckPin(checkPinDto);
            if (isPin)
            {
                return RedirectToAction("Error", "Management", new { message = "Pin นี้ถูกใช้งานในร้านค้านี้เรียบร้อย" });
            }

            CheckNameUserDtoCommand checkNameUser = new CheckNameUserDtoCommand()
            {
                Email = email,
                NameUser = nameUser
            };
            bool isName = _authService.ChechNameUser(checkNameUser);
            if (isName)
            {
                return RedirectToAction("Error", "Management", new { message = "ชื่อพนักงานคนนี้ถูกใช้งานในร้านค้านี้เรียบร้อย" });
            }

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
            return RedirectToAction("SuccessPage", "Management", new { message = "เพิ่มพนักงานสำเร็จ" });
        }
    }
}