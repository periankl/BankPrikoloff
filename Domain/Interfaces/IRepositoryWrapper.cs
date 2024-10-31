using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        IFileRepository File { get; }
        IMessageRepository Message { get; }
        ITredRepository Tred { get; }
        IChatRepository Chat { get; }
        IDocumentRepository Document { get; }
        IDepositTypeRepository DepositType { get; }
        IDepositRepository Deposit { get; }
        ILoanRepository Loan { get; }
        ILoanTypeRepository LoanType { get; }
        IOperationHistoryRepository OperationHistory { get; }
        IAccountRepository Account { get; }
        ICardRepository Card { get; }
        ICurrencyRepository Currency { get; }
        Task Save();
    }
}