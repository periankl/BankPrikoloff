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
    public class CurrencyService : ICurrencyService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public CurrencyService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<Currency>> GetAll()
        {
            return await _repositoryWrapper.Currency.FindAll();
        }

        public async Task<Currency> GetById(int id)
        {
            var model = await _repositoryWrapper.Currency
                .FindByCondition(x => x.CurrencyId == id);
            if (model is null || model.Count == 0)
            {
                throw new ArgumentException("Not found");
            }
            return model.First();
        }

        public async Task Create(Currency model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (String.IsNullOrEmpty(model.Name) )
            {
                throw new ArgumentException(nameof(model.Name));
            }

            if (model.Course < 1)
            {
                throw new ArgumentException(nameof(model.Course));
            }
            
            await _repositoryWrapper.Currency.Create(model);
            await _repositoryWrapper.Save();
        }

        public async Task Update(Currency model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (String.IsNullOrEmpty(model.Name) || model.Name.Length > 10)
            {
                throw new ArgumentException(nameof(model.Name));
            }

            if (model.Course < 1)
            {
                throw new ArgumentException(nameof(model.Course));
            }

            await _repositoryWrapper.Currency.Update(model);
            await _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var model = await _repositoryWrapper.Currency
                .FindByCondition(x => x.CurrencyId == id);
            if (model is null || model.Count == 0)
            {
                throw new ArgumentException("Not found");
            }
            await _repositoryWrapper.Currency.Delete(model.First());
            await _repositoryWrapper.Save();
        }
    }
}