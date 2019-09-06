using Core.Common.Model.Search;

namespace Core.Users.Query.Request
{
    public class UserQuery : BaseQuery
    {
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Name { get; set; }
    }
}
