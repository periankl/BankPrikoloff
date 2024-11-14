using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface ICardService
    {
        Task<List<Card>> GetAll();
        Task<Card> GetById(string id);
        Task<List<Card>> GetByAccountId(string accountId);
        Task<Card> GetByCardNumber(string number);
        Task Create(Card model);
        Task Update(Card model);
        Task Delete(string id);
    }
}