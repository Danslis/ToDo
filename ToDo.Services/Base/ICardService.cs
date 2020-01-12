using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Services.DTO;

namespace ToDo.Services.Base
{
    public interface ICardService
    {
        Task <CardCollectionDto> GetCardsCollection();
        Task<int> AddCard(CardDto modelDto);
        Task EditCard(int cardId, CardDto modelDto);
        Task SubmitDone(int previousTimeOfCompletion, CardDto modelDto);
        Task MoveCard(int currentBoardPlaceId, CardDto modelDto);
        Task MoveCardToAnotherBoard(int previousBoardPlaceId,int currentBoardPlaceId, int previousTimeOfCompletion, CardDto modelDto);
        Task DeleteCard(int cardId);
        Task<IEnumerable<CardDto>> GetCardsAsync();
    }
}
