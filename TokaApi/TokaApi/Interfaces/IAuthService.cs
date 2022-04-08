using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TokaApi.Models;

namespace TokaApi.Interfaces
{
    public interface IAuthService
    {
        Task<User> PostLogIn(User m);
        Task<User> PutPwdAsync(User m);
        Task<User> PutLogAutAsync(User m);
    }
}
