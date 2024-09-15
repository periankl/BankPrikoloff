using DataAccess.Interfaces;
using DataAccess.Models;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Wrapper
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private BankContext _repoContext;
        private IUserRepository _user;
        private IFileRepository _file;
        private IMessageRepository _message;
        private ITredRepository _tred;
        private IChatRepository _chat;
        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_repoContext);
                }
                return _user;
            }
        }

        public IFileRepository File
        {
            get
            {
                if(_file == null)
                {
                    _file = new FileRepository(_repoContext);
                }
                return _file;
            }
        }

        public IMessageRepository Message
        {
            get
            {
                if(_message == null)
                {
                    _message = new MessageRepository(_repoContext);
                }
                return _message;
            }
        }

        public ITredRepository Tred
        {
            get
            {
                if (_tred == null)
                {
                    _tred = new TredRepository(_repoContext);
                }
                return _tred;
            }
        }

        public IChatRepository Chat
        {
            get
            {
                if(_chat == null)
                {
                    _chat = new ChatRepository(_repoContext);
                }
                return _chat;
            }
        }

        public RepositoryWrapper(BankContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
