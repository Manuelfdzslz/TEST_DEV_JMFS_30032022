using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace TokaFront.Controllers
{
    public class ParentController : Controller
    {
        
        protected string GetTokenValue(string key)
        {
            string value = "";
            string token = string.Empty;
            if (HttpContext != null && HttpContext.Request != null && HttpContext.Request.Cookies.ContainsKey("toka-token-app"))
                token = HttpContext.Request.Cookies["toka-token-app"];

            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);

                switch (key)
                {
                    case "Token":
                        value = token;
                        break;

                    case "UserId":
                        value = jwt.Claims.FirstOrDefault(x => x.Type == "jti").Value;
                        break;

                    case "LoginId":
                        value = jwt.Claims.FirstOrDefault(x => x.Type == "sub").Value;
                        break;

                    default:
                        value = jwt.Claims.FirstOrDefault(x => x.Type == key).Value;
                        break;
                }
            }

            return value;
        }
        
        
        
    }
}
