using Microsoft.AspNetCore.Identity;
using System;

namespace Users.Models
{
    public class User : IdentityUser
    {
        public bool IsChecked { get; set; }
        public DateTimeOffset RegistrationDate { get; set; }
        public DateTimeOffset LastLoginDate { get; set; }
        public string UserStatus { get; set; }
    }
}
