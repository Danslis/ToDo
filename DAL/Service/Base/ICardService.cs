using System.Collections.Generic;
using DAL.Entities;
using DAL.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DAL.Service.Base
{
    public interface ICardService
    {
        CardCollectionEntity GetCardsCollection();
        EntityEntry<CardEntity> AddCard(CardModel model);
        void EditCard(int id, CardModel model);
        void SubmitCard(int previousTimeOfCompletion, CardModel value);
        void MoveCard(int currentBoardPlaceId, CardModel model);
        void MoveCardToAnotherBorder(int previousBoardPlaceId,int currentBoardPlaceId, int previousTimeOfCompletion, CardModel value);
        void DeleteCard(int id);
        IEnumerable<CardEntity> GetCards();
    }
}
