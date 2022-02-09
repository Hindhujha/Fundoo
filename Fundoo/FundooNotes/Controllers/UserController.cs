using BusinessLayer.Interface;
using CommonLayer.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

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
              this.userBL.ForgotPassword(email);              
                return this.Ok(new { success = true, message = $"Token generated.Please check your email" });              
            }
            catch (Exception e)
            {
                return BadRequest(new { success = false, e.Message });
            }

        }
        [AllowAnonymous]
        [HttpPut("Reset Password")]
        public ActionResult ResetPassword(string Email, string Password, string cpassword)
        {
            try
            {
                if (Password != cpassword)
                {
                    return this.BadRequest(new { success = false, message = $"Passwords are not same" });
                }
                var Identity = User.Identity as ClaimsIdentity;
                //var UserEmailObject = User.Claims.First(x => x.Type == "Email").Value;
                if (Identity != null)
                {
                    IEnumerable<Claim> claims = Identity.Claims;
                    var UserEmailObject = claims.Where(p => p.Type == @"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").FirstOrDefault()?.Value;
                    this.userBL.ResetPassword(UserEmailObject, Password, cpassword);
                    return Ok(new { success = true, message = "Password Changed Sucessfully", email = $"{Email}" });
                }

                //  this.userBL.ResetPassword(UserEmailObject, Password, cpassword);
                return this.BadRequest(new { success = false, message = $"Password changed UnSuccessfully {Email}" });
            }
            catch (Exception e)
            {
                throw e;
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
