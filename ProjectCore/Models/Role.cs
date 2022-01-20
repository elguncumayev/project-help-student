using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProjectCore.Models
{
    public class Role
    {
        [Key]
        public int ID { get; set; }
        public string RoleName { get; set; }
        public List<OperationPermission> Permissions { get; set; }
    }
}
