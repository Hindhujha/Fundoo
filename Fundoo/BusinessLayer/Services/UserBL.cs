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

        public void Login(UserLogin login)
        {
            try
            {
                userRL.login(login);

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

        public bool ForgotPassword(string email)
        {
            try
            {
                return userRL.ForgotPassword(email);
            }
            catch(Exception e)
            {
                throw e;
            }
        }


        public void ResetPassword(string email, string password, string cPassword)
        {
            try
            {
                userRL.ResetPassword(email, password, cPassword);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
