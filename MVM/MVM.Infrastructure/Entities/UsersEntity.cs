using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MVM.Infrastructure.Entities
{
    public class UsersEntity
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
        [ForeignKey("Roles")]
        public int RoleId { get; set; }

        [JsonIgnore]
        public virtual RolesEntity Role { get; set; }
        [JsonIgnore]
        public virtual ICollection<CorrespondencesEntity> Correspondences{ get; set; }
    }
}
