using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.User
{
    public class ForgotUserModel
    {
        [Required]
        [RegularExpression(@"^[a-z0-9]+(.[a-z0-9]+)?@[a-z]+[.][a-z]{3}$",
           ErrorMessage = "Please Enter Valid Email")]

        public string email { get; set; }
    }
}
