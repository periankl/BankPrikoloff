using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Currency
    {
        public Currency()
        {
            Accounts = new HashSet<Account>();
            Cards = new HashSet<Card>();
        }

        public int CurrencyId { get; set; }
        public string Name { get; set; } = null!;
        public decimal Course { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Card> Cards { get; set; }
    }
}
