using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CommonLayer.User
{
   public class UserPostModel
    {  
        [RegularExpression(@"^[a-zA-Z]{4,}$",
        ErrorMessage="Please Enter Valid First Name")]
        public string fName { get; set; }

        [RegularExpression(@"^[a-zA-Z]{4,}$",
       ErrorMessage = "Please Enter Valid Last Name")]
        public string lName { get; set; }

        [RegularExpression(@"^[6-9]{1}[0-9]{9}$",
      ErrorMessage = "Please Enter Valid Phone Number")]
        public string phNo { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9]{5,}$",
     ErrorMessage = "Please Enter Valid Address")]

        public string address { get; set; }

        [RegularExpression(@"^[a-z0-9]+(.[a-z0-9]+)?@[a-z]+[.][a-z]{3}$",
  ErrorMessage = "Please Enter Valid Email")]

        public string email { get; set; }

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
  ErrorMessage = "Password Have minimum 8 Characters, Should have at least 1 Upper Case and Should have at least 1 numeric number and Has exactly 1 Special Character")]



        public string password { get; set; }

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
ErrorMessage = "Password Have minimum 8 Characters, Should have at least 1 Upper Case and Should have at least 1 numeric number and Has exactly 1 Special Character")]


        public string cPassword { get; set; }
   }
}
