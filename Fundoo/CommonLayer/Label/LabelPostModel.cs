using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.LabelModel
{
  public  class LabelPostModel
  {
        [Required]
        public string LabelName { get; set; }

        [Required]
        public int LabelId { get; set; }
       
    }
}
