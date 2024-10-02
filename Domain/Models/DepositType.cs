using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class DepositType
    {
        public DepositType()
        {
            Deposits = new HashSet<Deposit>();
        }

        public int DepositTypeId { get; set; }
        public string Name { get; set; } = null!;
        public decimal InterestRate { get; set; }
        public decimal? MinAmount { get; set; }
        public int? MinTerm { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<Deposit> Deposits { get; set; }
    }
}