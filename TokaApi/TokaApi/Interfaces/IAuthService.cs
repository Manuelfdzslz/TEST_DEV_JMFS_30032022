using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TokaApi.Models;

namespace TokaApi.Interfaces
{
    public interface IAuthService
    {
        Task<User> PostLogIn(LogIn m);
        Task<User> PutPwdAsync(LogIn m);
        Task<User> PutLogAutAsync(User m);
    }
}
