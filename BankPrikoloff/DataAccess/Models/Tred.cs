﻿using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Tred
    {
        public Tred()
        {
            Messages = new HashSet<Message>();
        }

        public int TredId { get; set; }
        public int ChatId { get; set; }
        public string? OperatorId { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsClosed { get; set; }
        public DateTime? ClosedAt { get; set; }

        public virtual Chat Chat { get; set; } = null!;
        public virtual User? Operator { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}