using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Data
{
    public class UserSignUpData
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string SuperSecretOverride { get; set; }
    }
}
