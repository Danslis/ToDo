using System.Collections.Generic;
using System.Linq;
using DAL.Context;
using DAL.Entities;
using DAL.Models;
using DAL.Service.Base;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DAL.Service
{
    public class CardService : ICardService
    {
        private readonly CardsDbContext _context;
        private void DecrementPostionCurrentBoardAfterDelete(int timeOfCompletion, int previousBoardPlaceId)
        {
            var data = _context.Cards.Where(x => x.TimeOfCompletion == timeOfCompletion && x.Position > previousBoardPlaceId).ToList();
            foreach (var item in data)
            {
                item.Position = item.Position - 1;
                _context.Cards.Update(item);
                _context.SaveChanges();
            }

        }
        private void DecrementPostionCurrentBoard(int timeOfCompletion,int previousBoardPlaceId, int currentBoardPlaceId)
        {
            var data = _context.Cards.Where(x => x.TimeOfCompletion == timeOfCompletion && x.Position > previousBoardPlaceId && x.Position <= currentBoardPlaceId).ToList();
            foreach(var item in data)
            {
                 item.Position = item.Position-1;
                _context.Cards.Update(item);
                _context.SaveChanges();
            }

        }
        private void IncrementPositionCurrentBoard(int timeOfCompletion, int previousBoardPlaceId, int currentBoardPlaceId)
        {
           var data = _context.Cards.Where(x => x.TimeOfCompletion == timeOfCompletion &&  x.Position >= currentBoardPlaceId && x.Position < previousBoardPlaceId).ToList();
            foreach (var item in data)
            {
                item.Position = item.Position+1;
                _context.Cards.Update(item);
                _context.SaveChanges();
            }
        }
        private void DecrementPositionAnotherBoard(int timeOfCompletion, int currentBoardPlaceId)
        {
            var data = _context.Cards.Where(x => x.TimeOfCompletion == timeOfCompletion && x.Position >= currentBoardPlaceId).ToList();
            foreach (var item in data)
            {
                item.Position = item.Position + 1;
                _context.Cards.Update(item);
                _context.SaveChanges();
            }
        }

        private void IncrementPositionAnotherBoard(int timeOfCompletion, int currentBoardPlaceId)
        {
            var data = _context.Cards.Where(x => x.TimeOfCompletion == timeOfCompletion && x.Position >= currentBoardPlaceId).ToList();
            foreach (var item in data)
            {
                item.Position = item.Position + 1;
                _context.Cards.Update(item);
                _context.SaveChanges();
            }
        }


        public CardService(CardsDbContext context)
        {
            _context = context;
        }

        public IEnumerable<CardEntity> GetCards()
        {
            var cardEntity = _context.Cards;
            var cardDto = cardEntity.Select(result => new CardEntity
            {
                Id = result.Id,
                Name = result.Name,
                Date = result.Date,
                TimeOfCompletion = result.TimeOfCompletion,
                Priority = result.Priority,
                Position = result.Position
            });
            return cardDto;
        }

        public CardCollectionEntity GetCardsCollection()
        {
            var data = new CardCollectionEntity(); 
            data.DuringTheToday = _context.Cards.Where(value=> value.TimeOfCompletion == 1).OrderBy(m => m.Position).ToList();
            data.DuringTheTomorrow = _context.Cards.Where(value => value.TimeOfCompletion == 2).OrderBy(m => m.Position).ToList();
            data.DuringTheWeek = _context.Cards.Where(value => value.TimeOfCompletion == 3).OrderBy(m => m.Position).ToList();
            data.DuringTheMonth = _context.Cards.Where(value => value.TimeOfCompletion == 4).OrderBy(m => m.Position).ToList();
            data.Done = _context.Cards.Where(value => value.TimeOfCompletion == 5).OrderBy(m => m.Position).ToList();
            return data;
        }

        public EntityEntry<CardEntity> AddCard(CardModel model)
        { 
            if (model != null)
            {
                var position = _context.Cards.OrderByDescending(x => x.Position)
                    .FirstOrDefault(x => x.TimeOfCompletion == model.TimeOfCompletion)?.Position; 
                var entity = new CardEntity()
                {
                    Name = model.Name,
                    Date = model.Date,
                    TimeOfCompletion = model.TimeOfCompletion, 
                    Priority = model.Priority,
                    Position = position != null ? position.Value + 1 : 0
                }; 
                var newObj = _context.Cards.Add(entity);
                _context.SaveChanges();
                return newObj;
                
            }
            else
            {
                return null;
            }
        }
        public void EditCard(int id, CardModel value)
        {
            var card = _context.Cards.FirstOrDefault(x => x.Id == id);
            if (card != null)
            {
                if (value.TimeOfCompletion != card.TimeOfCompletion)
                {
                    DecrementPostionCurrentBoardAfterDelete(card.TimeOfCompletion, card.Position);
                    var position = _context.Cards.OrderByDescending(x => x.Position)
                        .FirstOrDefault(x => x.TimeOfCompletion == value.TimeOfCompletion)?.Position;
                    card.Name = value.Name;
                    card.Date = value.Date;
                    card.TimeOfCompletion = value.TimeOfCompletion;
                    card.Priority = value.Priority;
                    card.Position = position != null ? position.Value + 1 : 0;
                    _context.Cards.Update(card);
                    _context.SaveChanges();

                }
                else
                {
                    card.Name = value.Name;
                    card.Date = value.Date;
                    card.TimeOfCompletion = value.TimeOfCompletion;
                    card.Priority = value.Priority;
                    _context.Cards.Update(card);
                    _context.SaveChanges();
                }
            }
        }

        public void SubmitCard(int previousTimeOfCompletion, CardModel value)
        {
            var card = _context.Cards.FirstOrDefault(x => x.Id == value.Id);
            if (card != null)
            {
                DecrementPostionCurrentBoardAfterDelete(previousTimeOfCompletion, card.Position);
                var position = _context.Cards.OrderByDescending(x => x.Position)
                    .FirstOrDefault(x => x.TimeOfCompletion == value.TimeOfCompletion)?.Position;
                card.TimeOfCompletion = value.TimeOfCompletion;
                card.Position = position != null ? position.Value + 1 : 0;
                _context.Cards.Update(card);
                _context.SaveChanges();
            }
        }

        public void MoveCardToAnotherBorder(int previousBoardPlaceId,int currentBoardPlaceId,int previousTimeOfCompletion, CardModel value)
        {
            var card = _context.Cards.FirstOrDefault(x => x.Id == value.Id);
            if (card != null)
            {
                IncrementPositionAnotherBoard(value.TimeOfCompletion, currentBoardPlaceId);
                DecrementPostionCurrentBoardAfterDelete(previousTimeOfCompletion, previousBoardPlaceId);
                card.Position = currentBoardPlaceId;
                card.TimeOfCompletion = value.TimeOfCompletion;
                _context.Cards.Update(card);
                _context.SaveChanges();
            }
        }
        public void MoveCard(int currentBoardPlaceId, CardModel value)
        {
            var card = _context.Cards.FirstOrDefault(x => x.Id == value.Id);
            if (card != null)
            {
                if (currentBoardPlaceId > value.Position)
                {
                    DecrementPostionCurrentBoard(value.TimeOfCompletion, value.Position, currentBoardPlaceId);
                }
                else
                {
                    IncrementPositionCurrentBoard(value.TimeOfCompletion, value.Position, currentBoardPlaceId);
                }

                card.Position = currentBoardPlaceId;
                card.TimeOfCompletion = value.TimeOfCompletion;
                _context.Cards.Update(card);
                _context.SaveChanges();
            }
        }
        public void  DeleteCard(int id)
        {
            if (id != 0)
            {
                var card = _context.Cards.FirstOrDefault(x => x.Id == id);
                if (card != null)
                {
                    DecrementPostionCurrentBoardAfterDelete(card.TimeOfCompletion, card.Position);
                    _context.Cards.Remove(card);
                    _context.SaveChanges();
                }
            }
        }
    }
}
