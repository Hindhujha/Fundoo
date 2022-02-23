using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace RepositoryLayer.Entities
{
   public class Note
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int NotesId { get; set; }    

        [ForeignKey("User")]
        public int? UserId { get; set; }
        public virtual User User { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public bool  IsRemainder { get; set; }    
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Color { get; set; }
        public bool IsArchive { get; set; }
        public bool IsPin { get; set; }
        public bool IsTrash { get; set; }

        public virtual IList<Collab> Collab { get; set; }

        public virtual IList<Label>   Label { get; set; }

    }
}
