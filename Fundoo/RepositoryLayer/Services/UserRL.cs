using CommonLayer.User;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

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
    }

}
