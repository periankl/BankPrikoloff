﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using DataAccess.Interfaces;

namespace DataAccess.Repositories
{
    public class MessageRepository : RepositoryBase<Message>, IMessageRepository 
    {
        public MessageRepository(BankContext bankContext) 
            : base(bankContext)
        {
        }
    }
}
