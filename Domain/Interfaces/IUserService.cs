﻿using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAll();
        Task<User> GetById(string id);
        Task<User> GetByLogin(string login, string password);
        Task<User> GetByEmail(string email);
        Task Create(User model);
        Task Update(User model);
        Task Delete(string id);
    }
}