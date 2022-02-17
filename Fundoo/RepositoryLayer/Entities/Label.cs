using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Entities
{
    public class Label
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int LabelId { get; set; }

        [DataType(DataType.Text)]
        [Required]
        public string LabelName { get; set; }

     
        [ForeignKey("Note")]
        public int? NotesId { get; set; }
        public virtual Note Note { get; set; }


        [ForeignKey("User")]
        public int? UserId { get; set; }
        public virtual User User { get; set; }

        
    }
}
