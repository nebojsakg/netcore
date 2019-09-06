using DelegateDecompiler;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Domain.Model
{
    public class User : IdentityUser<int>
    {
        public string Name { get; set; }

        public List<UserRole> UserRoles { get; set; }

        [NotMapped]
        [Computed]
        public IEnumerable<Role> Roles =>
            UserRoles.Select(ur => ur.Role);

        [NotMapped]
        [Computed]
        public string RolesString => 
            UserRoles.Aggregate("", (result, ur) => result + result == "" ? ur.Role.Name : "," + ur.Role.Name);

        [Computed]
        public bool HasRole(string rolename)
        {
            return UserRoles.Any(r => r.Role.Name == rolename);
        }

    }
}
