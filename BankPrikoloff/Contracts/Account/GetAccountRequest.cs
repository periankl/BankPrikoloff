﻿namespace BankPrikoloff.Contracts
{
    public class GetAccountRequest
    {
        public string AccountId { get; set; } = null!;
        public string ClientId { get; set; } = null!;
        public int TypeId { get; set; }
        public int CurrencyId { get; set; }
        public int StatusId { get; set; }
        public decimal Balance { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int AccountNumber { get; set; }
    }
}