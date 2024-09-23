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
    public class AccountSerivceTest
    {
        private readonly AccountService service;
        private readonly Mock<IAccountRepository> userRepositoryMoq;

        public AccountSerivceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            userRepositoryMoq = new Mock<IAccountRepository>();

            repositoryWrapperMoq.Setup(x => x.Account).Returns(userRepositoryMoq.Object);

            service = new AccountService(repositoryWrapperMoq.Object);
        }
        public static IEnumerable<object[]> GetIncorrectAccounts()
        {
            return new List<object[]>
            {
                new object[] { new Account() {AccountId = "", ClientId = "", CurrencyId = 1, TypeId = 1, StatusId = 1, Balance = 0, UpdatedAt = DateTime.MaxValue } },
                new object[] { new Account() {AccountId = "Test", ClientId = "", CurrencyId = 1, TypeId = 1, StatusId = 1, Balance = 0, UpdatedAt = DateTime.MaxValue } },
                new object[] { new Account() {AccountId = "", ClientId = "Test", CurrencyId = 1, TypeId = 1, StatusId = 1, Balance = 0, UpdatedAt = DateTime.MaxValue } }
            };
        }

        [Theory]
        [MemberData(nameof(GetIncorrectAccounts))]
        public async Task CreateAsync_BadAccount_ShouldThrowNullArgumentException(Account model)
        {
            var newAccount = model;

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.Create(newAccount));

            Assert.IsType<ArgumentException>(ex);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<Account>()), Times.Never());
        }


        [Fact]
        public async Task CreateAsync_NullAccount_ShouldThrowNullArgumentException()
        {
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => service.Create(null));

            Assert.IsType<ArgumentNullException>(ex);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<Account>()), Times.Never());
        }

        [Fact]

        public async Task CreateAsyncNewAccountShouldCreateNewAccount()
        {
            var newAccount = new Account()
            {
                AccountId = "Test",
                ClientId = "Test",
                TypeId = 1,
                StatusId = 1,
                Balance = 0,
                UpdatedAt = DateTime.Now

            };
            await service.Create(newAccount);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<Account>()), Times.Once);

        }
    }
}