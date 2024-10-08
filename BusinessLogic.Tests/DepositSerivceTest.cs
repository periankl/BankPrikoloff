﻿using BusinessLogic.Servises;
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
                new object[] { new Deposit() {AccountId = "Test", Name = "", EndDate = DateTime.Now.AddYears(1)} },
                new object[] { new Deposit() {AccountId = "", Name = "Test", EndDate = DateTime.Now.AddYears(1)} },
                new object[] { new Deposit() {AccountId = "Test", Name = "Test", EndDate = DateTime.Now.AddYears(-1)} },

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
                AccountId = "Test",
                Name = "Test",
                EndDate = DateTime.Now.AddYears(1)
            };
            await service.Create(newDeposit);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<Deposit>()), Times.Once);

        }

        public static IEnumerable<object[]> UpdateIncorrectDeposits()
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
        [MemberData(nameof(UpdateIncorrectDeposits))]
        public async Task UpdateAsync_BadDeposit_ShouldThrowNullArgumentException(Deposit model)
        {
            var newDeposit = model;

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.Update(newDeposit));

            Assert.IsType<ArgumentException>(ex);
            userRepositoryMoq.Verify(x => x.Update(It.IsAny<Deposit>()), Times.Never());
            Assert.IsType<ArgumentException>(ex);

        }

        [Fact]
        public async Task UpdateAsyncNullDepositShouldThrowNullArgumentException()
        {
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => service.Update(null));

            Assert.IsType<ArgumentNullException>(ex);
            userRepositoryMoq.Verify(x => x.Update(It.IsAny<Deposit>()), Times.Never());
            Assert.IsType<ArgumentNullException>(ex);

        }

        [Fact]
        public async Task UpdateAsyncNewDepositShouldCreateNewDeposit()
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
            await service.Update(newDeposit);
            userRepositoryMoq.Verify(x => x.Update(It.IsAny<Deposit>()), Times.Once);
        }

        [Fact]
        public async void GetByIdAsyncNullDepositShouldThrowArgumentException()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.GetById("FFFFF"));

            Assert.IsType<ArgumentException>(ex);
            userRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<Deposit, bool>>>()), Times.Once);
        }


        [Fact]
        public async void DeleteAsyncNullDepositShouldThrowArgumentException()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Delete("FFFF"));

            Assert.IsType<ArgumentException>(ex);
            userRepositoryMoq.Verify(x => x.Delete(It.IsAny<Deposit>()), Times.Never);
        }
    }
}