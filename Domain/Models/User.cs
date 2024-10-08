﻿using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class User
    {
        public User()
        {
            Accounts = new HashSet<Account>();
            Documents = new HashSet<Document>();
            Files = new HashSet<File>();
            Messages = new HashSet<Message>();
            Treds = new HashSet<Tred>();
        }

        public string? ClientId { get; set; }
        public int RoleId { get; set; } = 1;
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Patronomic { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int SeriesPasport { get; set; }
        public int NumberPasport { get; set; }
        public string Email { get; set; } = null!;
        public int ChatId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public virtual Chat? Chat { get; set; } = null!;
        public virtual Role? Role { get; set; } = null!;
        public virtual ICollection<Account>? Accounts { get; set; }
        public virtual ICollection<Document>? Documents { get; set; }
        public virtual ICollection<File>? Files { get; set; }
        public virtual ICollection<Message>? Messages { get; set; }
        public virtual ICollection<Tred>? Treds { get; set; }
    }
}