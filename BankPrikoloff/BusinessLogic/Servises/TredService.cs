using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Interfaces;
using DataAccess.Models;
using DataAccess.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Servises
{
    public class TredService : ITredService
    {
         private IRepositoryWrapper _repositoryWrapper;

        public TredService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public Task<List<Tred>> GetAll()
        {
            return _repositoryWrapper.Tred.FindAll().ToListAsync();
        }

        public Task<Tred> GetById(int id)
        {
            var tred = _repositoryWrapper.Tred
                .FindByCondition(x => x.TredId == id).First();
            return Task.FromResult(tred);
        }

        public Task Create(Tred model)
        {
            _repositoryWrapper.Tred.Create(model);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }

        public Task Update(Tred model)
        {
            _repositoryWrapper.Tred.Update(model);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }

        public Task Delete(int id)
        {
            var tred = _repositoryWrapper.Tred
                .FindByCondition(x => x.TredId == id).First();
            
            _repositoryWrapper.Tred.Delete(tred);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }
    }
}
