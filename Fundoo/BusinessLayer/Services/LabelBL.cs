using BusinessLayer.Interface;
using CommonLayer.LabelModel;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections;
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


        public bool UpdateLabel(int LabelId, LabelPostModel labelPost)
        {
            try
            {
                if (labelRL.UpdateLabel(LabelId, labelPost))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

     
        public bool DeleteLabel(int LabelId)
        {
            try
            {
                if (labelRL.DeleteLabel(LabelId))
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<List<Label>> GetAllDatas(int UserId)
        {

            try
            {
                return await labelRL.GetAllDatas(UserId);
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public async Task<List<Label>> GetAllLabelsByNoteId(int NoteId,int UserId)
        {

            try
            {
                return await labelRL.GetAllLabelsByNoteId(NoteId,UserId);
            }
            catch (Exception e)
            {

                throw e;
            }

        }

    }
}
