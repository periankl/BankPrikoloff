﻿using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Currency
    {
        public Currency()
        {
            Accounts = new HashSet<Account>();
        }

        public int CurrencyId { get; set; }
        public string Name { get; set; } = null!;
        public decimal Course { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}