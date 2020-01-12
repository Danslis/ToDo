using System.Collections.Generic;

namespace ToDo.Web.Model
{
    public class CardCollectionModel
    {
        public IEnumerable<CardModel> DuringTheToday { get; set; }
        public IEnumerable<CardModel> DuringTheTomorrow { get; set; }
        public IEnumerable<CardModel> DuringTheWeek { get; set; }
        public IEnumerable<CardModel> DuringTheMonth { get; set; }
        public IEnumerable<CardModel> Done { get; set; }
    }
}
