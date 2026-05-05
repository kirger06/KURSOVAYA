namespace Rieltors.API.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        //  Фильтры поиска недвижимости 
        public decimal? MinPrice { get; set; }      // Диапазон бюджета
        public decimal? MaxPrice { get; set; }
        public string PreferredDistrict { get; set; } // Желаемый район
        public int? MinRooms { get; set; }            // Мин. количество комнат
        public string PropertyType { get; set; }      // Квартира, Дом, Коммерция

        // Статус 
        public string Status { get; set; } // "Активный поиск", "Купил", "Передумал"
        public DateTime RegistrationDate { get; set; }

        // Связь с исполнителем
        public int? AdministratorId { get; set; } // Какой админ ведет этого клиента
    }
}
