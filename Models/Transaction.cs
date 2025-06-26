﻿namespace SkytmBackend.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int UserId { get; set; }
        public int ReceiverId { get; set; }
        public string TransactionType { get; set; }  //Debit , Credit , Wallet
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
        public decimal InitialAmount { get; set; }
        public decimal TransferAmount { get; set; }
    }
}
