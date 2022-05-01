using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectCore.Models
{
    public class Answer
    {
        [Key]
        public int ID { get; set; }
        public User Author { get; set; }
        public string Text { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
    }
}
