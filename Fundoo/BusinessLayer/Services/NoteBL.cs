using BusinessLayer.Interface;
using CommonLayer.Notes;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
   public class NoteBL:INoteBL
    {
        INoteRL noteRL;

        public NoteBL(INoteRL noteRL)
        {
            this.noteRL = noteRL;
        }

        public Task AddNote(int UserId, NotePostModel notePost)
        {
            try
            {
                return noteRL.AddNote(UserId,notePost);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateNotes(int NotesId, NotePostModel notePost)
        {
            try
            {
                if (noteRL.UpdateNotes(NotesId, notePost))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<Note> GetAllNotes()
        {

            try
            {
                return noteRL.GetAllNotes();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public bool DeleteNote(int NotesId)
        {
            try
            {
                if (noteRL.DeleteNote(NotesId))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Note>> changeColor(int NotesId, string Color)
        {
            try
            {
                return await noteRL.changeColor(NotesId, Color);
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
                await noteRL.ArchieveNote(NotesId);
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
                await noteRL.Pin(NotesId);
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
                await noteRL.Trash(NotesId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }


}
