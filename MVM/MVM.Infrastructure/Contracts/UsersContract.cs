using System;

namespace MVM.Infrastructure.Contracts
{
    public class UsersContract
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
        public int RoleId { get; set; }
    }
}
