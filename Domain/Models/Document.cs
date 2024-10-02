using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Document
    {
        public Document()
        {
            Deposits = new HashSet<Deposit>();
            Loans = new HashSet<Loan>();
        }

        public string DocumentId { get; set; } = null!;
        public string ClientId { get; set; } = null!;
        public int TypeId { get; set; }
        public string Name { get; set; } = null!;
        public string Path { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? DeletedAt { get; set; }

        public virtual User Client { get; set; } = null!;
        public virtual DocumentType Type { get; set; } = null!;
        public virtual ICollection<Deposit> Deposits { get; set; }
        public virtual ICollection<Loan> Loans { get; set; }
    }
}