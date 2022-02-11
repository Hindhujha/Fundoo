using System;
using System.ComponentModel.DataAnnotations;
namespace RepositoryLayer.Services
{
    public class UserLogin
    {
        [RegularExpression(@"^[a-z0-9]+(.[a-z0-9]+)?@[a-z]+[.][a-z]{3}$",
         ErrorMessage = "Please Enter Valid Email")]

        public string email { get; set; }

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
 ErrorMessage = "Password Have minimum 8 Characters, Should have at least 1 Upper Case and Should have at least 1 numeric number and Has exactly 1 Special Character")]
        public string password { get; set; }
    }
}