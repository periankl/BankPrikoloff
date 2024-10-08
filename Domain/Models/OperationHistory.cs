﻿using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class OperationHistory
    {
        public string OperationId { get; set; } = Guid.NewGuid().ToString("N").Substring(0, 9);
        public string SenderAccountId { get; set; } = null!;
        public string? SenderCardId { get; set; }
        public string DestinationAccountId { get; set; } = null!;
        public string? DestinationCardId { get; set; }
        public int TypeId { get; set; }
        public int StatusId { get; set; } = 1;
        public DateTime Date { get; set; } = DateTime.Now;
        public decimal Amount { get; set; }

        public virtual Account DestinationAccount { get; set; } = null!;
        public virtual Card? DestinationCard { get; set; }
        public virtual Account SenderAccount { get; set; } = null!;
        public virtual Card? SenderCard { get; set; }
        public virtual OperationStatus Status { get; set; } = null!;
        public virtual OperationType Type { get; set; } = null!;
    }
}