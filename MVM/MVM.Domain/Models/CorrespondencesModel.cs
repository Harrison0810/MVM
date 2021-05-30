using System;

namespace MVM.Domain.Models
{
    public class CorrespondencesModel
    {
        public int CorrespondenceId { get; set; }
        public string Code { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public int UserId { get; set; }
        public int CorrespondenceTypeId { get; set; }

        public CorrespondenceTypesModel CorrespondenceType { get; set; }
    }
}
