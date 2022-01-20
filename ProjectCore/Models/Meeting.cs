using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProjectCore.Models
{
    public class Meeting
    {
        [Key]
        public int ID { get; set; }
        public string Place { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Info { get; set; }
    }
}
