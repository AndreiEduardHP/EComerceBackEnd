using Places.Models;
using System.Security.Policy;

namespace Places.Dto
{
    public class UserProfileDto
    {
        public int? Id { get; set; }
        public string? FirstName { get; set; }
        public DateTime DateAccountCreation { get; set; }
        public string? LanguagePreference { get; set; }
        public string? LastName { get; set; }

        public int? CompanyId { get; set; }
        public Company? Company { get; set; }    
        public string? Rol {  get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Country { get; set; }
        public int? Credit {  get; set; }
        public byte[]? ProfilePicture { get; set; }

    }
}
