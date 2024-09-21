using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class LoanType
    {
        public LoanType()
        {
            Loans = new HashSet<Loan>();
        }

        public int LoanTypeId { get; set; }
        public string Name { get; set; } = null!;
        public decimal InterestRate { get; set; }
        public decimal MaxLoanAmount { get; set; }

        public virtual ICollection<Loan> Loans { get; set; }
    }
}
