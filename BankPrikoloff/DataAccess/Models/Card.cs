using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Card
    {
        public Card()
        {
            OperationHistoryDestinationCards = new HashSet<OperationHistory>();
            OperationHistorySenderCards = new HashSet<OperationHistory>();
        }

        public string CardId { get; set; } = null!;
        public int TypeId { get; set; }
        public int CurrencyId { get; set; }
        public string AccountId { get; set; } = null!;
        public string CardNumber { get; set; } = null!;
        public DateTime ExpDate { get; set; }
        public string Cvv { get; set; } = null!;
        public string OwnerName { get; set; } = null!;
        public decimal Balance { get; set; }
        public bool Blocked { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? BlockedAt { get; set; }

        public virtual Account Account { get; set; } = null!;
        public virtual Currency Currency { get; set; } = null!;
        public virtual TypeOfCard Type { get; set; } = null!;
        public virtual ICollection<OperationHistory> OperationHistoryDestinationCards { get; set; }
        public virtual ICollection<OperationHistory> OperationHistorySenderCards { get; set; }
    }
}
