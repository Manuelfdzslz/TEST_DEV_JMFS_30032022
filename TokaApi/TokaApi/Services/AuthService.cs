using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TokaApi.Data;
using TokaApi.Interfaces;
using TokaApi.Models;

namespace TokaApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly TokaContext _context;
        private readonly IMapper _mapper;

        public AuthService(TokaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<User> PostLogIn(User m)
        {
            try
            {
                var dbUser = await _context.Tb_Users.FindAsync(m.Email);
                if (dbUser==null)
                {
                    throw new Exception("Invalid password");
                }
                if (m.Pasword != dbUser.Pasword)
                {
                    throw new Exception("Invalid password");
                }

                if (!dbUser.Active.Value)
                {
                    throw new Exception("User inactive");
                }
                User user = _mapper.Map<User>(dbUser);

                string token = await _createToken(user.UserID);
                var dbToken = new Tb_UserToken();
                dbToken.UserID = dbUser.UserID;
                dbToken.Activo = true;
                dbToken.FechaRegistro = DateTime.Now;
                dbToken.Token = token;
                _context.Tb_UserTokens.Add(dbToken);
                await _context.SaveChangesAsync();

                user.Token = token;
                return user;

            }
            catch (Exception)
            {

                return null;
            }
        }

       

        public Task<User> PutLogAutAsync(User m)
        {
            throw new NotImplementedException();
        }

        public Task<User> PutPwdAsync(User m)
        {
            throw new NotImplementedException();
        }


        private async Task<string> _createToken(int userID)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(Settings.Current.KeySecret);
                var infoUser = _context.Tb_UserInfos.Where(x => x.UserID == userID).FirstOrDefault();
                string lastname = infoUser?.Lastname;
                string firstname = infoUser?.Name;
                string userName = firstname +" "+ lastname;
               
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(JwtRegisteredClaimNames.Sub,userName),
                    new Claim(JwtRegisteredClaimNames.Jti,userID.ToString()),
                    new Claim("Fn", firstname),
                    new Claim("Ln", lastname),
                    }),
                    Expires = DateTime.UtcNow.AddDays(Settings.Current.Expires),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                    Issuer = Settings.Current.Issuer,
                    Audience = Settings.Current.Audience,
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private IEnumerable<Claim> _getTokenClaims(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            var TokenS = handler.ReadJwtToken(token);

            return TokenS.Claims;
        }
    }
}
