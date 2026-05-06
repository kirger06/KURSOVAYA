using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rieltors.API.Models
{
    public class Property
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [Required]
        public string PropertyType { get; set; } // Apartment, House, Land, Commercial

        public double TotalArea { get; set; } // м²

        public double LivingArea { get; set; } // м²

        public int RoomsCount { get; set; }

        public int Floor { get; set; }

        public int TotalFloors { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public string Features { get; set; } // JSON строка с характеристиками

        public string PhotoUrls { get; set; } // JSON массив URL фотографий

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public int? SellerId { get; set; }

        // Навигационные свойства
        [ForeignKey("SellerId")]
        public virtual Seller Seller { get; set; }

        public virtual ICollection<Deal> Deals { get; set; }
    }
}
