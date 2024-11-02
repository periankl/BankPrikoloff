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
    public class LoanSerivceTest
    {
        private readonly LoanService service;
        private readonly Mock<ILoanRepository> userRepositoryMoq;
        private readonly Mock<IRepositoryWrapper> repositoryWrapperMoq;

        public LoanSerivceTest()
        {
            repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            userRepositoryMoq = new Mock<ILoanRepository>();

            repositoryWrapperMoq.Setup(x => x.Loan).Returns(userRepositoryMoq.Object);

            service = new LoanService(repositoryWrapperMoq.Object);
        }
        public static IEnumerable<object[]> GetIncorrectLoans()
        {
            return new List<object[]>
            {
                new object[] { new Loan() {AccountId = "", EndDate = DateTime.Now.AddYears(2)} },
                new object[] { new Loan() {AccountId = "Test", EndDate = DateTime.Now.AddYears(-12)} }
            };
        }

        [Theory]
        [MemberData(nameof(GetIncorrectLoans))]
        public async Task CreateAsync_BadLoan_ShouldThrowNullArgumentException(Loan model)
        {
            var newLoan = model;

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.Create(newLoan));

            Assert.IsType<ArgumentException>(ex);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<Loan>()), Times.Never());
        }


        [Fact]
        public async Task CreateAsync_NullLoan_ShouldThrowNullArgumentException()
        {
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => service.Create(null));

            Assert.IsType<ArgumentNullException>(ex);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<Loan>()), Times.Never());
        }

        [Fact]

        public async Task CreateAsyncNewLoanShouldCreateNewLoan()
        {
            var newLoan = new Loan
            {
                LoanTypeId = 1,
                AccountId = "Qwerty",
                Amount = 1000,
                EndDate = DateTime.Now.AddYears(5),
                StartDate = DateTime.Now,
                LoanId = Guid.NewGuid().ToString("N").Substring(0, 9),
                DocumentId = Guid.NewGuid().ToString("N").Substring(0, 9),
            };

            var loanType = new LoanType { LoanTypeId = 1, MaxLoanAmount = 10000 };
            var account = new Account { AccountId = "Qwerty", ClientId = "Client123" };
            var user = new User { ClientId = "Client123" };

            repositoryWrapperMoq.Setup(x => x.LoanType.FindByCondition(It.IsAny<Expression<Func<LoanType, bool>>>()))
                                  .ReturnsAsync(new List<LoanType> { loanType });

            repositoryWrapperMoq.Setup(x => x.Account.FindByCondition(It.IsAny<Expression<Func<Account, bool>>>()))
                                  .ReturnsAsync(new List<Account> { account });

            repositoryWrapperMoq.Setup(x => x.User.FindByCondition(It.IsAny<Expression<Func<User, bool>>>()))
                                  .ReturnsAsync(new List<User> { user });


            // Act
            await service.Create(newLoan);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<Loan>()), Times.Once);
        }

        public static IEnumerable<object[]> UpdateIncorrectLoans()
        {
            return new List<object[]>
            {
                new object[] { new Loan() {LoanId = "", LoanTypeId = 1, StatusId = 1, DocumentId = "", AccountId = "Test", StartDate = DateTime.Now, EndDate = DateTime.Now.AddYears(1)} },
                new object[] { new Loan() {LoanId = "Test", LoanTypeId = 1, StatusId = 1, DocumentId = "", AccountId = "Test", StartDate = DateTime.Now, EndDate = DateTime.Now.AddYears(1)} },
                new object[] { new Loan() {LoanId = "", LoanTypeId = 1, StatusId = 1, DocumentId = "", AccountId = "Test", StartDate = DateTime.Now, EndDate = DateTime.Now.AddYears(1)} },
                new object[] { new Loan() {LoanId = "Test", LoanTypeId = 1, StatusId = 1, DocumentId = "", AccountId = "", StartDate = DateTime.Now, EndDate = DateTime.Now.AddYears(1)} },
                new object[] { new Loan() {LoanId = "Test", LoanTypeId = 1, StatusId = 1, DocumentId = "", AccountId = "Test", StartDate = DateTime.Now.AddYears(1), EndDate = DateTime.Now } }
            };
        }


        [Theory]
        [MemberData(nameof(UpdateIncorrectLoans))]
        public async Task UpdateAsyncBadDeposit_ShouldThrowNullArgumentException(Loan model)
        {
            var newLoan = model;

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.Update(newLoan));

            Assert.IsType<ArgumentException>(ex);
            userRepositoryMoq.Verify(x => x.Update(It.IsAny<Loan>()), Times.Never());
        }


        [Fact]

        public async Task UpdateAsyncNewLoanShouldUpdateLoan()
        {
            var newLoan = new Loan()
            {
                LoanId = "Test",
                LoanTypeId = 1,
                StatusId = 1,
                DocumentId = "Test",
                AccountId = "Test",
                RemarningAmount = 0,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddYears(5)
            };
            await service.Update(newLoan);
            userRepositoryMoq.Verify(x => x.Update(It.IsAny<Loan>()), Times.Once);

        }

        [Fact]
        public async Task UpdateAsyncNullLoanShouldThrowNullArgumentException()
        {
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => service.Update(null));

            Assert.IsType<ArgumentNullException>(ex);
            userRepositoryMoq.Verify(x => x.Update(It.IsAny<Loan>()), Times.Never());
        }

        [Fact]
        public async void GetByIdAsyncNullLoanShouldThrowArgumentException()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.GetById("FFFFF"));

            Assert.IsType<ArgumentException>(ex);
            userRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<Loan, bool>>>()), Times.Once);
        }

        [Fact]
        public async void DeleteAsyncNullLoanShouldThrowArgumentException()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Delete("FFFF"));

            Assert.IsType<ArgumentException>(ex);
            userRepositoryMoq.Verify(x => x.Delete(It.IsAny<Loan>()), Times.Never);
        }
    }
}