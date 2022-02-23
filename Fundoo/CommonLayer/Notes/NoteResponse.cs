using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Notes
{
   public class NoteResponse
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string color { get; set; }
        public string RegisteredDate { get; set; }

        public string LabelName { get; set; }

        //public virtual  Note noteModel { get; set; }
    }
}
