using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using DataAccess.Models;

namespace DataAccess.Repositories
{
    public class DocumentRepository : RepositoryBase<Document>, IDocumentRepository
    {
        public DocumentRepository(BankContext bankContext) : base(bankContext) { }
    }
}
