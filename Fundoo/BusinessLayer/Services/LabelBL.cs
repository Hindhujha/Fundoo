using BusinessLayer.Interface;
using CommonLayer.LabelModel;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class LabelBL : ILabelBL
    {
        ILabelRL labelRL;
        public LabelBL(ILabelRL labelRL)
        {
            this.labelRL = labelRL;
        }
        public async Task<List<Label>> CreateLabel(LabelPostModel labelModel, int NotesId, int UserId)
        {
            try
            {
                return await labelRL.CreateLabel(labelModel, NotesId, UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Label> GetLabelsByNoteID(int UserId, int NotesId)
        {
            try
            {
                return labelRL.GetLabelsByNoteID(UserId, NotesId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
