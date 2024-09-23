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
    public class DepositSerivceTest
    {
        private readonly DepositService service;
        private readonly Mock<IDepositRepository> userRepositoryMoq;

        public DepositSerivceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            userRepositoryMoq = new Mock<IDepositRepository>();

            repositoryWrapperMoq.Setup(x => x.Deposit).Returns(userRepositoryMoq.Object);

            service = new DepositService(repositoryWrapperMoq.Object);
        }
        public static IEnumerable<object[]> GetIncorrectDeposits()
        {
            return new List<object[]>
            {
                new object[] { new Deposit() {DepositId = "", DepositTypeId = 1, StatusId = 1, DocumentId = "", AccountId = "Test", Name = "", StartDate = DateTime.Now, EndDate = DateTime.Now.AddYears(1)} },
                new object[] { new Deposit() {DepositId = "Test", DepositTypeId = 1, StatusId = 1, DocumentId = "", AccountId = "Test", Name = "", StartDate = DateTime.Now, EndDate = DateTime.Now.AddYears(1)} },
                new object[] { new Deposit() {DepositId = "Test", DepositTypeId = 1, StatusId = 1, DocumentId = "Test", AccountId = "Test", Name = "", StartDate = DateTime.Now, EndDate = DateTime.Now.AddYears(1)} },
                new object[] { new Deposit() {DepositId = "", DepositTypeId = 1, StatusId = 1, DocumentId = "Test", AccountId = "Test", Name = "Test", StartDate = DateTime.Now, EndDate = DateTime.Now.AddYears(1)} },
                new object[] { new Deposit() {DepositId = "Test", DepositTypeId = 1, StatusId = 1, DocumentId = "Test", AccountId = "Test", Name = "Test", StartDate = DateTime.Now.AddYears(1), EndDate = DateTime.Now } }
            };
        }

        [Theory]
        [MemberData(nameof(GetIncorrectDeposits))]
        public async Task CreateAsync_BadDeposit_ShouldThrowNullArgumentException(Deposit model)
        {
            var newDeposit = model;

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.Create(newDeposit));

            Assert.IsType<ArgumentException>(ex);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<Deposit>()), Times.Never());
        }


        [Fact]
        public async Task CreateAsync_NullDeposit_ShouldThrowNullArgumentException()
        {
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => service.Create(null));

            Assert.IsType<ArgumentNullException>(ex);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<Deposit>()), Times.Never());
        }

        [Fact]

        public async Task CreateAsyncNewDepositShouldCreateNewDeposit()
        {
            var newDeposit = new Deposit()
            {
                DepositId = "Test",
                DepositTypeId = 1,
                StatusId = 1,
                DocumentId = "Test",
                AccountId = "Test",
                Name = "Test",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddYears(1)
            };
            await service.Create(newDeposit);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<Deposit>()), Times.Once);

        }
    }
}