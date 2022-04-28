using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TokaApi.Models;

namespace TokaApi.Interfaces
{
    public interface IUserService
    {
        Task<User> GetByIDAsync(int id);
        Task<IEnumerable<User>> GetAsync();
        Task<User> PostAsync(User m);
        Task<User> PutAsync(User m);
        Task DeleteAsync(int id);
    }
}
