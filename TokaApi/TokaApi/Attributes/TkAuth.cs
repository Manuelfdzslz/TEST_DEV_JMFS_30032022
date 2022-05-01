using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokaApi.Data;
using TokaApi.Interfaces;
using TokaApi.Models;

namespace TokaApi.Attributes
{
    public class TkAuth : Attribute, IAuthorizationFilter
    {
       
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool isValid = false;

            try
            {
                if (!string.IsNullOrWhiteSpace(context.HttpContext.Request.Headers["Authorization"].ToString()))
                {
                   
                    var token = (context.HttpContext.Request.Headers["Authorization"].ToString()).Replace("bearer ", "").Replace("Bearer ", "").Trim();
                    var handler = new JwtSecurityTokenHandler();
                    var tokenS = handler.ReadJwtToken(token);

                    var key = Encoding.ASCII.GetBytes(Settings.Current.KeySecret);
                    var validations = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };

                    var id = tokenS.Claims.FirstOrDefault(x => x.Type == "jti").Value;
                    var exp = tokenS.Claims.FirstOrDefault(x => x.Type == "exp").Value;
                    int ID = Int32.Parse(id);

                    var optionsBuilder = new DbContextOptionsBuilder<TokaContext>();
                    optionsBuilder.UseSqlServer(AppSettings.Current.Database);

                    using (TokaContext db= new TokaContext(optionsBuilder.Options))
                    {
                        var user = db.Tb_Users.Where(x => x.UserID == ID && x.Active.Value).FirstOrDefault();
                        if (user!=null)
                        {
                            var dbToken = db.Tb_UserTokens.Where(x => x.UserID == ID && x.Activo && x.Token==token).FirstOrDefault();
                            if (dbToken!=null)
                            {
                                isValid = true;

                            }

                        }
                    }

                   
                }
            }
            catch (Exception ex)
            {
            }

            if (!isValid)
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new { controller = "Home", action = "NotAuthorized" }));
            }

        }
    }
}
