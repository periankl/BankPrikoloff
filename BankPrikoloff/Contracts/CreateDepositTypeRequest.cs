﻿namespace BankPrikoloff.Contracts
{
    public class CreateDepositTypeRequest
    {
        public string Name { get; set; } = null!;
        public decimal InterestRate { get; set; }
        public decimal? MinAmount { get; set; }
        public int? MinTerm { get; set; }
    }
}