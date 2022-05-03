using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TokaApi.Models;

namespace TokaApi.Interfaces
{
    public interface IPersonaFisca
    {
        Task<PersonasFisica> GetByIDAsync(int id);
        Task<IEnumerable<PersonasFisica>> GetAsync();
        Task<ApiResponse> PostAsync(PersonasFisica m);
        Task<ApiResponse> PutAsync(PersonasFisica m);
        Task<ApiResponse> DeleteAsync(int id);

    }
}
