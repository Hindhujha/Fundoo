using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Notes
{
   public class NoteUserResponse
    {
        public int userId { get; set; }
        public int NotesId { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string color { get; set; }
        public DateTime RegisteredDate { get; set; }
        public string email { get; set; }

       
    }
}
