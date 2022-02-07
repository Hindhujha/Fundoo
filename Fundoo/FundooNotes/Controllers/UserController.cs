using BusinessLayer.Interface;
using CommonLayer.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
                string result = this.userBL.Login(login);
                if (result != null)
                    return this.Ok(new { success = true, message = $"LogIn Successful {login.email}, Token = {result}" });
                else
                    return this.BadRequest(new { Success = false, message = "Invalid Username and Password" });
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        [HttpPut("ForgotPassword")]
        public IActionResult ForgotPassword(string email)
        {
            
            try
            {
                bool result = this.userBL.ForgotPassword(email);
                if(result==true)
                return this.Ok(new { success = true, message = $"Token generated.Please check your email,data={result}" });
                else
                return this.Ok(new { success = false, message = $"email not sent" });

            }
            catch (Exception e)
            {
                return BadRequest(new { success = false, e.Message });
            }

        }
        [Authorize]
        [HttpPut("resetpassword")]
        public ActionResult ResetPassword(string email, string password, string cPassword)
        {
            try
            {
                var UserEmailObject = User.Claims.First(x=>x.Type=="email").Value;
                if (password != cPassword)
                {
                    return this.Ok(new { success = false, message = $"your old password is same as current password" });
                }
                this.userBL.ResetPassword(UserEmailObject, password, cPassword);
                return this.Ok(new { success = true, message = $"password changes successfully to {email}" });
            }
            catch (Exception e)
            {
                return BadRequest(new { success = false, e.Message });
            }
        }
        [HttpGet("getallusers")]
        public ActionResult GetAllUsers()
        {
            try
            {
                var result = this.userBL.GetAllUsers();
                return this.Ok(new { success = true, message = $"Below are the User data", data = result });
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
