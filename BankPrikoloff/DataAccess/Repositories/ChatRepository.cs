using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using DataAccess.Interfaces;

namespace DataAccess.Repositories
{
    public class ChatRepository : RepositoryBase<Chat>, IChatRepository
    {
        public ChatRepository(BankContext bankContext) : base(bankContext) { }
    }
}
