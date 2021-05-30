using System;

namespace MVM.Infrastructure.Contracts
{
    public class CorrespondencesContract
    {
        public int CorrespondenceId { get; set; }
        public string Code { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public int UserId { get; set; }
        public int CorrespondenceTypeId { get; set; }

        public CorrespondenceTypesContract CorrespondenceType { get; set; }
    }
}
