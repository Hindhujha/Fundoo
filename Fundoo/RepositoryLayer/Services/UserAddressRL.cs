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

        public bool AddUserAddress(UserAddressPostModel userAddress, int userId)
        {
            try
            {
                var UserAddress = dbContext.UserAddresses.FirstOrDefault(x => x.UserId == userId);
                UserAddress useraddress = new UserAddress();
                useraddress.UserId = userId;
                useraddress.AddressId = new UserAddress().AddressId;
                // userAddress1.AddressId = new UserAddress().AddressId;

                useraddress.State = userAddress.State;
                useraddress.City = userAddress.City;
                useraddress.Type = userAddress.Type;
                if (useraddress.Type == "Home")
                {
                    useraddress.Type = "Home";
                }
                else if (useraddress.Type == "Work")
                {
                    useraddress.Type = "Work";
                }
                else
                {
                    useraddress.Type = "Other";
                }
                var duplicates = dbContext.UserAddresses
                 .GroupBy(s => s.Type)
                 .Distinct();
                if(duplicates.Equals(useraddress.Type))
                {
                    return false;
                }
                else
                {
                    return true;
                    dbContext.UserAddresses.Add(useraddress);
                    dbContext.SaveChanges();
                }
                

               
                
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

       public async Task RemoveAddress(int AddressId, int UserId)
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
