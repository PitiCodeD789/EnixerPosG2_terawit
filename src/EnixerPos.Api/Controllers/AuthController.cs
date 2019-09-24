using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EnixerPos.Api.ViewModels.Auth;
using EnixerPos.Domain.DtoModels.Auth;
using EnixerPos.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EnixerPos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }

        //Login
        [HttpPost("login")]
        public IActionResult Login([FromBody]LoginCommand command)
        {
            try
            {
                string email = command.Email.ToLower();
                string password = command.Password;

                LoginDto loginDto = _authService.LoginMerchant(email, password);
                if (loginDto == null)
                {
                    return BadRequest();
                }

                string refreshToken = _authService.GetRefreshToken(email);
                LoginViewModel model = new LoginViewModel()
                {
                    RefreshToken = refreshToken,
                    StoreName = loginDto.StoreName,
                };

                return Ok(model);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : {0}", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        private string GetToken(string email, string user)
        {
            var claims = new Claim[]
            {
                new Claim("user",user)
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var tokendata = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                                                 email,
                                                 claims,
                                                 DateTime.Now,
                                                 expires: DateTime.Now.AddMinutes(5),
                                                 signingCredentials: credentials);
            var token = new JwtSecurityTokenHandler().WriteToken(tokendata);
            return token;
        }

        //LoginByPin
        [HttpPost("loginbypin")]
        public IActionResult LoginByPin([FromBody]LoginByPinCommand command)
        {
            try
            {
                string email = command.Email.ToLower();
                string pin = command.Pin;
                string refreshToken = command.RefreshToken;

                LoginByPinDto loginByPinDto = _authService.LoginUser(email, pin, refreshToken);
                if (loginByPinDto == null)
                {
                    return BadRequest();
                }

                string user = loginByPinDto.User;
                refreshToken = _authService.GetRefreshToken(email);
                string token = GetToken(email, user);
                LoginByPinViewModel model = new LoginByPinViewModel()
                {
                    RefreshToken = refreshToken,
                    Token = token,
                    User = user,
                    UserId = loginByPinDto.UserId
                };

                return Ok(model);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : {0}", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //GetTokenByRefreshMerchant
        [HttpPost("tokenmerchant")]
        public IActionResult GetTokenByRefreshMerchant([FromBody]GetTokenByRefreshMerchantCommand command)
        {
            try
            {
                string email = command.Email.ToLower();
                string refreshToken = command.RefreshToken;

                bool isRefreshToken = _authService.CheckRefresh(email, refreshToken);
                if (!isRefreshToken)
                {
                    return BadRequest();
                }

                refreshToken = _authService.GetRefreshToken(email);
                GetTokenByRefreshViewModel model = new GetTokenByRefreshViewModel()
                {
                    RefreshToken = refreshToken
                };

                return Ok(model);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : {0}", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //GetTokenByRefreshUser
        [Authorize]
        [AllowAnonymous]
        [HttpPost("tokenuser")]
        public IActionResult GetTokenByRefreshUser([FromBody]GetTokenByRefreshUserCommand command)
        {
            try
            {
                string email = User.Claims.SingleOrDefault(x => x.Type == "aud").Value;
                string refreshToken = command.RefreshToken;
                string user = command.User;

                bool isRefreshToken = _authService.CheckRefresh(email, refreshToken, user);
                if (!isRefreshToken)
                {
                    return BadRequest();
                }

                refreshToken = _authService.GetRefreshToken(email);
                string token = GetToken(email, user);
                GetTokenByRefreshViewModel model = new GetTokenByRefreshViewModel()
                {
                    RefreshToken = refreshToken,
                    Token = token
                };

                return Ok(model);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : {0}", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("logout")]
        [Authorize]
        [AllowAnonymous]
        public IActionResult Logout()
        {
            try
            {
                string email = User.Claims.SingleOrDefault(x => x.Type == "aud").Value;

                bool isLogout = _authService.Logout(email);
                if (!isLogout)
                {
                    return BadRequest();
                }
                return NoContent();
            }
            catch(Exception e)
            {
                Console.WriteLine("Error : {0}", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}