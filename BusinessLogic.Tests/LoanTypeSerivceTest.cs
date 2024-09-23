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
                new object[] { new LoanType() { Name = "", InterestRate = 3, MaxLoanAmount = 10000} },
                new object[] { new LoanType() {Name = "", InterestRate = 3, MaxLoanAmount = 10000 } },
                new object[] { new LoanType() {Name = "", InterestRate = 3, MaxLoanAmount = -10000 } },
                new object[] { new LoanType() {Name = "", InterestRate = -3, MaxLoanAmount = 10000 } },
                new object[] { new LoanType() {Name = "", InterestRate = 3, MaxLoanAmount = 10000 } }


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
    }
}