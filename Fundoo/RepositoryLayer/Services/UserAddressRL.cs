using CommonLayer.UserAddressPostModel;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class UserAddressRL : IUserAddressRL
    {
        FundooDbContext dbContext;

        public UserAddressRL(FundooDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddUserAddress(UserAddressPostModel userAddress, int userId)
        {
            try
            {
                var UserAddress = dbContext.UserAddresses.FirstOrDefault(x => x.UserId == userId);
                UserAddress useraddress = new UserAddress();
                useraddress.UserId = userId;
                useraddress.AddressId = new UserAddress().AddressId;
                useraddress.State = userAddress.State;
                useraddress.Type = userAddress.Type;
                useraddress.City = userAddress.City;
                dbContext.UserAddresses.Add(useraddress);
                await dbContext.SaveChangesAsync();
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
                UserAddress useraddress = dbContext.UserAddresses.Where(e => e.UserId == userId).FirstOrDefault();
                useraddress.Type = userAddress.Type;
                useraddress.City = userAddress.City;
                useraddress.State = userAddress.State;

                dbContext.UserAddresses.Update(useraddress);
                await dbContext.SaveChangesAsync();

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
                return await dbContext.UserAddresses.Where(u => u.UserId == UserId)

            .Include(u => u.User)

            .ToListAsync();

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
                UserAddress address = dbContext.UserAddresses.Where(e => e.AddressId == AddressId).FirstOrDefault();
                dbContext.UserAddresses.Remove(address);
                 await dbContext.SaveChangesAsync();
            }
            catch(Exception e)
            {
                throw e;
            }
        }



    }

       
}
