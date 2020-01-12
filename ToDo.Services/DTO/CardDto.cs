using System;

namespace ToDo.Services.DTO
{
    public class CardDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int TimeOfCompletion { get; set; }
        public int Priority { get; set; }
        public int Position { get; set; }
    }
}
