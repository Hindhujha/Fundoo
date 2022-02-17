using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Interface;
using Microsoft.EntityFrameworkCore;
using CommonLayer.LabelModel;

namespace RepositoryLayer.Services
{
    public class LabelRL : ILabelRL
    {
        FundooDbContext dbContext;

        public LabelRL(FundooDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Label>> CreateLabel(LabelPostModel labelModel, int NotesId, int UserId)
        {
            try
            {
                var user = dbContext.User.FirstOrDefault(e => e.UserId == UserId);
                var note = dbContext.Note.FirstOrDefault(u => u.NotesId == NotesId);
                Label label = new Label();
                label.UserId = UserId;
                label.NotesId = NotesId;
                label.LabelId = new Label().LabelId;
                label.LabelName = labelModel.LabelName;
                label.User = user;
                label.Note = note;
                dbContext.Label.Add(label);
                await dbContext.SaveChangesAsync();
                return await dbContext.Label.Where(u => u.UserId == UserId)
                    .Include(u => u.Note)
                    .Include(u => u.User)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Label> GetLabelsByNoteID(int UserId, int NotesId)
        {
            try
            {
                var result = dbContext.Label.Where(e => e.NotesId == NotesId && e.UserId == UserId).ToList();
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }



    }
   
}
