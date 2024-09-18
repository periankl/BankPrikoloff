using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;

namespace DataAccess.Repositories
{
    public class FileRepository : RepositoryBase<Domain.Models.File>, IFileRepository
    {
        public FileRepository(BankContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
