using BusinessLayer.Interface;
using CommonLayer.UserAddressPostModel;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
   public class UserAddressBL:IUserAddressBL
    {
        IUserAddressRL userAddressRL;
        public UserAddressBL(IUserAddressRL userAddressRL)
        {
            this.userAddressRL = userAddressRL;
        }

        public async Task AddUserAddress(UserAddressPostModel userAddress, int userId)
        {
            try
            {
                await userAddressRL.AddUserAddress(userAddress,userId);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task UpdateUserAddress(UserAddressPostModel userAddress, int userId,int AddressId)
        {
            try
            {
                await userAddressRL.UpdateUserAddress(userAddress,userId,AddressId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

       public async Task<List<UserAddress>> GetAllUserAddress(int UserId)
        {
            try
            {
               return await userAddressRL.GetAllUserAddress(UserId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task RemoveAddress(int AddressId)
        {
            try
            {
                await userAddressRL.RemoveAddress(AddressId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
