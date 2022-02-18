using BusinessLayer.Interface;
using RepositoryLayer.Entities;
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
using CommonLayer.Notes;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using Newtonsoft.Json;

namespace FundooNotes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoteController : ControllerBase
    {
        FundooDbContext fundooDbContext;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        
        INoteBL noteBL;
        public NoteController(INoteBL NoteBL,FundooDbContext fundooDb, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.noteBL = NoteBL;
            this.fundooDbContext = fundooDb;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }


        [Authorize]
        [HttpPost("addNotes")]
        public async Task<IActionResult> AddNote(NotePostModel notePost)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userId.Value);

                //  int userid = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);

                await this.noteBL.AddNote(UserId, notePost);


                return this.Ok(new { success = true, Message = $"Registration is successfull" });
            }
            catch (Exception e)
            {
                 throw e;
            }
        }

        [Authorize]
        [HttpPut("updatenote/{NotesId}")]
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
        //[Authorize]
        //[HttpGet("getAllNoteusingRedis")]
        //public async Task<IActionResult> GetAllNotes()
        //{
        //    try
        //    {
        //        var cacheKey = "NoteList";
        //        string serializedNoteList;
        //        var noteList = new List<Note>();
        //        var redisnoteList = await distributedCache.GetAsync(cacheKey);
        //        if (redisnoteList != null)
        //        {
        //            serializedNoteList = Encoding.UTF8.GetString(redisnoteList);
        //            noteList = JsonConvert.DeserializeObject<List<Note>>(serializedNoteList);
        //        }
        //        else
        //        {
        //            noteList = await noteBL.GetAllNotes();
        //            serializedNoteList = JsonConvert.SerializeObject(noteList);
        //            redisnoteList = Encoding.UTF8.GetBytes(serializedNoteList);
        //        }
        //        return this.Ok(noteList);

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        [Authorize]
        [HttpDelete("deleteNote/{NotesId}")]
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
        [HttpPut("changecolor/{NotesId}/{Color}")]
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
        [HttpPut("archivenote/{NotesId}")]
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
        [HttpPut("pin/{NotesId}")]
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
        [HttpPut("trash/{NotesId}")]
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

        [Authorize]
        [HttpGet("getAllNotes")]
        public async Task<IActionResult> GetAllNotes()
        {
            try
            {
                int userid = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
                var noteList = new List<Note>();
                noteList = await noteBL.GetAllNotes(userid);

                return this.Ok(new { Success = true, message = $"GetAll note successfull ", data = noteList });

            }
            catch (Exception)
            {

                throw;
            }
        }

    }

   



}
