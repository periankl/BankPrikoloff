using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Loan
    {
        public string LoanId { get; set; } = null!;
        public string? AccountId { get; set; }
        public int LoanTypeId { get; set; }
        public int StatusId { get; set; } = 1;
        public string DocumentId { get; set; } = null!;
        public decimal Amount { get; set; } = 0;
        public decimal RemarningAmount { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Document Document { get; set; } = null!;
        public virtual LoanType LoanType { get; set; } = null!;
        public virtual LoanStatus Status { get; set; } = null!;
    }
}