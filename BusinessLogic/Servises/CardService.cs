using BusinessLogic.Interfaces;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Servises
{
    public class CardService : ICardService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public CardService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<Card>> GetAll()
        {
            return await _repositoryWrapper.Card.FindAll();
        }

        public async Task<Card> GetById(string id)
        {
            var model = await _repositoryWrapper.Card
                .FindByCondition(x => x.CardId == id);
            if (model is null || model.Count == 0)
            {
                throw new ArgumentException("Not found");
            }
            return model.First();
        }

        public async Task<Card> GetByCardNumber(string number)
        {
            var model = await _repositoryWrapper.Card
                .FindByCondition(x => x.CardNumber == number);
            if (model is null || model.Count == 0)
            {
                throw new ArgumentException("Not found");
            }
            return model.First();
        }

        public async Task Create(Card model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (string.IsNullOrEmpty(model.CardId))
            {
                throw new ArgumentException(nameof(model.CardId));
            }
            if (string.IsNullOrEmpty(model.AccountId))
            {
                throw new ArgumentException(nameof(model.AccountId));
            }
            if (string.IsNullOrEmpty(model.OwnerName))
            {
                throw new ArgumentException(nameof(model.OwnerName));
            }

            await _repositoryWrapper.Card.Create(model);
            _repositoryWrapper.Save();
        }

        public async Task Update(Card model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (string.IsNullOrEmpty(model.CardId))
            {
                throw new ArgumentException(nameof(model.CardId));
            }
            if (string.IsNullOrEmpty(model.AccountId))
            {
                throw new ArgumentException(nameof(model.AccountId));
            }
            if (string.IsNullOrEmpty(model.CardNumber))
            {
                throw new ArgumentException(nameof(model.CardNumber));
            }
            if (string.IsNullOrEmpty(model.Cvv))
            {
                throw new ArgumentException(nameof(model.Cvv));
            }
            if (string.IsNullOrEmpty(model.OwnerName))
            {
                throw new ArgumentException(nameof(model.OwnerName));
            }
            if (model.ExpDate < model.CreatedAt)
            {
                throw new ArgumentException(nameof(model.CreatedAt));
            }
            _repositoryWrapper.Card.Update(model);
            _repositoryWrapper.Save();
        }

        public async Task Delete(string id)
        {
            var model = await _repositoryWrapper.Card
                .FindByCondition(x => x.CardId == id);
            if (model is null || model.Count == 0)
            {
                throw new ArgumentException("Not found");
            }
            _repositoryWrapper.Card.Delete(model.First());
            _repositoryWrapper.Save();
        }
    }
}