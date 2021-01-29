using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityLab.Persistence.DataTransferObjects
{
    public class Policy
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
