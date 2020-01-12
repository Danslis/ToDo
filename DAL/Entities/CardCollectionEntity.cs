using System.Collections.Generic;

namespace DAL.Entities
{
    public class CardCollectionEntity
    {
        public IEnumerable<CardEntity> DuringTheToday { get; set; }
        public IEnumerable<CardEntity> DuringTheTomorrow { get; set; }
        public IEnumerable<CardEntity> DuringTheWeek { get; set; }
        public IEnumerable<CardEntity> DuringTheMonth { get; set; }
        public IEnumerable<CardEntity> Done { get; set; }
    }
}
