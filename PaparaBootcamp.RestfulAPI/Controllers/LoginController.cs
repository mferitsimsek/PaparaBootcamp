using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using PaparaBootcamp.Application.Attributes;
using PaparaBootcamp.Application.Services;
using PaparaBootcamp.Domain.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PaparaBootcamp.RestfulAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginService _loginService;
        private readonly IConfiguration _configuration;
        public LoginController(LoginService loginService, IConfiguration configuration)
        {
            _loginService = loginService;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request == null) return BadRequest();

            var user = _loginService.Login(request.Email, request.Password);
            if (user)
            {
                HttpContext.Response.Cookies.Append("LoggedInUser",request.Email);
                return Ok(new { Message = "Giriş Başarılı" });
            }

            return Unauthorized(new { Message = "Geçersiz Kullanıcı Adı veya Parola" });
        }

        [HttpGet]
        [Auth]
        public IActionResult Test() 
        {
            return Ok(new { Message = "Test başarılı" });
        }

       
    }
}
