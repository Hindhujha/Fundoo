using BusinessLayer.Interface;
using CommonLayer.Notes;
using CommonLayer.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using BusinessLayer.Services;

namespace FundooNotes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoteController : ControllerBase
    {       
        FundooDbContext fundooDbContext;
        INoteBL noteBL;
        public NoteController(INoteBL NoteBL,FundooDbContext fundooDb)
        {
            this.noteBL = NoteBL;
            this.fundooDbContext = fundooDb;
        }


        //  [Authorize]
        [HttpPost("addnotes")]
        public async Task<ActionResult> AddNote(int UserId,NotePostModel notePost)
        {
            try
            {
                //var Userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                //int userid = Int32.Parse(Userid.Value);

                 await this.noteBL.AddNote(UserId, notePost);

                return this.Ok(new { success = true, message = $"Note Created Sucessfully " });

                


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //[HttpPost("register")]
        //public ActionResult AddNote(int UserId,NotePostModel notePost)
        //{
        //    try
        //    {
        //        this.noteBL.AddNote(UserId,notePost);
        //        return this.Ok(new { success = true, message = $"Note Created Sucessfully" });
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}
        [Authorize]
        [HttpPut("updatenote")]
        public IActionResult UpdateNotes(int NotesId, NotePostModel notePost)
        {
            try
            {
                if (noteBL.UpdateNotes(NotesId, notePost))
                {
                    return this.Ok(new { Success = true, message = "Notes updated successfully", response = notePost, NotesId });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Note with given ID not found" });
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        [Authorize]
        [HttpGet("getallnotes")]
        public IEnumerable<Note> GetAllNotes()
        {
            try
            {
                return noteBL.GetAllNotes();
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        [Authorize]
        [HttpDelete]
        public IActionResult DeleteNote(int NotesId)
        {
            try
            {
                if (noteBL.DeleteNote(NotesId))
                {
                    return this.Ok(new { Success = true, message = "Notes deleted successfully" });

                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Notes with given ID not found" });
                }

            }
            catch (Exception e)
            {

                throw e;
            }
        }
        [Authorize]
        [HttpPut("{noteID}/{color}")]
        public async Task<IActionResult> changeColor(int NotesId, string Color)
        {
            try
            {
                List<Note> note = await noteBL.changeColor(NotesId, Color);
                if (note != null)
                {
                    return this.Ok(new { Success = true, message = "Color changed successfully", data = note });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Notes with given ID not found" });
                }

            }
            catch (Exception e)
            {

                throw e;
            }
        }
        [Authorize]
        [HttpPut("{noteID}")]
        public async Task<IActionResult> IsArchieve(int NotesId)
        {
            try
            {
                await noteBL.ArchieveNote(NotesId);


                return this.Ok(new { Success = true, message = $"NoteArchieve successfull for {NotesId}" });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPut("{noteID}")]
        public async Task<IActionResult> Pin(int NotesId)
        {
            try
            {
                await noteBL.Pin(NotesId);
                return this.Ok(new { Success = true, message = $"Your Content is pinned {NotesId}" });
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [Authorize]
        [HttpPut("{noteID}")]
        public async Task<IActionResult> Trash(int NotesId)
        {
            try
            {
                await noteBL.Trash(NotesId);
                return this.Ok(new { Success = true, message = $"Your Content is moved to Trash {NotesId}" });
            }
            catch (Exception e)
            {

                throw e;
            }
        }

    }

   



}
