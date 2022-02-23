using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.UserAddressPostModel
{
    public class UserAddressPostModel
    {
        public UserAddressPostModel()
        {
            Type = "Home";
            //Type = "Work";
            //Type = "Other";
        }

        [Required]
        public string Type { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string City { get; set; }


       
    }
}
