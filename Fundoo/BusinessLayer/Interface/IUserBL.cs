using CommonLayer.User;
using RepositoryLayer.Entities;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
      
        void RegisterUser(UserPostModel userPostModel);
        string Login(UserLogin login);

        bool ForgotPassword(ForgotUserModel validate);
        void ResetPassword(string email,validations validate);

        List<User> GetAllUsers();
    }
}
