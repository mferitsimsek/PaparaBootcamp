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
            var cookie = context.HttpContext.Request.Cookies["LoggedInUser"];
            if (string.IsNullOrEmpty(cookie))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
