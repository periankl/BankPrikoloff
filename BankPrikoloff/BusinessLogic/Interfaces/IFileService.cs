using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using DataAccess.Models;


namespace BusinessLogic.Interfaces
{
    public interface IFileService
    {
        Task<List<DataAccess.Models.File>> GetAll();
        Task<DataAccess.Models.File> GetById(string id);
        Task Create(DataAccess.Models.File model);
        Task Update(DataAccess.Models.File model);
        Task Delete(string id);
    }
}
