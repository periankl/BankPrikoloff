﻿using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Wrapper
{
    public interface IRepositoryWrapper
    {
        IUserRepository User {  get; }

        IFileRepository File { get; }

        IMessageRepository Message { get; }

        ITredRepository Tred { get; }

        IChatRepository Chat { get; }
        void Save();
    }
}