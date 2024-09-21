using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Domain.Models
{
    public partial class Account
    {
        public Account()
        {
            Cards = new HashSet<Card>();
            Deposits = new HashSet<Deposit>();
            Loans = new HashSet<Loan>();
            OperationHistoryDestinationAccounts = new HashSet<OperationHistory>();
            OperationHistorySenderAccounts = new HashSet<OperationHistory>();
        }

        public string AccountId { get; set; } = null!;
        public string ClientId { get; set; } = null!;
        public int TypeId { get; set; }
        public int CurrencyId { get; set; }
        public int StatusId { get; set; }
        public decimal Balance { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual User Client { get; set; } = null!;
        public virtual Currency Currency { get; set; } = null!;
        public virtual AccountStatus Status { get; set; } = null!;
        public virtual AccountType Type { get; set; } = null!;
        public virtual ICollection<Card>? Cards { get; set; }
        public virtual ICollection<Deposit>? Deposits { get; set; }
        public virtual ICollection<Loan>? Loans { get; set; }
        public virtual ICollection<OperationHistory>? OperationHistoryDestinationAccounts { get; set; }
        public virtual ICollection<OperationHistory>? OperationHistorySenderAccounts { get; set; }
    }
}
