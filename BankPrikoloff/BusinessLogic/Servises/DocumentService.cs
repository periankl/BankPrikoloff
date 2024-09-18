using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Interfaces;
using Domain.Models;
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

        public async Task<List<Document>> GetAll()
        {
            return await _repositoryWrapper.Document.FindAll();
        }

        public async Task<Document> GetById(string id)
        {
            var document = await _repositoryWrapper.Document
                .FindByCondition(x => x.DocumentId == id);
            return document.First();
        }

        public async Task Create(Document model)
        {
            await _repositoryWrapper.Document.Create(model);
            _repositoryWrapper.Save();
        }

        public async Task Update(Document model)
        {
            _repositoryWrapper.Document.Update(model);
            _repositoryWrapper.Save();
        }

        public async Task Delete(string id)
        {
            var document = await _repositoryWrapper.Document
                .FindByCondition(x => x.DocumentId == id);

            _repositoryWrapper.Document.Delete(document.First());
            _repositoryWrapper.Save();
        }
    }
}
