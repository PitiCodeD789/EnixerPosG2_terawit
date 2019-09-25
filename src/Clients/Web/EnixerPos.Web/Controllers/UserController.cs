using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EnixerPos.Domain.DtoModels.Auth;
using EnixerPos.Domain.Interfaces;
using EnixerPos.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnixerPos.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        public UserController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            try
            {
                string email = HttpContext.Session.GetString("Email");
                if (String.IsNullOrEmpty(email))
                {
                    return RedirectToAction("Index", "Register");
                }
                ViewBag.Email = email;
                List<StaffDto> staffDtos = _authService.GetUserInStore(email);
                List<StaffModel> staffs = staffDtos.Select(x => new StaffModel()
                {
                    Name = x.Name
                }).ToList();
                ViewData["StaffName"] = staffs;
                return View();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : " + e.Message);
                return RedirectToAction("Index", "Register");
            }
        }

        public IActionResult UserEdit(string nameUser)
        {
            try
            {
                string email = HttpContext.Session.GetString("Email");
                if (String.IsNullOrEmpty(email))
                {
                    return RedirectToAction("Index", "Register");
                }
                ViewBag.NameUser = nameUser;
                return View();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : " + e.Message);
                return RedirectToAction("Index", "Register");
            }
        }

        public IActionResult CallEditUser(string nameUser, string pin, string oldUser)
        {
            try
            {
                string email = HttpContext.Session.GetString("Email");
                if (String.IsNullOrEmpty(email))
                {
                    return RedirectToAction("Index", "Register");
                }

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

                EditUserInStoreDtoCommand command = new EditUserInStoreDtoCommand()
                {
                    Email = email,
                    OldUser = oldUser,
                    NameUser = nameUser,
                    Pin = pin
                };
                bool isRegisUser = _authService.EditUserInStore(command);
                if (!isRegisUser)
                {
                    return RedirectToAction("Error", "User", new { message = "ไม่สามารถแก้ไขพนักงานได้" });
                }
                return RedirectToAction("SuccessPage", "User", new { message = "แก้ไขพนักงานสำเร็จ" });
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : " + e.Message);
                return RedirectToAction("Index", "Register");
            }
        }

        public IActionResult UserDelete(string nameUser)
        {
            try
            {
                string email = HttpContext.Session.GetString("Email");
                if (String.IsNullOrEmpty(email))
                {
                    return RedirectToAction("Index", "Register");
                }

                email = email.ToLower();
                bool isEmail = _authService.ChechEmail(email);
                if (!isEmail)
                {
                    return RedirectToAction("Error", "Management", new { message = "E-Mail ไม่มีอยู่ในระบบโปรดลงทะเบียนใหม่" });
                }

                CheckNameUserDtoCommand checkNameUser = new CheckNameUserDtoCommand()
                {
                    Email = email,
                    NameUser = nameUser
                };
                bool isName = _authService.DeleteNameUser(checkNameUser);
                if (!isName)
                {
                    return RedirectToAction("Error", "User", new { message = "ไม่สามารถลบพนักงานได้" });
                }
                return RedirectToAction("SuccessPage", "User", new { message = "ลบพนักงานสำเร็จ" });
            }
            catch (Exception e)
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