namespace SkytmBackend.Dto
{
    public class TransactionResponse
    {
        public string SenderPhoneNumber { get; set; }

        public string ReceiverPhoneNumber { get; set; }

        public string TransactionType { get; set; }

        public string TransactionAmount { get; set; }

        public string Response { get; set; }
        
        public string ResponseCode { get; set; }
    }
}
