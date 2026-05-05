using System.Xml;

namespace Rieltors.API.Models
{
    public class Realtor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        // --- Учетные данные для входа в CRM/API -- (если у риелтора отдельный вход)
        public string PasswordHash { get; set; }

        // --- Профессиональные данные ---
        public string LicenseNumber { get; set; } // Номер лицензии/сертификата
        public string Specialization { get; set; } // Специализация (Жилая, Коммерческая, Загородная)
        public decimal CommissionRate { get; set; } // Ставка комиссии риелтора (например, 30% от сделки)

        //Статистика и статус 
        public bool IsActive { get; set; } // Работает ли сейчас риелтор
        public DateTime HireDate { get; set; }
        public int SalesCount { get; set; } // Количество проведенных сделок (для рейтинга)

        // Связи (Navigation Properties) 
        // Какие сделки провел этот риелтор?
        public virtual List<Deal> Deals { get; set; }

        // Какие объекты недвижимости находятся у него в работе?
        public virtual List<RealEstate> Properties { get; set; }
        // Кто является его начальником (если у вас есть иерархия)?
        public int? AdministratorId { get; set; }
    }
}
