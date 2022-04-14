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
            var dbUsers = await _context.Tb_Users.Include("Tb_UserInfos").ToListAsync();
            //users = dbUsers.Select(x => _mapper.Map<User>(x)).ToList();
            foreach (var dbuser in dbUsers)
            {
                var user = _mapper.Map<User>(dbuser);
                user.Info = _mapper.Map<UserInfo>(dbuser.Tb_UserInfos.First());
                users.Add(user);
            }
           
            return users;
        }

        public async Task<User> GetByIDAsync(int id)
        {
            User user = new User();
            var dbUser = await _context.Tb_Users.FindAsync(id);
            if (dbUser!=null)
            {
                user = _mapper.Map<User>(dbUser);
                var dbInfo =  _context.Tb_UserInfos.Where(x=>x.UserID==dbUser.UserID).FirstOrDefault();
                user.Info = _mapper.Map<UserInfo>(dbInfo);
                user.Token =  _context.Tb_UserTokens.Where(x=>x.UserID==user.UserID && x.Activo).FirstOrDefault()?.Token;
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
                m.Info.UserID = dbUser.UserID;
                await _info.PostAsync(m.Info);
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
