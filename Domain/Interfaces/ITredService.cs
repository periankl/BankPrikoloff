﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Interfaces;
using Domain.Models;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

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
