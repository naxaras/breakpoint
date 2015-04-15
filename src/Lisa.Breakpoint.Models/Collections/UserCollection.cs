using System.Collections.Generic;
using System.Linq;

namespace Lisa.Breakpoint.Models.Collections
{
    public class UserCollection
    {
        public int Id { get; set; }
        public Roles Role { get; set; }
        public virtual List<User> Users { get; set; }

        public UserCollection()
        {
            Id = -1;
            Role = Roles.Default;
            Users = null;
        }

        public UserCollection(Roles role, IEnumerable<User> users)
        {
            Id = 0;
            Role = role;
            Users = users.ToList();
        }
    }
}