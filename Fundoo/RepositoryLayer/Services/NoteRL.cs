using CommonLayer.Notes;
using RepositoryLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Note = RepositoryLayer.Entities.Note;

namespace RepositoryLayer.Services
{
    public class NoteRL : INoteRL
    {
        FundooDbContext dbContext;

        public NoteRL(FundooDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddNote(int userId, NotePostModel notePost)
        {
            try
            {
                var user = dbContext.User.FirstOrDefault(x => x.UserId == userId);
                Note note = new Note();

                note.NotesId = new Note().NotesId;
                note.Title = notePost.Title;
                note.Description = notePost.Description;
                note.CreatedDate = DateTime.Now;
                note.ModifiedDate = DateTime.Now;
                note.IsRemainder = false;
                note.IsArchive = false;
                note.IsTrash = false;
                note.Color = notePost.Color;
                dbContext.Note.Add(note);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public bool UpdateNotes(int NotesId, NotePostModel notePost)
        {
            Note notes = dbContext.Note.Where(e => e.NotesId == NotesId).FirstOrDefault();
            notes.Title = notePost.Title;
            notes.Description = notePost.Description;
            //notes.IsReminder=notesPost.IsReminder;
            //notes.color=notesPost.color;
            //notes.IsArchive=notesPost.IsArchive;
            //notes.IsPin=notesPost.IsPin;
            //notes.IsTrash=notesPost.IsTrash;
            dbContext.Note.Update(notes);
            var result = dbContext.SaveChangesAsync();
            if (result != null)
                return true;
            else
                return false;

        }

        public IEnumerable<Note> GetAllNotes()
        {
            return dbContext.Note.ToList();
        }

        public bool DeleteNote(int NotesId)
        {
            Note notes = dbContext.Note.Where(e => e.NotesId == NotesId).FirstOrDefault();
            if (notes != null)
            {
                dbContext.Note.Remove(notes);
                dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Note>> changeColor(int NotesId, string Color)
        {
            try
            {
                var note = dbContext.Note.FirstOrDefault(u => u.NotesId == NotesId);
                note.Color = Color;
                await dbContext.SaveChangesAsync();
                return await dbContext.Note.ToListAsync();

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task ArchieveNote(int NotesId)
        {
            try
            {
                var note = dbContext.Note.FirstOrDefault(u => u.NotesId == NotesId);
                note.IsArchive = true;
                await dbContext.SaveChangesAsync();

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task Pin(int NotesId)
        {
            try
            {
                var note = dbContext.Note.FirstOrDefault(u => u.NotesId == NotesId);
                note.IsPin = true;
                await dbContext.SaveChangesAsync();

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task Trash(int NotesId)
        {
            try
            {
                var note = dbContext.Note.FirstOrDefault(u => u.NotesId == NotesId);
                note.IsTrash = true;
                await dbContext.SaveChangesAsync();

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
