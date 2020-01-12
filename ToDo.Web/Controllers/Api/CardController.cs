using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDo.Web.Model;
using ToDo.Services.DTO;
using ToDo.Services.Base;
using System.Collections.Generic;
using System;
using System.Linq;

namespace ToDo.Web.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardController : Controller
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpGet("getCards")]
        public async Task<IEnumerable<CardModel>> GetCards()
        {
            var dto = await _cardService.GetCardsAsync();
            var model = dto.Select(result => new CardModel
            {
                Id = result.Id,
                Name = result.Name,
                Date = result.Date,
                TimeOfCompletion = result.TimeOfCompletion,
                Priority = result.Priority
            });
            return model;
        }

        [HttpGet("getAllCards")]
        public async Task<CardCollectionModel> GetCardsCollection()
        {
            var cardCollectionDto = await _cardService.GetCardsCollection();
            var cardCollectionModel = new CardCollectionModel();
            cardCollectionModel.DuringTheToday = cardCollectionDto.DuringTheToday.Select(dto => new CardModel
            {
                Id = dto.Id,
                Name = dto.Name,
                Date = dto.Date,
                TimeOfCompletion = dto.TimeOfCompletion,
                Priority = dto.Priority,
                Position = dto.Position
            });
            cardCollectionModel.DuringTheTomorrow = cardCollectionDto.DuringTheTomorrow.Select(dto => new CardModel
            {
                Id = dto.Id,
                Name = dto.Name,
                Date = dto.Date,
                TimeOfCompletion = dto.TimeOfCompletion,
                Priority = dto.Priority,
                Position = dto.Position
            });
            cardCollectionModel.DuringTheWeek = cardCollectionDto.DuringTheWeek.Select(dto => new CardModel
            {
                Id = dto.Id,
                Name = dto.Name,
                Date = dto.Date,
                TimeOfCompletion = dto.TimeOfCompletion,
                Priority = dto.Priority,
                Position = dto.Position
            });
            cardCollectionModel.DuringTheMonth = cardCollectionDto.DuringTheMonth.Select(dto => new CardModel
            {
                Id = dto.Id,
                Name = dto.Name,
                Date = dto.Date,
                TimeOfCompletion = dto.TimeOfCompletion,
                Priority = dto.Priority,
                Position = dto.Position
            });
            cardCollectionModel.Done = cardCollectionDto.Done.Select(dto => new CardModel
            {
                Id = dto.Id,
                Name = dto.Name,
                Date = dto.Date,
                TimeOfCompletion = dto.TimeOfCompletion,
                Priority = dto.Priority,
                Position = dto.Position
            });
            return cardCollectionModel;
        }
        [HttpPost("addCard")]
        public async Task<IActionResult> AddCard([FromBody] CardModel model)
        {
            if (ModelState.IsValid)
            {
                var modelDto = new CardDto()
                {
                    Name = model.Name,
                    Date = DateTime.Now,
                    TimeOfCompletion = model.TimeOfCompletion,
                    Priority = model.Priority
                };
                model.Id = await _cardService.AddCard(modelDto);
                return Ok(model);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("deleteCard/{id}")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            await _cardService.DeleteCard(id);
            return Ok();
        }
        [HttpPut("editCard/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]CardModel model)
        {
            if (ModelState.IsValid)
            {
                var modelDto = new CardDto()
                {
                    Name = model.Name,
                    Date = model.Date,
                    TimeOfCompletion = model.TimeOfCompletion,
                    Priority = model.Priority
                };
                await _cardService.EditCard(id, modelDto);
                return Ok();
            }
            return BadRequest(ModelState);
        }
        
        [HttpPut("submitDone/{previousTimeOfCompletion}")]
        public async Task<IActionResult> SubmitDone(int previousTimeOfCompletion, [FromBody]CardModel model)
        {
            if (ModelState.IsValid)
            {
                var modelDto = new CardDto()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Date = model.Date,
                    TimeOfCompletion = model.TimeOfCompletion,
                    Priority = model.Priority,
                    Position = model.Position
                };
                await _cardService.SubmitDone(previousTimeOfCompletion, modelDto);
                return Ok();
            }
            return BadRequest(ModelState);
        }
        [HttpPut("moveCard/{currentBoardPlaceId}")]
        public async Task<IActionResult> MoveCard(int currentBoardPlaceId, [FromBody]CardModel model)
        {
            if (ModelState.IsValid)
            {
                var modelDto = new CardDto()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Date = model.Date,
                    TimeOfCompletion = model.TimeOfCompletion,
                    Priority = model.Priority,
                    Position = model.Position
                };
                await _cardService.MoveCard(currentBoardPlaceId, modelDto);
                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpPut("moveCardToAnotherBoard/{previousBoardPlaceId}/{currentBoardPlaceId}/{previousTimeOfCompletion}")]
        public async Task<IActionResult> MoveCardToAnotherBoard(int previousBoardPlaceId, int currentBoardPlaceId, int previousTimeOfCompletion,  [FromBody] CardModel model)
        {
            if (ModelState.IsValid)
            {
                var modelDto = new CardDto()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Date = model.Date,
                    TimeOfCompletion = model.TimeOfCompletion,
                    Priority = model.Priority,
                    Position = model.Position
                };
                await _cardService.MoveCardToAnotherBoard(previousBoardPlaceId,currentBoardPlaceId, previousTimeOfCompletion, modelDto);
                return Ok();
            }
            return BadRequest(ModelState);
        }
    }
}
