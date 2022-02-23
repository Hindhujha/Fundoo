using BusinessLayer.Interface;
using CommonLayer.UserAddressPostModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{

    [ApiController]
    [Route("[controller]")]

    public class UserAddressController : ControllerBase
    {
        IUserAddressBL userAddressBL;

        FundooDbContext fundooDbContext;
        public UserAddressController(IUserAddressBL userAddressBL, FundooDbContext fundooDbContext)
        {
            this.userAddressBL = userAddressBL;
            this.fundooDbContext = fundooDbContext;
        }
        [Authorize]
        [HttpPost("adduserAddress")]
        public  IActionResult AddUserAddress(UserAddressPostModel userAddress)
        {
            try
            {

                int userid = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);

             
                 bool result=this.userAddressBL.AddUserAddress(userAddress, userid);
                if(result==false)
                {
                    return this.Ok(new { success = true, Message = $"Address is created" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Address Type exists, it can't be added" });
                }
                
              
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [Authorize]
        [HttpPut("updateUserAddress/{AddressId}")]
        public async Task<IActionResult> UpdateUserAddress(UserAddressPostModel userAddress, int AddressId)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userId.Value);

                await this.userAddressBL.UpdateUserAddress(userAddress, UserId, AddressId);


                return this.Ok(new { success = true, Message = $"Address is updated successfull" });
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        [Authorize]
        [HttpGet("getAllAddress")]
        public async Task<IActionResult> GetAllUserAddress()
        {
            try
            {
                int userid = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
                var addressList = new List<UserAddress>();
                addressList = await userAddressBL.GetAllUserAddress(userid);

                return this.Ok(new { Success = true, message = $"GetAll Address successfully ", data = addressList });

            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpDelete("deleteAddress/{AddressId}")]
        public async Task<IActionResult> RemoveAddress(int AddressId)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userId.Value);

                await this.userAddressBL.RemoveAddress(AddressId,UserId);
                return this.Ok(new { Success = true, message = $"Address deleted successfully " });

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
