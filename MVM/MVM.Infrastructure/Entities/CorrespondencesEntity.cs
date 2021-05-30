using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MVM.Infrastructure.Entities
{
    public class CorrespondencesEntity
    {
        [Key]
        public int CorrespondenceId { get; set; }
        public string Code { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        [ForeignKey("Users")]
        public int UserId { get; set; }
        [ForeignKey("CorrespondenceTypes")]
        public int CorrespondenceTypeId { get; set; }

        [JsonIgnore]
        public virtual UsersEntity User { get; set; }
        [JsonIgnore]
        public virtual CorrespondenceTypesEntity CorrespondenceType { get; set; }
    }
}
