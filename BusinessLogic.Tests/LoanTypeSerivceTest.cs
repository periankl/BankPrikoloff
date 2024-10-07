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
    public class LoanTypeSerivceTest
    {
        private readonly LoanTypeService service;
        private readonly Mock<ILoanTypeRepository> userRepositoryMoq;

        public LoanTypeSerivceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            userRepositoryMoq = new Mock<ILoanTypeRepository>();

            repositoryWrapperMoq.Setup(x => x.LoanType).Returns(userRepositoryMoq.Object);

            service = new LoanTypeService(repositoryWrapperMoq.Object);
        }
        public static IEnumerable<object[]> GetIncorrectLoanTypes()
        {
            return new List<object[]>
            {
                new object[] { new LoanType() {Name = "", InterestRate = 3, MaxLoanAmount = -10000 } },
                new object[] { new LoanType() {Name = "", InterestRate = -3, MaxLoanAmount = 10000 } },
            };
        }

        [Theory]
        [MemberData(nameof(GetIncorrectLoanTypes))]
        public async Task CreateAsync_BadLoanType_ShouldThrowNullArgumentException(LoanType model)
        {
            var newLoanType = model;

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.Create(newLoanType));

            Assert.IsType<ArgumentException>(ex);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<LoanType>()), Times.Never());
        }


        [Fact]
        public async Task CreateAsync_NullLoanType_ShouldThrowNullArgumentException()
        {
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => service.Create(null));

            Assert.IsType<ArgumentNullException>(ex);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<LoanType>()), Times.Never());
        }

        [Fact]
        public async Task UpdateAsync_NullLoanType_ShouldThrowNullArgumentException()
        {
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => service.Update(null));

            Assert.IsType<ArgumentNullException>(ex);
            userRepositoryMoq.Verify(x => x.Update(It.IsAny<LoanType>()), Times.Never());
        }

        [Fact]
        public async Task CreateAsyncNewLoanTypeShouldCreateNewLoanType()
        {
            var newLoanType = new LoanType()
            {
                Name = "Test",
                InterestRate = 3,
                MaxLoanAmount = 10000
            };
            await service.Create(newLoanType);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<LoanType>()), Times.Once);
        }

        public static IEnumerable<object[]> UpdateIncorrectLoanTypes()
        {
            return new List<object[]>
            {
                new object[] { new LoanType() {LoanTypeId = 1, Name = "", InterestRate = 3, MaxLoanAmount = -10000 } },
                new object[] { new LoanType() {LoanTypeId = 1, Name = "", InterestRate = -3, MaxLoanAmount = 10000 } },
            };
        }

        [Theory]
        [MemberData(nameof(UpdateIncorrectLoanTypes))]
        public async Task UpdateAsync_BadLoanType_ShouldThrowNullArgumentException(LoanType model)
        {
            var newLoanType = model;

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.Update(newLoanType));

            Assert.IsType<ArgumentException>(ex);
            userRepositoryMoq.Verify(x => x.Update(It.IsAny<LoanType>()), Times.Never());
        }

        [Fact]
        public async Task UpdateAsyncNewLoanTypeShouldCreateNewLoanType()
        {
            var newLoanType = new LoanType()
            {
                Name = "Test",
                InterestRate = 3,
                MaxLoanAmount = 10000
            };
            await service.Update(newLoanType);
            userRepositoryMoq.Verify(x => x.Update(It.IsAny<LoanType>()), Times.Once);
        }

        [Fact]
        public async void GetByIdAsyncNullLoanTypeShouldThrowArgumentException()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.GetById(-1));

            Assert.IsType<ArgumentException>(ex);
            userRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<LoanType, bool>>>()), Times.Once);
        }

        [Fact]
        public async void DeleteAsyncNullLoanTypeShouldThrowArgumentException()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Delete(-1));

            Assert.IsType<ArgumentException>(ex);
            userRepositoryMoq.Verify(x => x.Delete(It.IsAny<LoanType>()), Times.Never);
        }
    }
}