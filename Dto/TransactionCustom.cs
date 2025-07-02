namespace SkytmBackend.Dto
{
    public class TransactionCustom
    {
        public int TransactionId { get; set; }
        public int UseId { get; set; }

        public int RecieverId { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverPhoneNumber { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
        public decimal TransactionAmount { get; set; }
        public decimal InitialAmount { get; set; }
    }

}
