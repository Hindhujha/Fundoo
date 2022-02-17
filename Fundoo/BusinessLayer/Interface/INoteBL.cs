﻿using CommonLayer.Notes;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
   public interface INoteBL
    {
        Task AddNote(int UserId, NotePostModel notePost);
        public bool UpdateNotes(int NotesId, NotePostModel notePost);
        Task<List<Note>> GetAllNotes(int UserId);
        public bool DeleteNote(int NotesId);
        Task<List<Note>> changeColor(int NotesId, string Color);
        Task ArchieveNote(int NotesId);
        Task Pin(int NotesId);

        Task Trash(int NotesId);
    }
}
