using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class MessageStatus
    {
        public MessageStatus()
        {
            Messages = new HashSet<Message>();
        }

        public int StatusId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Message> Messages { get; set; }
    }
}
