using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IDocumentService
    {
        Task<List<Document>> GetAll();
        Task<Document> GetById(string id);
        Task Create(Document model);
        Task Update(Document model);
        Task Delete(string id);
    }
}