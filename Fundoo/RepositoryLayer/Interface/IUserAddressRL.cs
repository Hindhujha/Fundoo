using CommonLayer.UserAddressPostModel;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IUserAddressRL
    {
        public bool AddUserAddress(UserAddressPostModel userAddress, int userId);

        Task UpdateUserAddress(UserAddressPostModel userAddress, int userId, int AddressId);

        Task<List<UserAddress>> GetAllUserAddress(int UserId);

        Task RemoveAddress(int AddressId, int UserId);


    }
}
