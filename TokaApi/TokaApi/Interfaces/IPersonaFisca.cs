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
        Task<PersonasFisica> PostAsync(PersonasFisica m);
        Task<PersonasFisica> PutAsync(PersonasFisica m);
        Task DeleteAsync(int id);

    }
}
