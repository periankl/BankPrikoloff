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
    public class ChatSerivceTest
    {
        private readonly ChatService service;
        private readonly Mock<IChatRepository> userRepositoryMoq;

        public ChatSerivceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            userRepositoryMoq = new Mock<IChatRepository>();

            repositoryWrapperMoq.Setup(x => x.Chat).Returns(userRepositoryMoq.Object);

            service = new ChatService(repositoryWrapperMoq.Object);
        }



        [Fact]
        public async Task CreateAsync_NullChat_ShouldThrowNullArgumentException()
        {
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => service.Create(null));

            Assert.IsType<ArgumentNullException>(ex);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<Chat>()), Times.Never());
        }

        [Fact]

        public async Task CreateAsyncNewChatShouldCreateNewChat()
        {
            var newChat = new Chat()
            {
                ChatId = 1,
                CreatedAt = DateTime.Now
            };
            await service.Create(newChat);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<Chat>()), Times.Once);

        }
    }
}