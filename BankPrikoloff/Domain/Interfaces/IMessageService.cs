using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace BusinessLogic.Interfaces
{
    public interface IMessageService
    {
        Task<List<Message>> GetAll();
        Task<Message> GetById(int id);
        Task Create(Message model);
        Task Update(Message model);
        Task Delete(int id);
    }
}
