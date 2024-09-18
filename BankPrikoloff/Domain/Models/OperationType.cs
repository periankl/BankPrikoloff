using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class OperationType
    {
        public OperationType()
        {
            OperationHistories = new HashSet<OperationHistory>();
        }

        public int TypeId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<OperationHistory> OperationHistories { get; set; }
    }
}
