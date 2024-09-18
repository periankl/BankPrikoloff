using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Chat
    {
        public Chat()
        {
            Treds = new HashSet<Tred>();
            Users = new HashSet<User>();
        }

        public int ChatId { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Tred> Treds { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
