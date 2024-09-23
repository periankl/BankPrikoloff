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
    public class MessageSerivceTest
    {
        private readonly MessageService service;
        private readonly Mock<IMessageRepository> userRepositoryMoq;

        public MessageSerivceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            userRepositoryMoq = new Mock<IMessageRepository>();

            repositoryWrapperMoq.Setup(x => x.Message).Returns(userRepositoryMoq.Object);

            service = new MessageService(repositoryWrapperMoq.Object);
        }
        public static IEnumerable<object[]> GetIncorrectMessages()
        {
            return new List<object[]>
            {
                new object[] { new Message() { ClientId = "", Content = "", CreatedAt  = DateTime.MaxValue } },
                new object[] { new Message() { ClientId = "Test", Content = "", CreatedAt  = DateTime.MaxValue } },
                new object[] { new Message() { ClientId = "", Content = "Test", CreatedAt  = DateTime.MaxValue } }

            };
        }

        [Theory]
        [MemberData(nameof(GetIncorrectMessages))]
        public async Task CreateAsync_BadMessage_ShouldThrowNullArgumentException(Message model)
        {
            var newMessage = model;

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.Create(newMessage));

            Assert.IsType<ArgumentException>(ex);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<Message>()), Times.Never());
        }


        [Fact]
        public async Task CreateAsync_NullMessage_ShouldThrowNullArgumentException()
        {
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => service.Create(null));

            Assert.IsType<ArgumentNullException>(ex);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<Message>()), Times.Never());
        }

        [Fact]

        public async Task CreateAsyncNewMessageShouldCreateNewMessage()
        {
            var newMessage = new Message()
            {
                MessageId = 1,
                StatusId = 1,
                TredId = 1,
                ClientId = "Test",
                Content = "Test",
                CreatedAt = DateTime.Now

            };
            await service.Create(newMessage);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<Message>()), Times.Once);

        }
    }
}