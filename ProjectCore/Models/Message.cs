using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProjectCore.Models
{
    public class Message
    {
        [Key]
        public int ID { get; set; }
        public User From { get; set; }
        public User To { get; set; }
        public string Text { get; set; }
        public DateTimeOffset SentTime { get; set; }
    }
}
