using BusinessLayer.Interface;
using CommonLayer.User;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        IUserRL userRL;

        public UserBL(IUserRL userRL)
        {
            this.userRL =userRL ;
        }

        public string Login(UserLogin login)
        {
            try
            {
                return userRL.login(login);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void RegisterUser(UserPostModel userPostModel)
        {

            try
            {
                userRL.RegisterUser(userPostModel);
                  
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public bool ForgotPassword(ForgotUserModel validate)
        {
            try
            {
                return userRL.ForgotPassword(validate);
            }
            catch(Exception e)
            {
                throw e;
            }
        }


        public void ResetPassword(string email, validations validate)
        {
            try
            {
                userRL.ResetPassword(email,validate);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<User>GetAllUsers()
        {
            try
            {
                return userRL.GetAllUsers();

            }
            catch(Exception e)
            {
                throw e;
            }
        }
       

       
    }
}
