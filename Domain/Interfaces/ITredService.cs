using BusinessLogic.Interfaces;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface ITredService
    {
        Task<List<Tred>> GetAll();
        Task<Tred> GetById(int id);
        Task Create(Tred model);
        Task Update(Tred model);
        Task Delete(int id);
    }
}