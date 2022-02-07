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
        void Login(UserLogin login);
    }
}
