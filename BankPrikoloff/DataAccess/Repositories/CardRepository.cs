using DataAccess.Interfaces;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class CardRepository : RepositoryBase<Card>, ICardRepository
    {
        public CardRepository(BankContext bankContext) : base(bankContext) { }
    }
}
