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
    public class CardSerivceTest
    {
        private readonly CardService service;
        private readonly Mock<ICardRepository> userRepositoryMoq;

        public CardSerivceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            userRepositoryMoq = new Mock<ICardRepository>();

            repositoryWrapperMoq.Setup(x => x.Card).Returns(userRepositoryMoq.Object);

            service = new CardService(repositoryWrapperMoq.Object);
        }
        public static IEnumerable<object[]> GetIncorrectCards()
        {
            return new List<object[]>
            {
                new object[] { new Card() {CardId = "", AccountId = "", CurrencyId = 1, CardNumber = "", ExpDate = DateTime.MinValue, Cvv = "", OwnerName = "", Balance = 0} },
                new object[] { new Card() {CardId = "Test", AccountId = "", CurrencyId = 1, CardNumber = "", ExpDate = DateTime.MinValue, Cvv = "", OwnerName = "", Balance = 0} },
                new object[] { new Card() {CardId = "", AccountId = "Test", CurrencyId = 1, CardNumber = "", ExpDate = DateTime.MinValue, Cvv = "", OwnerName = "", Balance = 0} },
                new object[] { new Card() {CardId = "Test", AccountId = "Test", CurrencyId = 1, CardNumber = "", ExpDate = DateTime.MinValue, Cvv = "", OwnerName = "", Balance = 0} },
            };
        }

        [Theory]
        [MemberData(nameof(GetIncorrectCards))]
        public async Task CreateAsync_BadCard_ShouldThrowNullArgumentException(Card model)
        {
            var newCard = model;

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.Create(newCard));

            Assert.IsType<ArgumentException>(ex);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<Card>()), Times.Never());
        }


        [Fact]
        public async Task CreateAsync_NullCard_ShouldThrowNullArgumentException()
        {
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => service.Create(null));

            Assert.IsType<ArgumentNullException>(ex);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<Card>()), Times.Never());
        }

        [Fact]

        public async Task CreateAsyncNewCardShouldCreateNewCard()
        {
            var newCard = new Card()
            {
                CardId = "Test",
                TypeId = 1,
                CurrencyId = 1,
                AccountId = "Test",
                OwnerName = "Test",
            };
            await service.Create(newCard);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<Card>()), Times.Once);

        }


        public static IEnumerable<object[]> UpdateIncorrectCards()
        {
            return new List<object[]>
            {
                new object[] { new Card() {CardId = "", AccountId = "", CurrencyId = 1, CardNumber = "", ExpDate = DateTime.MinValue, Cvv = "", OwnerName = "", Balance = 0} },
                new object[] { new Card() {CardId = "Test", AccountId = "", CurrencyId = 1, CardNumber = "", ExpDate = DateTime.MinValue, Cvv = "", OwnerName = "", Balance = 0} },
                new object[] { new Card() {CardId = "", AccountId = "Test", CurrencyId = 1, CardNumber = "", ExpDate = DateTime.MinValue, Cvv = "", OwnerName = "", Balance = 0} },
                new object[] { new Card() {CardId = "Test", AccountId = "Test", CurrencyId = 1, CardNumber = "", ExpDate = DateTime.MinValue, Cvv = "", OwnerName = "", Balance = 0} },
            };
        }

        [Theory]
        [MemberData(nameof(UpdateIncorrectCards))]
        public async Task UpdateAsync_BadCard_ShouldThrowNullArgumentException(Card model)
        {
            var newCard = model;

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.Update(newCard));

            Assert.IsType<ArgumentException>(ex);
            userRepositoryMoq.Verify(x => x.Update(It.IsAny<Card>()), Times.Never());
            Assert.IsType<ArgumentException>(ex);

        }


        [Fact]
        public async Task UpdateAsync_NullCard_ShouldThrowNullArgumentException()
        {
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => service.Update(null));

            Assert.IsType<ArgumentNullException>(ex);
            userRepositoryMoq.Verify(x => x.Update(It.IsAny<Card>()), Times.Never());
        }


        [Fact]

        public async Task UpdateAsyncNewCardShouldCreateNewCard()
        {
            var newCard = new Card()
            {
                CardId = "Test",
                TypeId = 1,
                CurrencyId = 1,
                AccountId = "Test",
                CardNumber = "Test",
                ExpDate = DateTime.Now.AddYears(3),
                Cvv = "Test",
                OwnerName = "Test",
                Balance = 1,
                Blocked = false,
                CreatedAt = DateTime.Now
            };
            await service.Update(newCard);
            userRepositoryMoq.Verify(x => x.Update(It.IsAny<Card>()), Times.Once);

        }


        [Fact]
        public async void GetByIdAsyncNullCardShouldThrowArgumentException()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.GetById("FFFFF"));

            Assert.IsType<ArgumentException>(ex);
            userRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<Card, bool>>>()), Times.Once);
        }

        [Fact]
        public async void DeleteAsyncNullCardShouldThrowArgumentException()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Delete("FFFF"));

            Assert.IsType<ArgumentException>(ex);
            userRepositoryMoq.Verify(x => x.Delete(It.IsAny<Card>()), Times.Never);
        }
    }
}