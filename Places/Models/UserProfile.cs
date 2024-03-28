using System.ComponentModel.DataAnnotations.Schema;

namespace Places.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsDisabled {  get; set; }

        public string Rol {  get; set; }

        public string Password { get; set; }

        public DateTime DateAccountCreation { get; set; }

        public string? LanguagePreference { get; set; }

        public int? Credit { get; set; }

        [Column(TypeName = "VARBINARY(MAX)")]
        public byte[]? ProfilePicture { get; set; }

        public ICollection<Order> Orders { get; set; }

        public int? CompanyId { get; set; }  
        public Company Company { get; set; }

  






    }
}
