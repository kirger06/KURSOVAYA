namespace Rieltors.API.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        //Данные для договора 
        public string PassportSeries { get; set; } // Если нужно хранить паспорт
        public string Inn { get; set; }            // ИНН (для налоговой)

        // Параметры сотрудничества 
        public decimal? DesiredPrice { get; set; }   // Цена, которую хочет продавец
        public bool IsUrgent { get; set; }           // Продажа срочная?
        public string OwnershipDocument { get; set; } // Вид собственности (Свидетельство, Договор)

        //  Статус
        public string Status { get; set; } // "Ожидает агента", "Проводит показы", "Продано"
        public DateTime ContractDate { get; set; } // Дата подписания договора с аге
    }
}
