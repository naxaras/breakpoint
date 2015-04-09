using System;
using System.Collections.Generic;
using System.Text;

namespace Lisa.Breakpoint.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public User()
        {
            Id = -1;
            UserName = null;
        }

        public User(string userName)
        {
            Id = 0;
            UserName = userName;
        }

        public static bool IsInitialized(User user)
        {
            if (user == null)
            {
                return false;
            }

            return string.IsNullOrEmpty(user.UserName) ? false : true;
        }
    }
}
