using Common;
using Core.Users.Query.Request;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Users.Query.Extensions
{
    public static class UserSearchExtensions
    {
        public static IQueryable<User> Search(this IQueryable<User> query, UserQuery searchRequest)
        {
            query = searchRequest.Id != null ? query.Where(e => e.Id == searchRequest.Id) : query;
            query = searchRequest.Email != null ? query.Where(e => e.Email == searchRequest.Email) : query;
            query = searchRequest.Name != null ? query.Where(e => e.Name == searchRequest.Name) : query;
            query = searchRequest.PhoneNumber != null ? query.Where(e => e.PhoneNumber == searchRequest.PhoneNumber) : query;

            return query;
        }
    }
}
