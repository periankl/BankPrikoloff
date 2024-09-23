using BusinessLogic.Interfaces;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (string.IsNullOrEmpty(model.DocumentId))
            {
                throw new ArgumentException(nameof(model.DocumentId));
            }
            if (string.IsNullOrEmpty(model.ClientId))
            {
                throw new ArgumentException(nameof(model.ClientId));
            }
            if (string.IsNullOrEmpty(model.Name))
            {
                throw new ArgumentException(nameof(model.Name));
            }
            if (string.IsNullOrEmpty(model.Path))
            {
                throw new ArgumentException(nameof(model.Path));
            }



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