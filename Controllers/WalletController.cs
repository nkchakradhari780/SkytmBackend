using Microsoft.AspNetCore.Mvc;
using SkytmBackend.Data;
using SkytmBackend.Dto;

namespace SkytmBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletController : Controller
    {
        private readonly AppDbContext _context;

        public WalletController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("addMoney")]
        
        public WalletResponse AddMoney(AddMoneyDto dto)
        {
            WalletResponse response = new WalletResponse();

            try
            {
                var user = _context.Users.FirstOrDefault(u => u.PhoneNumber == dto.phoneNumber);

                if (user == null)
                {
                    response.ResponseCode = "401";
                    response.Response = "User Not Found";
                }
                else
                {
                    user.Amount += dto.amount;

                    _context.Transactions.Add(new Models.Transaction
                    {
                        UserId = user.userId,
                        ReceiverId = user.userId,
                        TransactionType = "Wallet",
                        InitialAmount = user.Amount - dto.amount,
                        TransferAmount = dto.amount
                    });

                    _context.SaveChanges();

                    response.Amount = user.Amount;
                    response.Response = "Amount Added SuccessFully";
                    response.ResponseCode = "200";
                    

                }
            }
            catch (Exception ex)
            {
                response.Response = "Error Adding Amount: " + ex.Message;
                response.ResponseCode = "400";
            }

            return response;
        }
    }
}
