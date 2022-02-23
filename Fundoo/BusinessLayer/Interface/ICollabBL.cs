using CommonLayer.Collab;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface ICollabBL
    {
        Task<List<Collab>> AddCollab(int UserId, int NotesId, CollabPostModel collabPost);

        Task DeleteCollab(int UserId, int CollabId);

        Task<List<Collab>> GetAllCollaborators(int UserId);
    }
}
