using Microsoft.AspNetCore.Mvc;
using SkytmBackend.Data;
using SkytmBackend.Dto;

namespace SkytmBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAll")]
        
        public ApiResponse GetAllUsers(string phoneNumber)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var user = _context.Users.FirstOrDefault(u => u.PhoneNumber == phoneNumber);


                if (user == null || user.IsAdmin == false)
                {
                    response.Result = null;
                    response.Response = "Only admin can access this.";
                    response.ResponseCode = "401";
                    return response;
                }
                else
                {
                    response.Result = user;
                    response.Response = "Record fetched SuccessFully !!";
                    response.ResponseCode = "200";
                }

            } 
            catch (Exception ex)
            {
                response.Result = null;
                response.Response = "Bad Request" + ex.Message;
                response.ResponseCode = "400";
            }
            return response;
        }

        [HttpGet("WalletBalance")]

        public WalletResponse GetWalletBalance(string phoneNumber)
        {
            WalletResponse response = new WalletResponse();

            try
            {
                var user = _context.Users.FirstOrDefault(u => u.PhoneNumber == phoneNumber);

                if(user == null)
                {
                    response.Amount = 0;
                    response.Response = "Only admin can access this.";
                    response.ResponseCode = "401";
                    return response;
                }
                else
                {
                    response.Amount = user.Amount;
                    response.Response = "Record Fetched Successfully!!";
                    response.ResponseCode = "200";
                }
            } 
            catch (Exception ex)
            {
                response.Amount = 0;
                response.Response = "Bad Request" + ex.Message;
                response.ResponseCode = "400";
            }
            return response;
        }
    }
}
