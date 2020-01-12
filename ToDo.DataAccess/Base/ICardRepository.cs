using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.DataAccess.Entities;

namespace ToDo.DataAccess.Base
{
    public interface ICardRepository
    {
        Task<CardCollectionEntity> GetCardsCollection();
        Task<int> AddCard(CardEntity entity);
        Task EditCard(int cardId, CardEntity entity);
        Task SubmitDone(int previousTimeOfCompletion, CardEntity entity);
        Task MoveCard(int currentBoardPlaceId, CardEntity entity);
        Task MoveCardToAnotherBoard(int previousBoardPlaceId, int currentBoardPlaceId, int previousTimeOfCompletion, CardEntity entity);
        Task DeleteCard(int cardId);
        Task<IEnumerable<CardEntity>> GetCardsAsync();
    }
}
