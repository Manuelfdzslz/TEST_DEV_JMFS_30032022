using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TokaApi.Models;

namespace TokaApi.Interfaces
{
    public interface IAuthService
    {
        bool GetTokenAsync(string token);
        Task<User> PostLogIn(LogIn m);
        Task<User> PutPwdAsync(LogIn m);
        Task<User> PutLogAutAsync(User m);
    }
}
