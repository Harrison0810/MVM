using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MVM.Infrastructure.Entities
{
    public class CorrespondenceTypesEntity
    {
        [Key]
        public int CorrespondenceTypeId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<CorrespondencesEntity> Correspondences{ get; set; }
    }
}
