using CommonLayer.LabelModel;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public  interface ILabelBL
    {
        Task<List<Label>> CreateLabel(LabelPostModel labelModel, int NotesId, int UserId);

        public IEnumerable<Label> GetLabelsByNoteID(int UserId, int NotesId);

        public bool RenameLabel(int UserId, string OldLabelName, string LabelName);
        public bool RemoveLabel(int UserId, string LabelName);


        
    }
}
