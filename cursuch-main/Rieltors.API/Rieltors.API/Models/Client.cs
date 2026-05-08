using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Rieltors.API.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [Phone]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? MinPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal MaxPrice { get; set; }

        [StringLength(100)]
        public string PreferredDistrict { get; set; }

        public int? MinRooms { get; set; }

        [StringLength(50)]
        public string PropertyType { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Активный поиск";

        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

        public int? AdministratorId { get; set; }

        [ForeignKey("AdministratorId")]
        [JsonIgnore]
        public virtual Admin Administrator { get; set; }

        public string Notes { get; set; }

        public bool IsActive { get; set; } = true;
    }
}