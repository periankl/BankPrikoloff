﻿using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class DepositStatus
    {
        public DepositStatus()
        {
            Deposits = new HashSet<Deposit>();
        }

        public int StatusId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Deposit> Deposits { get; set; }
    }
}