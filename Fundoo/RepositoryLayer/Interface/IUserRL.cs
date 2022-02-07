﻿using CommonLayer.User;
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

        public bool ForgotPassword(string email);

        public void ResetPassword(string email, string password, string cPassword);

        List<User> GetAllUsers();

    }

}
