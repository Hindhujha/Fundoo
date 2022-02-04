using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.User
{
   public class UserPostModel
    {
        public string fName { get; set; }
        public string lName { get; set; }
        public string phNo { get; set; }
        public string address { get; set; }
        public string email { get; set; }
  
        public string password { get; set; }
        public string cPassword { get; set; }
    }
}
