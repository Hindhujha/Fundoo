
using CommonLayer.User;
using RepositoryLayer.Entities;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        void RegisterUser(UserPostModel userPostModel);

        public string login (UserLogin userLogin);

        public bool ForgotPassword(ForgotUserModel validate);

        public void ResetPassword(string email,validations validate);

        List<User> GetAllUsers();

    }

}
