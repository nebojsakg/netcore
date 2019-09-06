using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Core.Users
{
    public static class UserIncludeExtensions
    {
        public static IQueryable<User> IncludeAll(this IQueryable<User> query)
        {
            return query.Include("UserRoles.Role");
        }

        public static IQueryable<User> IncludeRoles(this IQueryable<User> query)
        {
            return query.Include("UserRoles.Role");
        }
    }
}
