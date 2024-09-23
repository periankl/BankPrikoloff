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
    public class LoanSerivceTest
    {
        private readonly LoanService service;
        private readonly Mock<ILoanRepository> userRepositoryMoq;

        public LoanSerivceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            userRepositoryMoq = new Mock<ILoanRepository>();

            repositoryWrapperMoq.Setup(x => x.Loan).Returns(userRepositoryMoq.Object);

            service = new LoanService(repositoryWrapperMoq.Object);
        }
        public static IEnumerable<object[]> GetIncorrectLoans()
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
            await service.Create(newLoan);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<Loan>()), Times.Once);

        }
    }
}