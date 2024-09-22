using DataAccess.Repositories;
using Domain.Interfaces;
using Domain.Models;
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
        private IDocumentRepository _document;
        private IDepositTypeRepository _depositType;
        private IDepositRepository _deposit;
        private ILoanRepository _loan;
        private ILoanTypeRepository _loanType;
        private IOperationHistoryRepository _operationHistory;
        private IAccountRepository _account;
        private ICardRepository _card;
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
                if (_file == null)
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
                if (_message == null)
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
                if (_chat == null)
                {
                    _chat = new ChatRepository(_repoContext);
                }
                return _chat;
            }
        }

        public IDocumentRepository Document
        {
            get
            {
                if (_document == null)
                {
                    _document = new DocumentRepository(_repoContext);
                }
                return _document;
            }
        }

        public IDepositTypeRepository DepositType
        {
            get
            {
                if (_depositType == null)
                {
                    _depositType = new DepositTypeRepository(_repoContext);
                }
                return _depositType;
            }
        }

        public IDepositRepository Deposit
        {
            get
            {
                if (_deposit == null)
                {
                    _deposit = new DepositRepository(_repoContext);
                }
                return _deposit;
            }
        }

        public ILoanRepository Loan
        {
            get
            {
                if (_loan == null)
                {
                    _loan = new LoanRepository(_repoContext);
                }
                return _loan;
            }
        }

        public ILoanTypeRepository LoanType
        {
            get
            {
                if (_loanType == null)
                {
                    _loanType = new LoanTypeRepository(_repoContext);
                }
                return _loanType;
            }
        }

        public IOperationHistoryRepository OperationHistory
        {
            get
            {
                if (_operationHistory == null)
                {
                    _operationHistory = new OperationHistoryRepository(_repoContext);
                }
                return _operationHistory;
            }
        }

        public IAccountRepository Account
        {
            get
            {
                if (_account == null)
                {
                    _account = new AccountRepository(_repoContext);
                }
                return _account;
            }
        }

        public ICardRepository Card
        {
            get
            {
                if (_card == null)
                {
                    _card = new CardRepository(_repoContext);
                }
                return _card;
            }
        }
        public RepositoryWrapper(BankContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public async Task Save()
        {
            await _repoContext.SaveChangesAsync();
        }
    }
}