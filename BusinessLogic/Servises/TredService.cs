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
    public class TredService : ITredService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public TredService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<Tred>> GetAll()
        {
            return await _repositoryWrapper.Tred.FindAll();
        }

        public async Task<Tred> GetById(int id)
        {
            var model = await _repositoryWrapper.Tred
                .FindByCondition(x => x.TredId == id);
            if (model is null || model.Count == 0)
            {
                throw new ArgumentException("Not found");
            }
            return model.First();
        }

        public async Task Create(Tred model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (model.IsClosed == true & model.ClosedAt == null)
            {
                throw new ArgumentException(nameof(model.IsClosed));
            }
            if (model.IsClosed == false & model.ClosedAt != null)
            {
                throw new ArgumentException(nameof(model.IsClosed));
            }
            if (model.CreatedAt > model.ClosedAt)
            {
                throw new ArgumentException(nameof(model.CreatedAt));
            }
            await _repositoryWrapper.Tred.Create(model);
            _repositoryWrapper.Save();
        }

        public async Task Update(Tred model)
        {
            _repositoryWrapper.Tred.Update(model);
            _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var model = await _repositoryWrapper.Tred
                .FindByCondition(x => x.TredId == id);
            if (model is null || model.Count == 0)
            {
                throw new ArgumentException("Not found");
            }
            _repositoryWrapper.Tred.Delete(model.First());
            _repositoryWrapper.Save();
        }
    }
}