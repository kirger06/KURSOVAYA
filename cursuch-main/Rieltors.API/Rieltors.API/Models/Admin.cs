namespace Rieltors.API.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; } // Перенос поля Number сюда
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsActive { get; set; }
                
    }
}
