using Domain.Models;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class DepositTypeRepository : RepositoryBase<DepositType>, IDepositTypeRepository
    {
        public DepositTypeRepository(BankContext bankContext) : base(bankContext) { }
    }
}
