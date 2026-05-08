using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Rieltors.API.Models
{
    public class Realtor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [Phone]
        [StringLength(20)]
        public string Phone { get; set; }

        [Required]
        [StringLength(50)]
        public string LicenseNumber { get; set; }

        public int ExperienceYears { get; set; } = 0;

        public double Rating { get; set; } = 0;

        public int CompletedDeals { get; set; } = 0;

        public DateTime HireDate { get; set; } = DateTime.UtcNow;

        [StringLength(100)]
        public string Specialization { get; set; }

        [StringLength(500)]
        public string PhotoUrl { get; set; }

        public bool IsAvailable { get; set; } = true;

        // Навигационные свойства
        [JsonIgnore]
        public virtual ICollection<Deal> Deals { get; set; }
    }

    // DTO для создания риелтора
    public class CreateRealtorDto
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        public string LicenseNumber { get; set; }

        public int ExperienceYears { get; set; }
        public string Specialization { get; set; }
    }

    // DTO для обновления статуса риелтора
    public class UpdateRealtorStatusDto
    {
        public bool IsAvailable { get; set; }
    }
}
