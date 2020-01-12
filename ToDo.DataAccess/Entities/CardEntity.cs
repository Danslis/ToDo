using System;

namespace ToDo.DataAccess.Entities
{
    public class CardEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int TimeOfCompletion { get; set; }
        public int Priority { get; set; }
        public int Position { get; set; }
    }
}
