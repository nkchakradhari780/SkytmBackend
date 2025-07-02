namespace SkytmBackend.Dto
{
    public class TListResponse
    {
        public List<TransactionCustom> Result { get; set; }

        public string Response {  get; set; }

        public string ResponseCode { get; set; }
    }
}
