using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using PaparaBootcamp.Application.Services;
using PaparaBootcamp.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaBootcamp.Application.Attributes
{
    public class AuthAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // istek header'ından Authorization kısmını alıyoruz
            var authHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            if (authHeader == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var credentials = authHeader.Split(':');
            var username = credentials[0];
            var password = credentials[1];

            var authService = context.HttpContext.RequestServices.GetService<LoginService>();

            if (!authService.Login(username, password))
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
