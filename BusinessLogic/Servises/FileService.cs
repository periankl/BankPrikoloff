using BusinessLogic.Interfaces;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Servises
{
    public class FileService : IFileService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public FileService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<Domain.Models.File>> GetAll()
        {
            return await _repositoryWrapper.File.FindAll();
        }

        public async Task<Domain.Models.File> GetById(string id)
        {
            var model = await _repositoryWrapper.File
                .FindByCondition(x => x.FileId == id);
            if (model is null || model.Count == 0)
            {
                throw new ArgumentException("Not found");
            }
            return model.First();
        }

        public async Task Create(Domain.Models.File model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (string.IsNullOrEmpty(nameof(model.MessageId)) || model.MessageId <= 0)
            {
                throw new ArgumentNullException(nameof(model.MessageId));
            }
            await _repositoryWrapper.File.Create(model);
            await _repositoryWrapper.Save();
        }

        public async Task Update(Domain.Models.File model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (string.IsNullOrEmpty(model.FileId))
            {
                throw new ArgumentException(nameof(model.FileId));
            }
            if (string.IsNullOrEmpty(model.FilePath))
            {
                throw new ArgumentException(nameof(model.FilePath));
            }
            if (string.IsNullOrEmpty(model.ClientId))
            {
                throw new ArgumentException(nameof(model.ClientId));
            }
            if (string.IsNullOrEmpty(nameof(model.MessageId)) || model.MessageId <= 0)
            {
                throw new ArgumentNullException(nameof(model.MessageId));
            }
            await _repositoryWrapper.File.Update(model);
            await _repositoryWrapper.Save();
        }

        public async Task Delete(string id)
        {
            var model = await _repositoryWrapper.File
                .FindByCondition(x => x.FileId == id);
            if (model is null || model.Count == 0)
            {
                throw new ArgumentException("Not found");
            }
            await _repositoryWrapper.File.Delete(model.First());
            await _repositoryWrapper.Save();
        }
    }
}