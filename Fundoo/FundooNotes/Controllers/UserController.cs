using BusinessLayer.Interface;
using CommonLayer.User;
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
    public class UserController : ControllerBase
    {
        IUserBL userBL;
        FundooDbContext fundooDbContext;
            public  UserController(IUserBL userBL,FundooDbContext fundooDb)
            {
               this.userBL = userBL;
               this.fundooDbContext = fundooDb;
            }
        [HttpPost("register")]
        public ActionResult RegisterUser(UserPostModel userPostModel)
        {
            try
            {
                this.userBL.RegisterUser(userPostModel);
                return this.Ok(new { success = true, message = $"Registration Successful for your given Mail-ID  {userPostModel.email}" });
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        [HttpPost("login")]
        public ActionResult Login(UserLogin login)
        {
            try
            {
                this.userBL.Login(login);
                return this.Ok(new { success = true, message = $"Login Successful for your given Mail-ID  {login.email}" });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
