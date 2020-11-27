using FeedBackDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedBackDemo.Repo
{
    public class UserRepo : IDisposable
    {
        private readonly ApplicationDbContext context;

        public UserRepo(ApplicationDbContext _context)
        {
            context = new ApplicationDbContext();
        }

     

        public IQueryable<ApplicationUser> GetUsersInRole(string roleName)
        {

            var roles = context.Roles.Where(r => r.Name == roleName);
            if (roles.Any())
            {
                var roleId = roles.First().Id;
                return from user in context.Users
                       where user.Roles.Any(r => r.RoleId == roleId)
                       select user;
            }
            return null;

        }


        void IDisposable.Dispose()
        {
            context.Dispose();
        }
    }
}