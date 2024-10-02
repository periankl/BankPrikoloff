using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Deposit
    {
        public string DepositId { get; set; } = null!;
        public int DepositTypeId { get; set; }
        public int StatusId { get; set; } = 1;
        public string DocumentId { get; set; } = null!;
        public string AccountId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime? EndDate { get; set; }

        public virtual Account Account { get; set; } = null!;
        public virtual DepositType DepositType { get; set; } = null!;
        public virtual Document Document { get; set; } = null!;
        public virtual DepositStatus Status { get; set; } = null!;
    }
}