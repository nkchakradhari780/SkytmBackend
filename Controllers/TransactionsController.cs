using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
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
            TransactionResponse response = new TransactionResponse(); 
            try
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

        [HttpGet("GetTransaction")]
        public TListResponse getTransaction(string phoneNumber)
        {
            TListResponse response = new TListResponse();
            List<TransactionCustom> rlist = new List<TransactionCustom>();
            TransactionCustom obj = new TransactionCustom();

            try
            {
                var user = _context.Users.FirstOrDefault(u => u.PhoneNumber == phoneNumber);

                if (user == null)
                {
                    response.Result = null;
                    response.Response = "User not Found";
                    response.ResponseCode = "200";
                    return response;
                }
                else
                {
                    var history = _context.Transactions
                                            .Where(t => t.UserId == user.userId)
                                            .OrderByDescending(t => t.TransactionDate)
                                            .ToList();
                    foreach (var transaction in history)
                    {
                        obj = new TransactionCustom();
                        var Receiverdata = _context.Users.Where(u => u.userId == transaction.ReceiverId).FirstOrDefault();

                        obj.TransactionDate = transaction.TransactionDate;
                        obj.UseId = transaction.UserId;
                        obj.TransactionId = transaction.TransactionId;
                        obj.ReceiverPhoneNumber = Receiverdata.PhoneNumber;
                        obj.ReceiverName = Receiverdata.userName;
                        obj.TransactionType = transaction.TransactionType;
                        obj.RecieverId = transaction.ReceiverId;
                        obj.InitialAmount = transaction.InitialAmount;
                        obj.TransactionAmount = transaction.TransferAmount;

                        rlist.Add(obj);

                    }
                    response.Result = rlist;
                    response.Response = "200";
                    response.ResponseCode = "History fetched successfully";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Result = null;
                response.ResponseCode = "400";
                response.Response = "Error Fetching Transaction History: " + ex.Message;
                return response;
            }

        }

        [HttpDelete("DeleteTransactionById")]
        public TListResponse DeleteTransactionById(int tid)
        {
            TListResponse response = new TListResponse();

            try
            {


                var transactions = _context.Transactions.Where(t => t.TransactionId == tid);
                _context.Transactions.RemoveRange(transactions);
                _context.SaveChanges();

                response.Result = null;
                response.Response = "Transaction record deleted";
                response.ResponseCode = "200";
                return response;

            }
            catch (Exception ex)
            {
                response.Result = null;
                response.ResponseCode = "400";
                response.Response = "Error Deleting History: " + ex.ToString();
                
            }
            return response;
        }

        [HttpDelete("deleteHistory")]
        public TListResponse DeleteHistory(string phoneNumber)
        {
            TListResponse response = new TListResponse();
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.PhoneNumber == phoneNumber);
                if (user == null)
                {
                    response.Result = null;
                    response.ResponseCode = "200";
                    response.Response = "User Not Found: ";
                    return response;
                }
                else
                {
                    var transactions = _context.Transactions.Where(t => t.UserId == user.userId);
                    _context.Transactions.RemoveRange(transactions);
                    _context.SaveChanges();

                    response.Result = null;
                    response.ResponseCode = "200";
                    response.Response = "History Deleted Successfully!! " ;
                    return response;
                }

            }
            catch (Exception ex)
            {
                response.Result = null;
                response.ResponseCode = "400";
                response.Response = "Error Deleting History: " + ex.ToString();
            }
            return response;
        }
    }
}
