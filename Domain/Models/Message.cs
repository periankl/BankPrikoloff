using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Message
    {
        public Message()
        {
            Files = new HashSet<File>();
        }

        public int? MessageId { get; set; }
        public int StatusId { get; set; } = 1;
        public int TredId { get; set; }
        public string ClientId { get; set; } = null!;

        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual User Client { get; set; } = null!;
        public virtual MessageStatus Status { get; set; } = null!;
        public virtual Tred Tred { get; set; } = null!;
        public virtual ICollection<File> Files { get; set; }
    }
}