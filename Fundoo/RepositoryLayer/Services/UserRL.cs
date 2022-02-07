using CommonLayer.User;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;


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
                if (result != null)
                    return GenerateJwtToken(userLogin.email, user.UserId);
                else
                    return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        private static string GenerateJwtToken(string email, int userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenkey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim("email",email),
                        new Claim("userId",userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials=
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenkey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);
        }
            
    
    }
}


