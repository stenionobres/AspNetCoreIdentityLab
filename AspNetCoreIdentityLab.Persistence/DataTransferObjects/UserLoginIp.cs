using System;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityLab.Persistence.DataTransferObjects
{
    public class UserLoginIp
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime When { get; set; }

        [Required]
        [MaxLength(50)]
        public string IP { get; set; }
    }
}
