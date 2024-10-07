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
    public class DepositTypeSerivceTest
    {
        private readonly DepositTypeService service;
        private readonly Mock<IDepositTypeRepository> userRepositoryMoq;

        public DepositTypeSerivceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            userRepositoryMoq = new Mock<IDepositTypeRepository>();

            repositoryWrapperMoq.Setup(x => x.DepositType).Returns(userRepositoryMoq.Object);

            service = new DepositTypeService(repositoryWrapperMoq.Object);
        }
        public static IEnumerable<object[]> GetIncorrectDepositTypes()
        {
            return new List<object[]>
            {
                new object[] { new DepositType() { Name = "", InterestRate = 3, MinAmount = 10000, MinTerm = 5, CreatedAt = DateTime.Now} },
                new object[] { new DepositType() { Name = "", InterestRate = 3, MinAmount = -10000, MinTerm = 5, CreatedAt = DateTime.Now} },
                new object[] { new DepositType() { Name = "", InterestRate = -3, MinAmount = 10000, MinTerm = 5, CreatedAt = DateTime.Now} },
                new object[] { new DepositType() { Name = "", InterestRate = 3, MinAmount = 10000, MinTerm = -5, CreatedAt = DateTime.Now} }
            };
        }

        [Theory]
        [MemberData(nameof(GetIncorrectDepositTypes))]
        public async Task CreateAsync_BadDepositType_ShouldThrowNullArgumentException(DepositType model)
        {
            var newDepositType = model;

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.Create(newDepositType));

            Assert.IsType<ArgumentException>(ex);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<DepositType>()), Times.Never());
        }


        [Fact]
        public async Task CreateAsync_NullDepositType_ShouldThrowNullArgumentException()
        {
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => service.Create(null));

            Assert.IsType<ArgumentNullException>(ex);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<DepositType>()), Times.Never());
        }

        [Fact]

        public async Task CreateAsyncNewDepositTypeShouldCreateNewDepositType()
        {
            var newDepositType = new DepositType()
            {
                Name = "Test",
                InterestRate = 5,
                MinAmount = 100,
                MinTerm = 3,
            };
            await service.Create(newDepositType);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<DepositType>()), Times.Once);

        }

        public static IEnumerable<object[]> UpdateIncorrectDepositTypes()
        {
            return new List<object[]>
            {
                new object[] { new DepositType() { DepositTypeId = -1, Name = "", InterestRate = 3, MinAmount = 10000, MinTerm = 5, CreatedAt = DateTime.Now} },
                new object[] { new DepositType() { DepositTypeId = 2, Name = "", InterestRate = 3, MinAmount = -10000, MinTerm = 5, CreatedAt = DateTime.Now} },
                new object[] { new DepositType() { DepositTypeId = 2, Name = "", InterestRate = -3, MinAmount = 10000, MinTerm = 5, CreatedAt = DateTime.Now} },
                new object[] { new DepositType() { DepositTypeId = -1, Name = "", InterestRate = 3, MinAmount = 10000, MinTerm = -5, CreatedAt = DateTime.Now} }
            };
        }

        [Theory]
        [MemberData(nameof(UpdateIncorrectDepositTypes))]
        public async Task UpdateAsync_BadDeposit_ShouldThrowNullArgumentException(DepositType model)
        {
            var newDeposit = model;

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.Update(newDeposit));

            Assert.IsType<ArgumentException>(ex);
            userRepositoryMoq.Verify(x => x.Update(It.IsAny<DepositType>()), Times.Never());
        }

        [Fact]
        public async Task UpdateAsyncNullDepositShouldThrowNullArgumentException()
        {
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => service.Update(null));

            Assert.IsType<ArgumentNullException>(ex);
            userRepositoryMoq.Verify(x => x.Update(It.IsAny<DepositType>()), Times.Never());
        }

        [Fact]
        public async Task UpdateAsyncNewDepositTypeShouldCreateNewDepositType()
        {
            var newDepositType = new DepositType()
            {
                DepositTypeId = 1,
                Name = "Test",
                InterestRate = 5,
                MinAmount = 100,

                MinTerm = 3,
                CreatedAt = DateTime.Now
            };
            await service.Update(newDepositType);
            userRepositoryMoq.Verify(x => x.Update(It.IsAny<DepositType>()), Times.Once);

        }

        [Fact]
        public async void GetByIdAsyncNullDepositTypeShouldThrowArgumentException()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.GetById(-1));

            Assert.IsType<ArgumentException>(ex);
            userRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<DepositType, bool>>>()), Times.Once);
        }

        [Fact]
        public async void DeleteAsyncNullDepositTypeShouldThrowArgumentException()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Delete(-1));

            Assert.IsType<ArgumentException>(ex);
            userRepositoryMoq.Verify(x => x.Delete(It.IsAny<DepositType>()), Times.Never);
        }
    }
}