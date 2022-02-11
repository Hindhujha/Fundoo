using CommonLayer.Notes;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class NoteRL : INoteRL
    {
        FundooDbContext dbContext;

        private readonly IConfiguration configuration;
        public NoteRL(FundooDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddNote(int UserId, NotePostModel notePost)
        {
            try
            {
                var user = dbContext.User.FirstOrDefault(x => x.UserId == UserId);
                Note note = new Note();
                note.NotesId = new Note().NotesId;
                note.Title = notePost.Title;
                note.Description = notePost.Description;
                note.CreatedDate = DateTime.Now;
                dbContext.Note.Add(note);
                await dbContext.SaveChangesAsync();
               
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
