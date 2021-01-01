using System;
using System.Linq;
using AspNetCoreIdentityLab.Persistence.DataTransferObjects;
using AspNetCoreIdentityLab.Persistence.EntityFrameworkContexts;

namespace AspNetCoreIdentityLab.Persistence.Mappers
{
    public class UserLoginIPMapper
    {
        public UserLoginIp FindBy(int userId)
        {
            using (var context = new AspNetCoreIdentityLabDbContext())
            {
                return context.UserLoginIp.FirstOrDefault(u => u.UserId.Equals(userId));
            }
        }

        public void Save(int userId, string IP)
        {
            using(var context = new AspNetCoreIdentityLabDbContext())
            {
                var userLoginIp = new UserLoginIp()
                {
                    UserId = userId,
                    When = DateTime.Now,
                    IP = IP
                };

                context.UserLoginIp.Add(userLoginIp);
                context.SaveChanges();
            }
        }

        public void Update(UserLoginIp userLoginIp)
        {
            using (var context = new AspNetCoreIdentityLabDbContext())
            {
                context.UserLoginIp.Update(userLoginIp);
                context.SaveChanges();
            }
        }
    }
}
