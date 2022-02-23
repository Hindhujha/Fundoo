using BusinessLayer.Interface;
using CommonLayer.Collab;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entities;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class CollabController : Controller
    {
        ICollabBL collabBL;

        FundooDbContext fundooDbContext;
        public CollabController(ICollabBL collabBL)
        {
            this.collabBL = collabBL;
            this.fundooDbContext = fundooDbContext;
        }
        [Authorize]
        [HttpPost("addcollaborator/{NotesId}")]
        public async Task<IActionResult> AddCollab(int NotesId, CollabPostModel collabPost)
        {
            try
            {

                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userId.Value);

                List<Collab> collab = new List<Collab>();


                collab = await collabBL.AddCollab(UserId, NotesId, collabPost);

                return this.Ok(new { success = true, message = "Collaborator is added successfully", response = collabPost });

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [Authorize]
        [HttpDelete("deleteCollaborator/{collabId}")]
        public async Task<IActionResult> DeleteCollab(int collabId)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userId.Value);

                //List<Collab> collab = new List<Collab>();


                await collabBL.DeleteCollab(UserId,collabId);

                return this.Ok(new { success = true, message = "Collaborator is deleted successfully", response = collabId });

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Authorize]
        [HttpGet("getallCollaborators/{noteId}")]
        public async Task<IActionResult> GetAllCollaborators(int noteId)
        {
            try
            {

                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userId.Value);

                List<Note> collab = new List<Note>();


                collab = await collabBL.GetAllCollaborators(UserId,noteId);

                return this.Ok(new { success = true, message = "  Get All Collaborators from User ", response =collab });

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
