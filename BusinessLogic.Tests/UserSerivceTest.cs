using BusinessLogic.Servises;
using Domain.Interfaces;
using Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Tests
{
    public class UserSerivceTest
    {
        private readonly UserService service;
        private readonly Mock<IUserRepository> userRepositoryMoq;

        public UserSerivceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            userRepositoryMoq = new Mock<IUserRepository>();

            repositoryWrapperMoq.Setup(x => x.User).Returns(userRepositoryMoq.Object);

            service = new UserService(repositoryWrapperMoq.Object);
        }
        public static IEnumerable<object[]> GetIncorrectUsers()
        {
            return new List<object[]>
            {
                new object[] { new User() { ClientId = "", FirstName = "", LastName = "", DateOfBirth = DateTime.MaxValue, Login = "", Email = "", Password = "" } },
                new object[] { new User() { ClientId = "", FirstName = "Test", LastName = "", DateOfBirth = DateTime.MaxValue, Login = "", Email = "", Password = "" } },
                new object[] { new User() { ClientId = "", FirstName = "Test", LastName = "Test", DateOfBirth = DateTime.Now, Login = "", Email = "", Password = "" } }
            };
        }

        [Theory]
        [MemberData(nameof(GetIncorrectUsers))]
        public async Task CreateAsync_BadUser_ShouldThrowNullArgumentException(User model)
        {
            var newUser = model;

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.Create(newUser));

            Assert.IsType<ArgumentException>(ex);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<User>()), Times.Never());
        }


        [Fact]
        public async Task CreateAsync_NullUser_ShouldThrowNullArgumentException()
        {
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => service.Create(null));

            Assert.IsType<ArgumentNullException>(ex);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<User>()), Times.Never());
        }

        [Fact]

        public async Task CreateAsyncNewUserShouldCreateNewUser()
        {
            var newUser = new User()
            {
                ClientId = "Test",
                FirstName = "Test",
                LastName = "Test",
                Patronomic = "Test",
                DateOfBirth = DateTime.ParseExact("22-01-2006", "dd-MM-yyyy", CultureInfo.InvariantCulture),
                Email = "Test",
                Login = "Test123441513412241",
                Password = "Test",
                SeriesPasport = 1111,
                NumberPasport = 111111

            };
            await service.Create(newUser);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<User>()), Times.Once);

        }


        [Fact]
        public async void GetByIdAsyncNullUserShouldThrowArgumentException()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.GetById("FFFFF"));

            Assert.IsType<ArgumentException>(ex);
            userRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
        }

        [Fact]
        public async void DeleteAsyncNullUserShouldThrowArgumentException()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Delete("FFFF"));

            Assert.IsType<ArgumentException>(ex);
            userRepositoryMoq.Verify(x => x.Delete(It.IsAny<User>()), Times.Never);
        }
    }
}