using CommonLayer.Collab;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface ICollabRL
    {
        Task<List<Collab>> AddCollab(int UserId, int NotesId,  CollabPostModel collabPost);

        Task DeleteCollab(int UserId, int CollabId);
        Task<List<Note>> GetAllCollaborators(int UserId, int NotesId);
    }
}
