using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProjectCore.Models
{
    public class OperationPermission
    {
        [Key]
        public int ID { get; set; }
        public string Definition { get; set; }
    }
}
