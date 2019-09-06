using System.Collections.Generic;

namespace Core.Users.Query.Response
{
    public class UserDetails : BasicUser
    {
        public List<RoleDetails> Roles { get; set; }
    }
}
