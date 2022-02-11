using BusinessLayer.Interface;
using CommonLayer.Notes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

      
    }
}
