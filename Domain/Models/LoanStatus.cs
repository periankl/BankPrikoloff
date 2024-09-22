using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class LoanStatus
    {
        public LoanStatus()
        {
            Loans = new HashSet<Loan>();
        }

        public int StatusId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Loan> Loans { get; set; }
    }
}