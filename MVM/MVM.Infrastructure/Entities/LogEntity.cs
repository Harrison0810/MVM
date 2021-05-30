using System;
using System.ComponentModel.DataAnnotations;

namespace MVM.Infrastructure.Entities
{
    public class LogEntity
    {
        [Key]
        public int LogId { get; set; }
        public DateTime CreationDate { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
    }
}
