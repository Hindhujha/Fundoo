using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CommonLayer.LabelModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using RepositoryLayer.Entities;
using RepositoryLayer.Services;

namespace FundooNotes.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class LabelController : ControllerBase
    {
        ILabelBL labelBL;

        FundooDbContext fundooDbContext;
        public LabelController(ILabelBL labelBL)
        {
            this.labelBL = labelBL;
            this.fundooDbContext = fundooDbContext;
        }
        [Authorize]
        [HttpPost("createlabels/{NotesId}")]
        public async Task<IActionResult> CreateLabel(LabelPostModel labelModel, int NotesId)
        {
            try
            {

                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userId.Value);

                List<Label> labels = new List<Label>();


                labels = await labelBL.CreateLabel(labelModel, NotesId, UserId);

                return this.Ok(new { success = true, message = "Label added successfully", response = labelModel, NotesId });

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        [Authorize]
        [HttpPut("updatelabel/{LabelId}")]
        public IActionResult UpdateLabel(int LabelId, LabelPostModel labelPost)
        {
            try
            {
                if (labelBL.UpdateLabel(LabelId, labelPost))
                {
                    return this.Ok(new { Success = true, message = "Label updated successfully", response = labelPost, LabelId });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Label is not updated" });
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }


        [Authorize]
        [HttpDelete("deleteNote/{LabelId}")]
        public IActionResult DeleteLabel(int LabelId)
        {
            try
            {
                if (labelBL.DeleteLabel(LabelId))
                {
                    return this.Ok(new { Success = true, message = "Label deleted successfully" });

                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Label with given ID not found" });
                }

            }
            catch (Exception e)
            {

                throw e;
            }
        }
        [Authorize]
        [HttpGet(" getallLabels")]
        public async Task<IActionResult> GetAllLabels()
        {

            try
            {
                int userID = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);

                var LabelList = new List<Label>();
                var NoteList = new List<Note>();
                LabelList = await labelBL.GetAllDatas(userID);
             

                return this.Ok(new { Success = true, message = $"GetAll Labels of UserId={userID} ", data = LabelList });
                return this.Ok(new { Success = true, message = $"GetAll Notes of UserId={userID} ", data = NoteList });


            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpGet("getAllLabelsbyNoteId/{NoteId}")]
        public async Task<IActionResult> GetAllLabelsByNoteId(int NoteId)
        {
            try
            {
                int userid = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);

            
              //  var userList = new List<User>();
                var LabelList = new List<Label>();
                var noteList = new List<Note>();
                LabelList = await labelBL.GetAllLabelsByNoteId(NoteId, userid);


                //   return this.Ok(new { Success = true, message = $"GetAll note successfull ", data = userList });
                return this.Ok(new { Success = true, message = $"GetAll Labels successfull ", data = LabelList });

                return this.Ok(new { Success = true, message = $"Note datas are: " , data = noteList});

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
