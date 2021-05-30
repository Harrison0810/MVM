using System;

namespace MVM.Infrastructure.Contracts
{
    public class LogContract
    {
        public int LogId { get; set; }
        public DateTime CreationDate { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
    }
}
