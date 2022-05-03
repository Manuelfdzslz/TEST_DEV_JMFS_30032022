using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TokaApi.Data;
using TokaApi.Interfaces;
using TokaApi.Models;

namespace TokaApi.Services
{
    public class UserInfoService: IUserInfoService
    {
        private readonly TokaContext _context;
        private readonly IMapper _mapper;
        public UserInfoService()
        {

        }

        public UserInfoService(TokaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

       
        public async Task<UserInfo> GetByIDAsync(int id)
        {
            UserInfo info = new UserInfo();
            var dbinfo = await _context.Tb_UserInfos.FindAsync(id);
            if (dbinfo != null)
            {
                info = _mapper.Map<UserInfo>(info);
                return info;
            }
            return null;
        }

        public async Task<UserInfo> PostAsync(UserInfo m)
        {
            UserInfo info = new UserInfo();
            try
            {
                Tb_UserInfo dbInfo = _mapper.Map<Tb_UserInfo>(m);
                _context.Tb_UserInfos.Add(dbInfo);
                await _context.SaveChangesAsync();
                return info;

            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<UserInfo> PutAsync(UserInfo m)
        {
            try
            {
                var dbInfo = await _context.Tb_UserInfos.FindAsync(m.UserInfoID);
                if (dbInfo != null)
                {
                    dbInfo.Name = m.Name;
                    dbInfo.Lastname = m.Lastname;
                    _context.Tb_UserInfos.Update(dbInfo);
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
                var inf = await _context.Tb_UserInfos.FindAsync(id);
                if (inf != null)
                {
                    _context.Tb_UserInfos.Remove(inf);
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

