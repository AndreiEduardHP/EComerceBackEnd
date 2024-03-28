using Places.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Places.Dto
{
    public class GetFurnizorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public string Rol { get; set; }

        public bool IsDisabled { get; set; }    
        public Company Company { get; set; }

    

        public DateTime DateAccountCreation { get; set; }

        public string? LanguagePreference { get; set; }

        public int? Credit { get; set; }

      
        public byte[]? ProfilePicture { get; set; }
    }
}
