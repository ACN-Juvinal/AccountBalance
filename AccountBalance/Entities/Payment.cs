using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AccountBalance.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
    }
}
