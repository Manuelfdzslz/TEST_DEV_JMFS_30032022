using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TokaApi.Models;

namespace TokaApi.Interfaces
{
    public interface IUserInfoService
    {
        Task<UserInfo> GetByIDAsync(int id);
        Task<UserInfo> PostAsync(UserInfo m);
        Task<UserInfo> PutAsync(UserInfo m);
        Task DeleteAsync(int id);

    }
}
