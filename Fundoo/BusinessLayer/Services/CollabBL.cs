using BusinessLayer.Interface;
using CommonLayer.Collab;
using CommonLayer.Label;
using CommonLayer.LabelModel;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class CollabBL : ICollabBL
    {
        ICollabRL collabRL;
        public CollabBL(ICollabRL collabRL)
        {
            this.collabRL = collabRL;
        }

        public async Task<List<Collab>> AddCollab(int UserId, int NotesId, CollabPostModel collabPost)
        {
            try
            {
                return await collabRL.AddCollab(UserId,NotesId,collabPost);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteCollab(int UserId, int CollabId)
        {
            try
            {
               await collabRL.DeleteCollab(UserId, CollabId);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Note>> GetAllCollaborators(int UserId,int NotesId)
        {

            try
            {
                return await collabRL.GetAllCollaborators(UserId,NotesId);
            }
            catch (Exception e)
            {

                throw e;
            }

        }
    }
}
