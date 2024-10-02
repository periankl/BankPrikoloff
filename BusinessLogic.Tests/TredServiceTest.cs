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
    public class TredSerivceTest
    {
        private readonly TredService service;
        private readonly Mock<ITredRepository> userRepositoryMoq;

        public TredSerivceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            userRepositoryMoq = new Mock<ITredRepository>();

            repositoryWrapperMoq.Setup(x => x.Tred).Returns(userRepositoryMoq.Object);

            service = new TredService(repositoryWrapperMoq.Object);
        }
        public static IEnumerable<object[]> GetIncorrectTreds()
        {
            return new List<object[]>
            {
                new object[] { new Tred() { IsClosed = true, ClosedAt = null} },
                new object[] { new Tred() { CreatedAt = DateTime.Now, IsClosed = true, ClosedAt = DateTime.Now.AddDays(-5)} },
                new object[] { new Tred() { CreatedAt = DateTime.Now, IsClosed = false, ClosedAt = DateTime.Now.AddDays(1)} },



            };
        }

        [Theory]
        [MemberData(nameof(GetIncorrectTreds))]
        public async Task CreateAsync_BadTred_ShouldThrowNullArgumentException(Tred model)
        {
            var newTred = model;

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.Create(newTred));

            Assert.IsType<ArgumentException>(ex);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<Tred>()), Times.Never());
        }


        [Fact]
        public async Task CreateAsync_NullTred_ShouldThrowNullArgumentException()
        {
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => service.Create(null));

            Assert.IsType<ArgumentNullException>(ex);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<Tred>()), Times.Never());
        }

        [Fact]

        public async Task CreateAsyncNewTredShouldCreateNewTred()
        {
            var newTred = new Tred()
            {
                TredId = 1,
                ChatId = 1,
                CreatedAt = DateTime.Now,
                IsClosed = false,
                ClosedAt = null

            };
            await service.Create(newTred);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<Tred>()), Times.Once);

        }

        [Fact]
        public async void GetByIdAsyncNullTredShouldThrowArgumentException()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.GetById(-1));

            Assert.IsType<ArgumentException>(ex);
            userRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<Tred, bool>>>()), Times.Once);
        }

        [Fact]
        public async void DeleteAsyncNullTredShouldThrowArgumentException()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Delete(-1));

            Assert.IsType<ArgumentException>(ex);
            userRepositoryMoq.Verify(x => x.Delete(It.IsAny<Tred>()), Times.Never);
        }
    }
}