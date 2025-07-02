using Microsoft.AspNetCore.Mvc;
using SkytmBackend.Data;
using SkytmBackend.Dto;
using SkytmBackend.Models;

namespace SkytmBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("Register")]

        public ApiResponse Register(RegisterDto dto)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                if (_context.Users.Any(u => u.PhoneNumber == dto.PhoneNumber))
                {
                    response.Result = null;
                    response.Response = "This phone number is already registered";
                    response.ResponseCode = "200";
                    return response;
                }
                var user = new User
                {
                    userName = dto.Username,
                    Email = dto.Email,
                    PhoneNumber = dto.PhoneNumber,
                    Gender = dto.Gender,
                    password = dto.password,
                    Amount = 0,
                    IsAdmin = dto.IsAdmin
                };


                try
                {
                    _context.Users.Add(user);
                    _context.SaveChanges();

                    response.Result = user;
                    response.Response = "Registered Successfully!!";
                    response.ResponseCode = "200";
                }
                catch (Exception ex)
                {
                    response.Result = null;
                    response.Response = " Hii Error Adding User" + ex.InnerException.Message;
                    response.ResponseCode = "400";

                }
            }
            catch (Exception ex)
            {
                response.Result = null;
                response.Response = "Bad Request" + ex.InnerException.Message;
                response.ResponseCode = "400";
            }

            return response;
        }

        [HttpPost("login")]

        public ApiResponse Login(LoginDto dto)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.PhoneNumber == dto.PhoneNumber);
                if(user == null)
                {
                    response.Result = null;
                    response.Response = "User Not Found !";
                    response.ResponseCode = "200";
                    return response;
                }
                if (user.password != dto.password)
                {
                    response.Result = null;
                    response.Response = "Incorrect Phone Number or password !";
                    response.ResponseCode = "200";
                    return response;
                }

                response.Result = user;
                response.ResponseCode = "200";
                response.Response = "LoggedIn Successfully";

            }
            catch (Exception ex)
            {
                response.Result = null;
                response.ResponseCode = "400";
                response.Response = "Error Loginin User: " + ex.ToString();
            }
            return response;
        }
    }
}
