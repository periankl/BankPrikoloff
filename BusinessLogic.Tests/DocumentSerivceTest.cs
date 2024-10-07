using BusinessLogic.Servises;
using Domain.Interfaces;
using Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Tests
{
    public class DocumentSerivceTest
    {
        private readonly DocumentService service;
        private readonly Mock<IDocumentRepository> userRepositoryMoq;

        public DocumentSerivceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            userRepositoryMoq = new Mock<IDocumentRepository>();

            repositoryWrapperMoq.Setup(x => x.Document).Returns(userRepositoryMoq.Object);

            service = new DocumentService(repositoryWrapperMoq.Object);
        }
        public static IEnumerable<object[]> GetIncorrectDocuments()
        {
            return new List<object[]>
            {
                new object[] { new Document() {ClientId = ""} }
            };
        }

        [Theory]
        [MemberData(nameof(GetIncorrectDocuments))]
        public async Task CreateAsync_BadDocument_ShouldThrowNullArgumentException(Document model)
        {
            var newDocument = model;

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.Create(newDocument));

            Assert.IsType<ArgumentException>(ex);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<Document>()), Times.Never());
        }


        [Fact]
        public async Task CreateAsync_NullDocument_ShouldThrowNullArgumentException()
        {
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => service.Create(null));

            Assert.IsType<ArgumentNullException>(ex);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<Document>()), Times.Never());
        }

        [Fact]

        public async Task CreateAsyncNewDocumentShouldCreateNewDocument()
        {
            var newDocument = new Document()
            {
                ClientId = "Test",
            };
            await service.Create(newDocument);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<Document>()), Times.Once);

        }

        public static IEnumerable<object[]> UpdateIncorrectDocuments()
        {
            return new List<object[]>
            {
                new object[] { new Document() {DocumentId = "", ClientId = "", Name = "", Path = "", CreatedAt = DateTime.MaxValue} },
                new object[] { new Document() {DocumentId = "Test", ClientId = "", Name = "", Path = "", CreatedAt = DateTime.MaxValue} },
                new object[] { new Document() {DocumentId = "", ClientId = "Test", Name = "", Path = "", CreatedAt = DateTime.MaxValue} },
                new object[] { new Document() {DocumentId = "", ClientId = "", Name = "", Path = "Test", CreatedAt = DateTime.MaxValue} },

            };
        }

        [Theory]
        [MemberData(nameof(UpdateIncorrectDocuments))]
        public async Task UpdateAsyncBadDeposit_ShouldThrowNullArgumentException(Document model)
        {
            var newDeposit = model;

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.Update(newDeposit));

            Assert.IsType<ArgumentException>(ex);
            userRepositoryMoq.Verify(x => x.Update(It.IsAny<Document>()), Times.Never());
        }

        [Fact]

        public async Task UpdateAsyncNewDocumentShouldCreateNewDocument()
        {
            var newDocument = new Document()
            {
                ClientId = "Test",
                Path = "Test",
                Name = "Test",

                CreatedAt = DateTime.Now

            };
            await service.Create(newDocument);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<Document>()), Times.Once);

        }

        [Fact]
        public async Task UpdateAsyncNullDepositShouldThrowNullArgumentException()
        {
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => service.Update(null));

            Assert.IsType<ArgumentNullException>(ex);
            userRepositoryMoq.Verify(x => x.Update(It.IsAny<Document>()), Times.Never());
        }

        [Fact]
        public async void GetByIdAsyncNullDocumentShouldThrowArgumentException()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.GetById("FFFFF"));

            Assert.IsType<ArgumentException>(ex);
            userRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<Document, bool>>>()), Times.Once);
        }

        [Fact]
        public async void DeleteAsyncNullDocumentShouldThrowArgumentException()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Delete("FFFF"));

            Assert.IsType<ArgumentException>(ex);
            userRepositoryMoq.Verify(x => x.Delete(It.IsAny<Document>()), Times.Never);
        }
    }
}