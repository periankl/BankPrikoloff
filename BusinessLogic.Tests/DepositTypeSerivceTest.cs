using BusinessLogic.Servises;
using Domain.Interfaces;
using Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
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
                DepositTypeId = 1,
                Name = "Test",
                InterestRate = 5,
                MinAmount = 100,
                MinTerm = 3,
                CreatedAt = DateTime.Now
            };
            await service.Create(newDepositType);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<DepositType>()), Times.Once);

        }
    }
}