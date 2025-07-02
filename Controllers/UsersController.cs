using System.Runtime.Serialization;
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
        private object data;

        public object JsonConvert { get; private set; }

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

                if (user == null)
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

        [HttpGet("basic-user-list")]
        public UserBasicList GetUserBasicList()
        {
            UserBasicList response = new UserBasicList();
            List<BasicUser> ulist = new List<BasicUser>();
            BasicUser obj = new BasicUser();

            try
            {
                var users = _context.Users.ToList();
                if (users == null)
                {
                    response.Result = null;
                    response.Response = "Users list not found";
                    response.ResponseCode = "200";
                    return response;
                }
                else
                {
                    obj = new BasicUser();
                    Console.WriteLine("Received data:");

                    foreach (var user in users)
                    {
                        obj.phoneNumber = user.PhoneNumber;
                        obj.userId = user.userId;
                        obj.username = user.userName;

                        ulist.Add(obj);
                    }

                    response.Result = ulist;
                    response.ResponseCode = "200";
                    response.Response = "Users List Fetched Successfully!";
                }
            }
            catch (Exception ex)
            {
                response.Result = null;
                response.ResponseCode = "400";
                response.Response = "Error Fetching Users List: " + ex.ToString();
            }

            return response;

        }
    }

}
