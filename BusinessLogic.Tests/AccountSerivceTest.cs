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
                new object[] { new Account() {ClientId = "", CurrencyId = -1, TypeId = 1} },
                new object[] { new Account() {ClientId = "", CurrencyId = 1, TypeId = 1} },
                new object[] { new Account() {ClientId = "Test", CurrencyId = 1, TypeId = -1} }
            };
        }

        [Theory]
        [MemberData(nameof(GetIncorrectAccounts))]
        public async Task CreateAsyncBadAccountShouldThrowNullArgumentException(Account model)
        {
            var newAccount = model;

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.Create(newAccount));

            Assert.IsType<ArgumentException>(ex);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<Account>()), Times.Never());
        }


        [Fact]
        public async Task CreateAsyncNullAccountShouldThrowNullArgumentException()
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
                ClientId = "Test",
                TypeId = 1,
                StatusId = 1,
                CurrencyId = 1
            };
            await service.Create(newAccount);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<Account>()), Times.Once);

        }

        public static IEnumerable<object[]> UpdateIncorrectAccounts()
        {
            return new List<object[]>
            {
                new object[] { new Account() {AccountId = "", ClientId = "", CurrencyId = 1, TypeId = 1, StatusId = 1, Balance = 0, UpdatedAt = DateTime.MaxValue } },
                new object[] { new Account() {AccountId = "Test", ClientId = "", CurrencyId = 1, TypeId = 1, StatusId = 1, Balance = 0, UpdatedAt = DateTime.MaxValue } },
                new object[] { new Account() {AccountId = "", ClientId = "Test", CurrencyId = 1, TypeId = 1, StatusId = 1, Balance = 0, UpdatedAt = DateTime.MaxValue } }
            };
        }

        [Theory]
        [MemberData(nameof(UpdateIncorrectAccounts))]
        public async Task UpdateAsyncNewAccountShouldNotCreateNewUser(Account model)
        {
            var newAccount = model;

            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Update(newAccount));

            Assert.IsType<ArgumentException>(ex);
            userRepositoryMoq.Verify(x => x.Update(It.IsAny<Account>()), Times.Never);

            Assert.IsType<ArgumentException>(ex);
        }

        [Fact]
        public async void UpdateAsyncNullAccountShouldThrowNullArgumentExpression()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Update(null));

            Assert.IsType<ArgumentNullException>(ex);
            userRepositoryMoq.Verify(x => x.Update(It.IsAny<Account>()), Times.Never);
        }

        [Fact]
        public async void UpdateAsyncNewAccountShouldCreateNewUser()
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

            await service.Update(newAccount);

            userRepositoryMoq.Verify(x => x.Update(It.IsAny<Account>()), Times.Once);
        }

        [Fact]
        public async void GetByIdAsync_NullAccountShouldThrowArgumentException()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.GetById("FFFFF"));

            Assert.IsType<ArgumentException>(ex);
            userRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<Account, bool>>>()), Times.Once);
        }

        [Fact]
        public async void DeleteAsync_NullAccountShouldThrowArgumentException()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Delete("FFFF"));

            Assert.IsType<ArgumentException>(ex);
            userRepositoryMoq.Verify(x => x.Delete(It.IsAny<Account>()), Times.Never);
        }
    }
}