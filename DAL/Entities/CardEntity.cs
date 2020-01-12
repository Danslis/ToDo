using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("cards")]
    public class CardEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("date")]
        public DateTime Date { get; set; }
        [Column("time_of_completion")]
        public int TimeOfCompletion { get; set; }
        [Column("priority")]
        public int Priority { get; set; }
        [Column("position")]
        public int Position { get; set; }
    }
}
