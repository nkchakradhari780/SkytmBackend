using Microsoft.AspNetCore.Mvc;
using SkytmBackend.Data;
using SkytmBackend.Dto;

namespace SkytmBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : Controller
    {
        private readonly AppDbContext _context;

        public TransactionsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("SendMoney")]

        public TransactionResponse PayMoney(TransactionDto dto)
        {
            TransactionResponse response = new TransactionResponse(); try
            {
                var sender = _context.Users.FirstOrDefault(u => u.PhoneNumber == dto.SenderPhoneNumber);

                var reciever = _context.Users.FirstOrDefault(u => u.PhoneNumber == dto.ReceiverPhoneNumber);

                if (sender == null || reciever == null)
                {
                    response.SenderPhoneNumber = null;
                    response.ReceiverPhoneNumber = null;
                    response.Response = "Sender Or Reciever Not Found";
                    response.ResponseCode = "200";
                    return response;
                }

                if (sender.Amount < dto.TransactionAmount)
                {
                    response.Response = "Insufficient Balance";
                    response.ResponseCode = "200";
                    return response;
                }

                decimal senderInitial = sender.Amount;
                decimal receiverInitial = reciever.Amount;

                sender.Amount -= dto.TransactionAmount;
                reciever.Amount += dto.TransactionAmount;

                _context.Transactions.Add(new Models.Transaction
                {
                    UserId = sender.userId,
                    ReceiverId = reciever.userId,
                    TransactionType = "Debit",
                    InitialAmount = senderInitial,
                    TransferAmount = dto.TransactionAmount
                });

                _context.Transactions.Add(new Models.Transaction
                {
                    UserId = reciever.userId,
                    ReceiverId = reciever.userId,
                    TransactionType = "Credit",
                    InitialAmount = receiverInitial,
                    TransferAmount = dto.TransactionAmount
                });

                _context.SaveChanges();
                response.Response = "Pyment SuccessFull";
                response.ResponseCode = "200";
                return response;

            }
            catch (Exception ex)
            {
                response.ResponseCode = "400";
                response.Response = ex.Message;
                return response;
            }
        }
    }
}
