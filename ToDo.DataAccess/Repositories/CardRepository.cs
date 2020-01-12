using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using ToDo.DataAccess.Base;
using ToDo.DataAccess.Entities;


namespace ToDo.DataAccess.Repositories
{
    public class CardRepository: ICardRepository
    {
        private readonly HttpClient _httpClient;

        public CardRepository(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("User-Agent", "HttpClient");
            _httpClient = client;
        }

        public async Task<IEnumerable<CardEntity>> GetCardsAsync()
        {
            var response = await _httpClient.GetAsync($"/api/card/get_cards");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsAsync<IEnumerable<CardEntity>> ();
            return result;
        }

        public async Task<CardCollectionEntity> GetCardsCollection()
        {
            var response = await _httpClient.GetAsync($"/api/card/get_cards_collection");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsAsync<CardCollectionEntity>();
            return result;
        }

        public async Task<int> AddCard(CardEntity entity)
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/card/add_card", entity);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsAsync<int>();
            return result;
        }

        public async Task EditCard(int cardId, CardEntity entity)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/card/{cardId}", entity);
            response.EnsureSuccessStatusCode();
        }

        public async Task SubmitDone(int previousTimeOfCompletion, CardEntity entity)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/card/submit_card/{previousTimeOfCompletion}", entity);
            response.EnsureSuccessStatusCode();
        }
        public async Task MoveCard(int currentBoardPlaceId, CardEntity entity)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/card/move_card/{currentBoardPlaceId}", entity);
            response.EnsureSuccessStatusCode();
        }
        public async Task MoveCardToAnotherBoard(int previousBoardPlaceId, int currentBoardPlaceId, int previousTimeOfCompletion, CardEntity entity)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/card/move_card_to_another_board/{previousBoardPlaceId}/{currentBoardPlaceId}/{previousTimeOfCompletion}", entity);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteCard(int cardId)
        {
            var response = await _httpClient.DeleteAsync($"/api/card/{cardId}");
            response.EnsureSuccessStatusCode();
        }
    }
}
    

