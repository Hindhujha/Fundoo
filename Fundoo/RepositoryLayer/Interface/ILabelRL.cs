using CommonLayer.LabelModel;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
   public interface ILabelRL
   {
        Task<List<Label>> CreateLabel(LabelPostModel labelModel, int NotesId, int UserId);

        public IEnumerable<Label> GetLabelsByNoteID(int UserId, int NotesId);

        public bool RenameLabel(int UserId, string OldLabelName, string LabelName);
        public bool RemoveLabel(int UserId, string LabelName);

      
    }
}
