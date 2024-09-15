using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using DataAccess.Interfaces;

namespace DataAccess.Repositories
{
    public class TredRepository : RepositoryBase<Tred>, ITredRepository
    {
        public TredRepository(BankContext bankContext) : base(bankContext) { }
    }
}
