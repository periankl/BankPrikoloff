using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IChatService
    {
        Task<List<Chat>> GetAll();
        Task<Chat> GetById(int id);
        Task Create(Chat model);
        Task Update(Chat model);
        Task Delete(int id);
    }
}
