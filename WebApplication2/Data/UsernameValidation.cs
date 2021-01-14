using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Data
{
    public class UsernameValidation: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string username = value.ToString();

            // no spaces, use _ instead
            return !username.Contains(" ") && username.Length >= 3;
        }
    }
}
