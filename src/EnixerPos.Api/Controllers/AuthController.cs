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
            string email = command.Email;
            string password = command.Password;
            string imei = command.Imei;

            LoginDto loginDto = _authService.LoginMerchant(email, password, imei);
            if (loginDto == null)
            {
                return BadRequest();
            }

            string refreshToken = _authService.GetRefreshToken(email, imei);
            string token = GetToken(email, imei);
            LoginViewModel model = new LoginViewModel()
            {
                RefreshToken = refreshToken,
                Token = token,
                MerchantName = loginDto.StoreName,
                PosName = loginDto.PosName
            };

            return Ok(model);
        }

        private string GetToken(string email, string imei)
        {
            var claims = new Claim[]
            {
                new Claim("imei",imei)
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

        private string GetToken(string email, string imei, string user)
        {
            var claims = new Claim[]
            {
                new Claim("imei",imei),
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
            string email = "";
            string imei = "";
            string pin = command.Pin;

            LoginByPinDto loginByPinDto = _authService.LoginUser(email, imei, pin);
            if(loginByPinDto == null)
            {
                return BadRequest();
            }

            string user = loginByPinDto.User;
            string refreshToken = _authService.GetRefreshToken(email, imei);
            string token = GetToken(email, imei, user);
            LoginByPinViewModel model = new LoginByPinViewModel()
            {
                RefreshToken = refreshToken,
                Token = token,
                User = user,
                UserId = loginByPinDto.UserId
            };

            return Ok(model);
        }

        //GetTokenByRefreshMerchant
        [HttpPost("tokenmerchant")]
        public IActionResult GetTokenByRefreshMerchant([FromBody]GetTokenByRefreshMerchantCommand command)
        {
            string email = command.Email;
            string imei = command.Imei;
            string refreshToken = command.RefreshToken;

            bool isRefreshToken = _authService.CheckRefresh(email, imei, refreshToken);
            if (!isRefreshToken)
            {
                return BadRequest();
            }

            refreshToken = _authService.GetRefreshToken(email, imei);
            string token = GetToken(email, imei);
            GetTokenByRefreshViewModel model = new GetTokenByRefreshViewModel()
            {
                RefreshToken = refreshToken,
                Token = token
            };

            return Ok(model);
        }

        //GetTokenByRefreshUser
        [HttpPost("tokenuser")]
        public IActionResult GetTokenByRefreshUser([FromBody]GetTokenByRefreshUserCommand command)
        {
            string email = "";
            string imei = "";
            string refreshToken = command.RefreshToken;
            int userId = command.UserId;
            string user = command.User;

            bool isRefreshToken = _authService.CheckRefresh(email, imei, refreshToken, userId);
            if (!isRefreshToken)
            {
                return BadRequest();
            }

            refreshToken = _authService.GetRefreshToken(email, imei);
            string token = GetToken(email, imei, user);
            GetTokenByRefreshViewModel model = new GetTokenByRefreshViewModel()
            {
                RefreshToken = refreshToken,
                Token = token
            };

            return Ok(model);
        }
    }
}