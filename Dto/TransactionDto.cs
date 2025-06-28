namespace SkytmBackend.Dto
{
    public class TransactionDto
    {
        public string SenderPhoneNumber { get; set; }

        public string ReceiverPhoneNumber { get; set; }


        public decimal TransactionAmount { get; set; }
    }
}
