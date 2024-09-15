using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Interfaces;
using DataAccess.Models;
using DataAccess.Wrapper;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BusinessLogic.Servises
{
    public class DocumentService : IDocumentService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public DocumentService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public Task<List<Document>> GetAll()
        {
            return _repositoryWrapper.Document.FindAll().ToListAsync();
        }

        public Task<Document> GetById(string id)
        {
            var document = _repositoryWrapper.Document
                .FindByCondition(x => x.DocumentId == id).First();
            return Task.FromResult(document);
        }

        public Task Create(Document model)
        {
            _repositoryWrapper.Document.Create(model);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }

        public Task Update(Document model)
        {
            _repositoryWrapper.Document.Update(model);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }

        public Task Delete(string id)
        {
            var document = _repositoryWrapper.Document
                .FindByCondition(x => x.DocumentId == id).First();

            _repositoryWrapper.Document.Delete(document);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }
    }
}
