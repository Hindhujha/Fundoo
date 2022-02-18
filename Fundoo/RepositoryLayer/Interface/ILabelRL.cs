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

        public bool UpdateLabel(int LabelId, LabelPostModel labelPost);
        public bool DeleteLabel(int LabelId);

        Task<List<Label>> GetAllDatas(int UserId);
    }
}
