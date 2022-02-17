using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Entities
{
   public class User
   {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public  int UserId { get; set; } 

        [Required]
        public string fName { get; set; }

        [Required]
        public string lName { get; set; }
        public string phNo { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        [Required]

        public string password { get; set; }
        public string cPassword { get; set; }
        public DateTime registeredDate { get; set; }
        public DateTime modifiedDate { get; set; }

    

    }
}
