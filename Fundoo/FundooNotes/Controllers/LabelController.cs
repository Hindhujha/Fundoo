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

namespace FundooNotes.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class LabelController : ControllerBase
    {
        ILabelBL labelBL;
        public LabelController(ILabelBL labelBL)
        {
            this.labelBL = labelBL;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateLabel(LabelPostModel labelModel, int NotesId)
        {
            try
            {
                
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userId.Value);

                List<Label> labels = new List<Label>();

               
               labels= await labelBL.CreateLabel(labelModel, NotesId, UserId);

                return this.Ok(new { success = true, message = "Label added successfully", response = labelModel, NotesId });

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [Authorize]
        [HttpGet("GetLabelsByNoteID/{NotesId}")]
        public IEnumerable GetLabelsByNoteID(int NotesId)
        {
            try
            {
                int userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                return labelBL.GetLabelsByNoteID(userID, NotesId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
