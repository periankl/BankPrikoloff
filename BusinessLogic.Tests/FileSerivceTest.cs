using BusinessLogic.Servises;
using Domain.Interfaces;
using Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = Domain.Models.File;

namespace BusinessLogic.Tests
{
    public class FileSerivceTest
    {
        private readonly FileService service;
        private readonly Mock<IFileRepository> userRepositoryMoq;

        public FileSerivceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            userRepositoryMoq = new Mock<IFileRepository>();

            repositoryWrapperMoq.Setup(x => x.File).Returns(userRepositoryMoq.Object);

            service = new FileService(repositoryWrapperMoq.Object);
        }
        public static IEnumerable<object[]> GetIncorrectFiles()
        {
            return new List<object[]>
            {
                new object[] { new File() {FileId = "", FilePath = "", ClientId = "", UploadAt = DateTime.MaxValue} },
                new object[] { new File() {FileId = "Test", FilePath = "", ClientId = "", UploadAt = DateTime.MaxValue} },
                new object[] { new File() {FileId = "", FilePath = "", ClientId = "Test", UploadAt = DateTime.MaxValue} },
                new object[] { new File() {FileId = "", FilePath = "Test", ClientId = "", UploadAt = DateTime.MaxValue} },


            };
        }

        [Theory]
        [MemberData(nameof(GetIncorrectFiles))]
        public async Task CreateAsync_BadFile_ShouldThrowNullArgumentException(File model)
        {
            var newFile = model;

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.Create(newFile));

            Assert.IsType<ArgumentException>(ex);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<File>()), Times.Never());
        }


        [Fact]
        public async Task CreateAsync_NullFile_ShouldThrowNullArgumentException()
        {
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => service.Create(null));

            Assert.IsType<ArgumentNullException>(ex);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<File>()), Times.Never());
        }

        [Fact]

        public async Task CreateAsyncNewFileShouldCreateNewFile()
        {
            var newFile = new File()
            {
                FileId = "Test",
                ClientId = "Test",
                FilePath = "Test",
                MessageId = 1,
                UploadAt = DateTime.Now
            };
            await service.Create(newFile);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<File>()), Times.Once);

        }
    }
}