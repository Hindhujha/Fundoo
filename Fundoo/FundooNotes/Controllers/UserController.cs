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
                    return this.Ok(new { success = true, message = $"LogIn Successful  {login.email}" });
                else
                    return this.BadRequest(new { Success = false, message = "Invalid Username and Password" });
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        [HttpPost("forgotpassword")]
        public IActionResult ForgotPassword(ForgotUserModel validate)
        {
            
            try
            {
                var result = fundooDbContext.User.FirstOrDefault(x => x.email == validate.email);
                if(result==null)
                {
                    return BadRequest(new { success = false, Message="Email is invalid" });
                }
                else
                {
                    this.userBL.ForgotPassword(validate);
                    return this.Ok(new { success = true, message = $"Token generated.Please check your email" });
                }
                           
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        [Authorize]
        [HttpPut("resetpassword")]
        public ActionResult ResetPassword(validations validate)
        {
            try
            {
                if (validate.password != validate.cPassword)
                {
                    return this.BadRequest(new { success = false, message = $"Passwords are not same" });
                }
                var identity=User.Identity as ClaimsIdentity;

                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    var UserEmailObject = claims.Where(p => p.Type == @"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").FirstOrDefault()?.Value;
                    if (UserEmailObject != null)
                    {
                        this.userBL.ResetPassword(UserEmailObject, validate);

                        return Ok(new { success = true, message = "Password Reset succesfully" });
                    }
                    else
                    {
                        return Ok(new { success = false, message = "Email is not Authorized" });
                    }
                }
                    return this.BadRequest(new { success = false, message = "Password Reset unsuccesfully" });
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
                //hindhusrii

            }
    }
}
