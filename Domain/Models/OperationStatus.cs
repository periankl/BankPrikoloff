using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class OperationStatus
    {
        public OperationStatus()
        {
            OperationHistories = new HashSet<OperationHistory>();
        }

        public int StatusId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<OperationHistory> OperationHistories { get; set; }
    }
}