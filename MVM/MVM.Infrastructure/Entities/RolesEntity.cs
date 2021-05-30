using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MVM.Infrastructure.Entities
{
    public class RolesEntity
    {
        [Key]
        public int RoleId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public virtual ICollection<UsersEntity> Users{ get; set; }
    }
}
