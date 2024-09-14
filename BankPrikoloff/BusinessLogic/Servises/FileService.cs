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
    public class FileService : IFileService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public FileService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public Task<List<DataAccess.Models.File>> GetAll()
        {
            return _repositoryWrapper.File.FindAll().ToListAsync();
        }

        public Task<DataAccess.Models.File> GetById(string id)
        {
            var file = _repositoryWrapper.File.FindByCondition(x => x.FileId == id).First();
            return Task.FromResult(file);
        }

        public Task Create(DataAccess.Models.File model)
        {
            _repositoryWrapper.File.Create(model);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }

        public Task Update(DataAccess.Models.File model)
        {
            _repositoryWrapper.File.Update(model);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }

        public Task Delete(string id)
        {
            var file = _repositoryWrapper.File.FindByCondition(x => x.FileId == id).First();
            _repositoryWrapper.File.Delete(file);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }
    }
}
