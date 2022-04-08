using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TokaApi.Data;
using TokaApi.Interfaces;
using TokaApi.Models;

namespace TokaApi.Services
{
    public class UserService : IUserService
    {
        private readonly TokaContext _context;
        private readonly IMapper _mapper;
        private readonly IUserInfoService _info;


        public UserService(TokaContext context, IMapper mapper, IUserInfoService userInfoService)
        {
            _context = context;
            _mapper = mapper;
            _info = userInfoService;
        }

        public async Task<IEnumerable<User>> GetAsync()
        {
            List<User> users = new List<User>();
            var dbUsers = await _context.Tb_Users.ToListAsync();
            users= dbUsers.Select(x => _mapper.Map<User>(x)).ToList();
           
            return users;
        }

        public async Task<User> GetByIDAsync(int id)
        {
            User user = new User();
            var dbUser = await _context.Tb_Users.FindAsync(id);
            if (dbUser!=null)
            {
                user = _mapper.Map<User>(dbUser);
                return user;
            }
            return null;
        }

        public async Task<User> PostAsync(User m)
        {
            User user = new User();
            try
            {
                Tb_User dbUser =_mapper.Map<Tb_User>(m);
                _context.Tb_Users.Add(dbUser);
                await _context.SaveChangesAsync();
                return user;

            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<User> PutAsync(User m)
        {
            try
            {
                var dbUser = await _context.Tb_Users.FindAsync(m.UserID);
                if (dbUser!=null)
                {
                    dbUser.Email = m.Email;
                    dbUser.Pasword = m.Pasword;
                    _context.Tb_Users.Update(dbUser);
                    await _context.SaveChangesAsync();
                }
                return m;

            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var dbUser = await _context.Tb_Users.FindAsync(id);
                if (dbUser != null)
                {
                    _context.Tb_Users.Remove(dbUser);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
