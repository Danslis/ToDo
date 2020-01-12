using System.Collections.Generic;
using System.Linq;
using DAL.Entities;
using DAL.Models;
using DAL.Service.Base;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;
       
        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }
        [HttpGet("get_cards")]
        public ActionResult<IEnumerable<CardEntity>> GetCards()
        {
            var result = _cardService.GetCards();
            return result.ToList();
        }

        [HttpGet("get_cards_collection")]
        public ActionResult<CardCollectionEntity> GetCardsCollection()
        {
            var result = _cardService.GetCardsCollection();
            return result;
        }
        // POST api/card/add_card
        [HttpPost("add_card")]
        public int AddCard([FromBody] CardModel value)
        {
            var result = _cardService.AddCard(value);
            var newId = result.Entity.Id;
            return newId;
        }
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] CardModel value)
        {
            _cardService.EditCard(id,value);
        }

        [HttpPut("submit_card/{previousTimeOfCompletion}")]
        public void SubmitCard(int previousTimeOfCompletion, [FromBody] CardModel value)
        {
            _cardService.SubmitCard(previousTimeOfCompletion, value);
        }
        [HttpPut("move_card/{currentBoardPlaceId}")]
        public void MoveCard(int currentBoardPlaceId, [FromBody] CardModel value)
        {
            _cardService.MoveCard(currentBoardPlaceId, value);
        }
        [HttpPut("move_card_to_another_board/{previousBoardPlaceId}/{currentBoardPlaceId}/{previousTimeOfCompletion}")]
        public void MoveCardToAnotherBorder(int previousBoardPlaceId, int currentBoardPlaceId, int previousTimeOfCompletion,  [FromBody] CardModel value)
        {
            _cardService.MoveCardToAnotherBorder(previousBoardPlaceId ,currentBoardPlaceId, previousTimeOfCompletion, value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _cardService.DeleteCard(id);
        }
    }
}
