using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;


namespace BusinessLogic.Interfaces
{
    public interface IFileService
    {
        Task<List<Domain.Models.File>> GetAll();
        Task<Domain.Models.File> GetById(string id);
        Task Create(Domain.Models.File model);
        Task Update(Domain.Models.File model);
        Task Delete(string id);
    }
}
