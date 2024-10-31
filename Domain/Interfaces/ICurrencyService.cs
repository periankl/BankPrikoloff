using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface ICurrencyService
    {
        Task<List<Currency>> GetAll();
        Task<Currency> GetById(int id);
        Task Create(Currency model);
        Task Update(Currency model);
        Task Delete(int id);
    }
}