using AutoMapper;
using Microsoft.Extensions.Options;
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
        private readonly IOptions<AuthenticationSettings> _authSettings;

        public AuthService(TokaContext context, IMapper mapper, IOptions<AuthenticationSettings> authSettings)
        {
            _context = context;
            _mapper = mapper;
            _authSettings = authSettings;
        }
        public bool GetTokenAsync(string token)
        {
            var Claims = _getTokenClaims(token);

            if (!Claims.Any())
                throw new Exception("Invalid Token");

            var expiration = Claims.FirstOrDefault(x => x.Type == "exp");
            if (string.IsNullOrWhiteSpace(expiration.Value))
                throw new Exception("Invalid Token");

            DateTime expirationDate = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(expiration.Value)).UtcDateTime;
            if (expirationDate < DateTime.Now)
                throw new Exception("Expired Token");

            var id = Claims.FirstOrDefault(x => x.Type == "jti").Value;
            int userId = int.Parse(id);
            var tk = _context.Tb_UserTokens.Where(x => x.Token == token && x.Activo && x.UserID == userId);
            if (!tk.Any())
            {
                throw new Exception("Invalid Token");
            }

            return true;
        }

        public async Task<User> PostLogIn(LogIn m)
        {
            try
            {
                var dbUser =  _context.Tb_Users.Where(x=>x.Email==m.Email).FirstOrDefault();
                if (dbUser==null)
                {
                    throw new Exception("Invalid user");
                }
                if (m.Password != dbUser.Pasword)
                {
                    throw new Exception("Invalid password");
                }

                if (!dbUser.Active.Value)
                {
                    throw new Exception("User inactive");
                }
                User user = _mapper.Map<User>(dbUser);
                var tokens=_context.Tb_UserTokens.Where(x=>x.UserID== dbUser.UserID);
                foreach (var t in tokens)
                {
                    t.Activo = false;
                    _context.Tb_UserTokens.Update(t);
                }
                string token = _createToken(user.UserID);
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
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<User> PutLogAutAsync(User m)
        {
            try
            {
                var user = _context.Tb_Users.Where(x => x.Email == m.Email).FirstOrDefault();

                var dbtoken = _context.Tb_UserTokens.Where(x => x.Activo && x.UserID == user.UserID).FirstOrDefault();
                if (dbtoken != null)
                {
                    dbtoken.Activo = false;
                    _context.Tb_UserTokens.Update(dbtoken);
                    await _context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

            return m;

        }

        public Task<User> PutPwdAsync(LogIn m)
        {
            throw new NotImplementedException();
        }


        private string _createToken(int userID)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_authSettings.Value.KeySecret);
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
                    Expires = DateTime.UtcNow.AddDays(_authSettings.Value.Expires),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                    Issuer = _authSettings.Value.Issuer,
                    Audience = _authSettings.Value.Audience,
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
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
