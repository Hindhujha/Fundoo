using CommonLayer.Collab;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
  public  class CollabRL:ICollabRL
    {
        FundooDbContext dbContext;

        public CollabRL(FundooDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Collab>> AddCollab(int UserId, int NotesId, CollabPostModel collabPost)
        {
            try
            {
                var user = dbContext.User.FirstOrDefault(e => e.UserId == UserId);
                var note = dbContext.Note.FirstOrDefault(u => u.NotesId == NotesId);
                Collab collab = new Collab();
                collab.collabEmail = collabPost.collabEmail;
                collab.NotesId = NotesId;
                collab.CollabId = new Collab().CollabId;            
                collab.User = user;
                collab.Note = note;
                dbContext.Collab.Add(collab);
                await dbContext.SaveChangesAsync();
                return await dbContext.Collab.Where(u => u.UserId == UserId)
                    .Include(u => u.Note)
                    .Include(u => u.User)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task DeleteCollab(int UserId, int CollabId)
        {
            try
            {
                Collab collab = dbContext.Collab.Where(e => e.CollabId == CollabId).FirstOrDefault();
                if (collab != null)
                {
                    //this.dbContext.collabarators.Remove(collabarator);
                    this. dbContext.Collab.Remove(collab);
                    await dbContext.SaveChangesAsync();

                }
             
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Collab>> GetAllCollaborators(int UserId)
        {
            try
            {
                Collab collabarator = new Collab();
                return await dbContext.Collab.Where(u => u.UserId == UserId)
                    .Include(u => u.Note)
                    .Include(u => u.User)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }


        }

    }
}
