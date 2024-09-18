using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Interfaces;
using Domain.Models;
using Domain.Interfaces;
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

        public async Task<List<Tred>> GetAll()
        {
            return await _repositoryWrapper.Tred.FindAll();
        }

        public async Task<Tred> GetById(int id)
        {
            var tred = await _repositoryWrapper.Tred
                .FindByCondition(x => x.TredId == id);
            return tred.First();
        }

        public async Task Create(Tred model)
        {
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
            var tred = await _repositoryWrapper.Tred
                .FindByCondition(x => x.TredId == id);

            _repositoryWrapper.Tred.Delete(tred.First());
            _repositoryWrapper.Save();
        }
    }
}
