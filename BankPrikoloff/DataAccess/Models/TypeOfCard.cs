using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class TypeOfCard
    {
        public TypeOfCard()
        {
            Cards = new HashSet<Card>();
        }

        public int TypeId { get; set; }
        public string TypeName { get; set; } = null!;

        public virtual ICollection<Card> Cards { get; set; }
    }
}
