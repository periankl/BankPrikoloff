﻿using BusinessLogic.Servises;
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
    public class OperationHistorySerivceTest
    {
        private readonly OperationHistoryService service;
        private readonly Mock<IOperationHistoryRepository> userRepositoryMoq;

        public OperationHistorySerivceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            userRepositoryMoq = new Mock<IOperationHistoryRepository>();

            repositoryWrapperMoq.Setup(x => x.OperationHistory).Returns(userRepositoryMoq.Object);

            service = new OperationHistoryService(repositoryWrapperMoq.Object);
        }
        public static IEnumerable<object[]> GetIncorrectOperationHistorys()
        {
            return new List<object[]>
            {
                new object[] { new OperationHistory() { OperationId = "", SenderAccountId = "Test", DestinationAccountId = "Test" } },
                new object[] { new OperationHistory() { OperationId = "Test", SenderAccountId = "Test", DestinationAccountId = "Test", Amount = -1 } },
                new object[] { new OperationHistory() { OperationId = "Test", SenderAccountId = "Test", DestinationAccountId = "Test", DestinationCardId = null } },
            };
        }

        [Theory]
        [MemberData(nameof(GetIncorrectOperationHistorys))]
        public async Task CreateAsync_BadOperationHistory_ShouldThrowNullArgumentException(OperationHistory model)
        {
            var newOperationHistory = model;

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.Create(newOperationHistory));

            Assert.IsType<ArgumentException>(ex);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<OperationHistory>()), Times.Never());
        }


        [Fact]
        public async Task CreateAsync_NullOperationHistory_ShouldThrowNullArgumentException()
        {
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => service.Create(null));

            Assert.IsType<ArgumentNullException>(ex);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<OperationHistory>()), Times.Never());
        }

        [Fact]

        public async Task CreateAsyncNewOperationHistoryShouldCreateNewOperationHistory()
        {
            var newOperationHistory = new OperationHistory()
            {
                OperationId = "Test",
                SenderAccountId = "Test",
                SenderCardId = "Test",
                DestinationAccountId = "Test1",
                DestinationCardId = "Test12",
                StatusId = 1,
                Date = DateTime.Now,
                Amount = 100
            };
            await service.Create(newOperationHistory);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<OperationHistory>()), Times.Once);

        }
    }
}