﻿namespace BankPrikoloff.Contracts
{
    public class CreateOperationHistoryRequest
    {
        public string SenderAccountId { get; set; } = null!;
        public string? SenderCardId { get; set; }
        public string DestinationAccountId { get; set; } = null!;
        public string? DestinationCardId { get; set; }
        public int TypeId { get; set; }
        public decimal Amount { get; set; }
    }
}