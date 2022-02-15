using CommonLayer.Notes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
   public interface INoteRL
   {
        Task AddNote(int UserId, NotePostModel notePost);
        public bool UpdateNotes(int NotesId, NotePostModel notePost);
        public IEnumerable<Note> GetAllNotes();
        public bool DeleteNote(int NotesId);
        Task<List<Note>> changeColor(int NotesId, string Color);
        Task ArchieveNote(int NotesId);

        Task Pin(int NotesId);
        Task Trash(int NotesId);
    }
}
