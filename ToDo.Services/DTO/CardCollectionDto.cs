using System.Collections.Generic;

namespace ToDo.Services.DTO
{
    public class CardCollectionDto
    {
        public IEnumerable<CardDto> DuringTheToday { get; set; }
        public IEnumerable<CardDto> DuringTheTomorrow { get; set; }
        public IEnumerable<CardDto> DuringTheWeek { get; set; }
        public IEnumerable<CardDto> DuringTheMonth { get; set; }
        public IEnumerable<CardDto> Done { get; set; }
    }
}
