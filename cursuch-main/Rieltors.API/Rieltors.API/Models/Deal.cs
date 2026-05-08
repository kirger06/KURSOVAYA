using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Rieltors.API.Models
{
    public class Deal
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string DealNumber { get; set; }

        public DateTime DealDate { get; set; } = DateTime.UtcNow;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } // Pending, Completed, Cancelled

        [StringLength(50)]
        public string DealType { get; set; } // Sale, Rent

        public DateTime? CompletionDate { get; set; }

        [StringLength(1000)]
        public string ContractTerms { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal CommissionAmount { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal CommissionPercentage { get; set; } = 3.0;

        public string Notes { get; set; }

        // Внешние ключи
        public int PropertyId { get; set; }
        public int ClientId { get; set; }
        public int SellerId { get; set; }
        public int RealtorId { get; set; }
        public int? AdministratorId { get; set; }

        // Навигационные свойства
        [ForeignKey("PropertyId")]
        public virtual Property Property { get; set; }

        [ForeignKey("ClientId")]
        public virtual Client Client { get; set; }

        [ForeignKey("SellerId")]
        public virtual Seller Seller { get; set; }

        [ForeignKey("RealtorId")]
        public virtual Realtor Realtor { get; set; }

        [ForeignKey("AdministratorId")]
        public virtual Admin Administrator { get; set; }
    }

    // DTO для создания сделки
    public class CreateDealDto
    {
        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string DealType { get; set; }

        public string ContractTerms { get; set; }
        public decimal CommissionPercentage { get; set; } = 3.0;
        public string Notes { get; set; }

        [Required]
        public int PropertyId { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        public int SellerId { get; set; }

        [Required]
        public int RealtorId { get; set; }

        public int? AdministratorId { get; set; }
    }

    // DTO для обновления статуса сделки
    public class UpdateDealStatusDto
    {
        [Required]
        public string Status { get; set; }
    }
}