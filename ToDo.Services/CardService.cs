using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.DataAccess.Base;
using ToDo.DataAccess.Entities;
using ToDo.Services.Base;
using ToDo.Services.DTO;
using System.Linq;

namespace ToDo.Services
{
    public class CardService: ICardService
    {
        private readonly ICardRepository _cardRepository;
        public CardService(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }
        public async Task<IEnumerable<CardDto>> GetCardsAsync()
        {
            var cardEntity = await _cardRepository.GetCardsAsync();
            var cardDto = cardEntity.Select(ent => new CardDto
            {
                Id = ent.Id,
                Name = ent.Name,
                Date = ent.Date,
                TimeOfCompletion = ent.TimeOfCompletion,
                Priority = ent.Priority
            });
            return cardDto;
        }
        public async Task<CardCollectionDto> GetCardsCollection()
        {
            var dataEntity = await _cardRepository.GetCardsCollection();
            var dataDto = new CardCollectionDto();
            dataDto.DuringTheToday = dataEntity.DuringTheToday.Select(ent => new CardDto
            {
                Id = ent.Id,
                Name = ent.Name,
                Date = ent.Date,
                TimeOfCompletion = ent.TimeOfCompletion,
                Priority = ent.Priority,
                Position = ent.Position
            });
            dataDto.DuringTheTomorrow = dataEntity.DuringTheTomorrow.Select(ent => new CardDto
            {
                Id = ent.Id,
                Name = ent.Name,
                Date = ent.Date,
                TimeOfCompletion = ent.TimeOfCompletion,
                Priority = ent.Priority,
                Position = ent.Position
            });
            dataDto.DuringTheWeek = dataEntity.DuringTheWeek.Select(ent => new CardDto
            {
                Id = ent.Id,
                Name = ent.Name,
                Date = ent.Date,
                TimeOfCompletion = ent.TimeOfCompletion,
                Priority = ent.Priority,
                Position = ent.Position
            });
            dataDto.DuringTheMonth = dataEntity.DuringTheMonth.Select(ent => new CardDto
            {
                Id = ent.Id,
                Name = ent.Name,
                Date = ent.Date,
                TimeOfCompletion = ent.TimeOfCompletion,
                Priority = ent.Priority,
                Position = ent.Position
            });
            dataDto.Done = dataEntity.Done.Select(ent => new CardDto
            {
                Id = ent.Id,
                Name = ent.Name,
                Date = ent.Date,
                TimeOfCompletion = ent.TimeOfCompletion,
                Priority = ent.Priority,
                Position = ent.Position
            });
            return dataDto;
        }
        public async Task<int> AddCard(CardDto modelDto)
        {
            var entity = new CardEntity()
            {
                Name = modelDto.Name,
                Date = modelDto.Date,
                TimeOfCompletion = modelDto.TimeOfCompletion,
                Priority = modelDto.Priority
            };
            var result = await _cardRepository.AddCard(entity);
            return result;
        }

        public async Task EditCard(int cardId, CardDto modelDto)
        {
            var entity = new CardEntity()
            {
                Name = modelDto.Name,
                Date = modelDto.Date,
                TimeOfCompletion = modelDto.TimeOfCompletion,
                Priority = modelDto.Priority
            };
            await _cardRepository.EditCard(cardId, entity);
        }

        public async Task SubmitDone(int previousTimeOfCompletion, CardDto modelDto)
        {
            var entity = new CardEntity()
            {
                Id = modelDto.Id,
                Name = modelDto.Name,
                Date = modelDto.Date,
                TimeOfCompletion = modelDto.TimeOfCompletion,
                Priority = modelDto.Priority,
                Position = modelDto.Position
            };
            await _cardRepository.SubmitDone(previousTimeOfCompletion, entity);
        }
        public async Task MoveCard(int currentBoardPlaceId, CardDto modelDto)
        {
            var entity = new CardEntity()
            {
                Id = modelDto.Id,
                Name = modelDto.Name,
                Date = modelDto.Date,
                TimeOfCompletion = modelDto.TimeOfCompletion,
                Priority = modelDto.Priority,
                Position = modelDto.Position
            };
            await _cardRepository.MoveCard(currentBoardPlaceId, entity);
        }
        public async Task MoveCardToAnotherBoard(int previousBoardPlaceId,int currentBoardPlaceId, int previousTimeOfCompletion, CardDto modelDto)
        {
            var entity = new CardEntity()
            {
                Id = modelDto.Id,
                Name = modelDto.Name,
                Date = modelDto.Date,
                TimeOfCompletion = modelDto.TimeOfCompletion,
                Priority = modelDto.Priority,
                Position = modelDto.Position
            };
            await _cardRepository.MoveCardToAnotherBoard(previousBoardPlaceId,currentBoardPlaceId, previousTimeOfCompletion, entity);
        }

        

        public async Task DeleteCard(int cardId)
        {
            await _cardRepository.DeleteCard(cardId);
        }
    }
}
