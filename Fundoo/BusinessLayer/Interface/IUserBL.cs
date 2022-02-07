using CommonLayer.User;
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

        bool ForgotPassword(string email);
        void ResetPassword(string email, string password, string cPassword);

        List<User> GetAllUsers();
    }
}
