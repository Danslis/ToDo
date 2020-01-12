using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.DataAccess.Base;
using ToDo.DataAccess.Entities;
using ToDo.Services;
using ToDo.Services.Base;
using ToDo.Services.DTO;

namespace FrontToDoUnit.Tests.Services
{
    class CardServiceTest
    {
        private Mock<ICardRepository> _mockRepository;
        private ICardService _service;
        private CardCollectionEntity _data;
        private List<CardDto> _listCard;
        [SetUp]
        public void Init()
        {
            _listCard = new List<CardDto>
            {
                new CardDto
                {Id = 1, Name = "Тест1" ,Date=DateTime.Now ,TimeOfCompletion = 1, Priority=1},
                new CardDto
                { Id = 2, Name = "Тест2" ,Date=DateTime.Now ,TimeOfCompletion = 1, Priority=2}
            };
        
            _data = new CardCollectionEntity();
            _data.DuringTheToday = new List<CardEntity>()
            {
                new CardEntity()
                   { Id = 1, Name = "Тест1" ,Date=DateTime.Now ,TimeOfCompletion = 1, Priority=1},
                new CardEntity()
                   { Id = 2, Name = "Тест2" ,Date=DateTime.Now ,TimeOfCompletion = 1, Priority=2}
            };
            _data.DuringTheTomorrow = new List<CardEntity>()
            {
                new CardEntity()
                   { Id = 3, Name = "Тест3" ,Date=DateTime.Now ,TimeOfCompletion = 2, Priority=1},
                new CardEntity()
                   { Id = 4, Name = "Тест4" ,Date=DateTime.Now ,TimeOfCompletion = 2, Priority=2}
            };
            _data.DuringTheWeek = new List<CardEntity>(){
                new CardEntity()
                   { Id = 5, Name = "Тест5" ,Date=DateTime.Now ,TimeOfCompletion = 3, Priority=1},
                new CardEntity()
                   { Id = 6, Name = "Тест6" ,Date=DateTime.Now ,TimeOfCompletion = 3, Priority=2}
            };
            _data.DuringTheMonth = new List<CardEntity>(){
                new CardEntity()
                   { Id = 7, Name = "Тест7" ,Date=DateTime.Now ,TimeOfCompletion = 4, Priority=1},
                new CardEntity()
                   { Id = 8, Name = "Тест8" ,Date=DateTime.Now ,TimeOfCompletion = 4, Priority=2}
            };
            _data.Done = new List<CardEntity>(){
                new CardEntity()
                   { Id = 8, Name = "Тест9" ,Date=DateTime.Now ,TimeOfCompletion = 5, Priority=1},
                new CardEntity()
                   { Id = 9, Name = "Тест10" ,Date=DateTime.Now ,TimeOfCompletion = 5, Priority=2}
            };
           _mockRepository = new Mock<ICardRepository>();
           _service = new CardService(_mockRepository.Object);
        }

        [Test]
        public async Task GetCardsCollection()
        {
            //Arrange
            _mockRepository.Setup(x => x.GetCardsCollection()).Returns(Task.FromResult(_data));

            //Act
            var results = await _service.GetCardsCollection();

            //Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(2, results.DuringTheToday.ToList().Count);
            Assert.AreEqual(2, results.DuringTheTomorrow.ToList().Count);
            Assert.AreEqual(2, results.DuringTheWeek.ToList().Count);
            Assert.AreEqual(2, results.DuringTheMonth.ToList().Count);
            Assert.AreEqual(2, results.Done.ToList().Count);
        }

        [Test]
        public async Task AddCard()
        {
            int maxId = _listCard.Max(u => u.Id);
            //Arrange
            var card = new CardDto() {Id= maxId, Name = "Тест11", Date = DateTime.Now, TimeOfCompletion = 5, Priority = 2 };
            _mockRepository.Setup(x => x.AddCard(It.IsAny<CardEntity>()))
           .Callback((CardEntity label) => _listCard.Add(card))
           .Returns(Task.FromResult(maxId + 1))
           ;
            //Act
            var results = await _service.AddCard(card);
            _mockRepository.Verify(x => x.AddCard(It.IsAny<CardEntity>()), Times.Once);
            //Assert
            Assert.AreEqual(results, maxId + 1);
            Assert.AreEqual(_listCard.Count, 3);
            Assert.AreEqual(_listCard[2].Name, "Тест11");
        }

        [Test]
        public async Task EditCard()
        {
            var card = new CardDto()
            { Id = 1, Name = "Тест25", Date = DateTime.Now, TimeOfCompletion = 1, Priority = 1 };
            _mockRepository.Setup(x => x.EditCard(It.IsAny<int>(),It.IsAny<CardEntity>()))
                .Callback((int id,CardEntity cardDto) => _listCard[_listCard.FindIndex(x => x.Id == id)] = new CardDto()
                {
                    Id = id,
                    Name = cardDto.Name,
                    Date = cardDto.Date,
                    Priority = cardDto.Priority,
                });

            await _service.EditCard(1, card);
            // Assert
            _mockRepository.Verify(x => x.EditCard(It.IsAny<int>(), It.IsAny<CardEntity>()), Times.Once);

            Assert.AreEqual(2, _listCard.Count());
            Assert.AreEqual("Тест25", _listCard.First().Name);
        }
        [Test]
        public async Task DeleteCard()
        {
            _mockRepository.Setup(x => x.DeleteCard(It.IsAny<int>()))
            .Callback((int id) => _listCard.RemoveAt(_listCard.FindIndex(x => x.Id == id)));
            await _service.DeleteCard(2);
            _mockRepository.Verify(x => x.DeleteCard(It.IsAny<int>()), Times.Once);
            Assert.AreEqual(1, _listCard.Count());
        }
    }
}