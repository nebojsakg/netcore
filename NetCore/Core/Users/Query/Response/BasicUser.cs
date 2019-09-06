using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Users.Query.Response
{
    public class BasicUser
    {
        public int? Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string UserName { get; set; }
    }
}
