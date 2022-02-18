using CommonLayer.User;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Experimental.System.Messaging;
using RepositoryLayer.Entities;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        FundooDbContext dbContext;
       
        public UserRL(FundooDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

     
        public void RegisterUser(UserPostModel userPostModel)
        {
            try
            {
                User user = new User();
                user.UserId = new User().UserId;
                user.fName = userPostModel.fName;
                user.lName = userPostModel.lName;
                user.email = userPostModel.email;
               
                user.address = userPostModel.address;
                user.password = userPostModel.password;
                user.cPassword = userPostModel.cPassword;
                user.phNo = userPostModel.phNo;

                user.registeredDate = DateTime.Now;
             
                dbContext.User.Add(user);
                dbContext.SaveChanges();
               
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string login(UserLogin userLogin)
        {
            try
            {
                User user = new User();
           
                var result = dbContext.User.Where(x => x.email == userLogin.email && x.password == userLogin.password).FirstOrDefault();
                int Id = result.UserId;
                if (result != null)
                {
                    return GenerateJWTToken(userLogin.email, Id);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

      
        private static string GenerateToken(string email)
        {
            if (email == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("email", email),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private static string GenerateJWTToken(string email, int userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("email", email),
                    new Claim("userId", userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }



        public bool ForgotPassword(ForgotUserModel validate)
        {
            try
            {

                var checkemail = dbContext.User.FirstOrDefault(e => e.email == validate.email);
         
                if (checkemail != null)
                {
                    MessageQueue queue;
                    //ADD MESSAGE TO QUEUE
                    if (MessageQueue.Exists(@".\Private$\FundooQueue"))
                    {
                        queue = new MessageQueue(@".\Private$\FundooQueue");
                    }
                    else
                    {
                        queue = MessageQueue.Create(@".\Private$\FundooQueue");
                    }

                    Message MyMessage = new Message();
                    MyMessage.Formatter = new BinaryMessageFormatter();
                    MyMessage.Body = GenerateJWTToken(validate.email, checkemail.UserId);
                    MyMessage.Label = "Forget Password Email";
                    queue.Send(MyMessage);
                    Message msg = queue.Receive();
                    msg.Formatter = new BinaryMessageFormatter();
                    EmailServices.SendMail(validate.email, msg.Body.ToString());
                    queue.ReceiveCompleted += new ReceiveCompletedEventHandler(msmqQueue_ReceiveCompleted);

                    queue.BeginReceive();
                    queue.Close();
                    return true;

                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }


      
        public void ResetPassword(string email, validations validate)
        {   
            try
            {
             
                    var result = dbContext.User.FirstOrDefault(a => a.email == email);
                    if (result != null)
                    {
                       result.password = validate.password;
                        result.cPassword = validate.cPassword;
                        dbContext.SaveChanges();
                    }
            }
            catch (Exception e)
            {
                    throw e;
            }
            

        }

        public List<User> GetAllUsers()
        {
            try
            {
                var result = dbContext.User.Include(u=>u.UserAddresses).ToList();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void msmqQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                MessageQueue queue = (MessageQueue)sender;
                Message msg = queue.EndReceive(e.AsyncResult);
                EmailServices.SendMail(e.Message.ToString(), GenerateToken(e.Message.ToString()));
                queue.BeginReceive();
            }
            catch (MessageQueueException ex)
            {
                if (ex.MessageQueueErrorCode ==

                    MessageQueueErrorCode.AccessDenied)
                {
                    Console.WriteLine("Access is denied. " +
                        "Queue might be a system queue.");
                }
                
            }
        }

       
       
    }
   
}


