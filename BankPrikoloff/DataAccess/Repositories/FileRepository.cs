using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using DataAccess.Models;

namespace DataAccess.Repositories
{
    public class FileRepository : RepositoryBase<Models.File>, IFileRepository
    {
        public FileRepository(BankContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
