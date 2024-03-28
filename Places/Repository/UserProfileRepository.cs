using Microsoft.EntityFrameworkCore;
using Places.Data;
using Places.Dto;
using Places.Interfaces;
using Places.Models;

namespace Places.Repository
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly PlacesContext _context;
        public UserProfileRepository(PlacesContext context)
        {
            _context = context;
        }

        public UserProfile GetUserProfile(int id)
        {
            return _context.UserProfile.Where(up => up.Id == id).FirstOrDefault();
        }
        public UserProfile GetUserProfileByEmail(string email)
        {
            return _context.UserProfile.Where(up => up.Email == email).FirstOrDefault();
        }

        public UserProfile GetUserProfileByPhone(string phoneNumber)
        {
            return _context.UserProfile.Where(up => up.PhoneNumber == phoneNumber).FirstOrDefault();
        }

        public ICollection<UserProfile> GetUserProfilesFurnizori()
        {
            return _context.UserProfile.Where(up => up.Rol == "furnizor").Include(up => up.Company).OrderBy(up => up.Id).ToList();
        }
        public ICollection<UserProfile> GetUserProfiles()
        {
            return _context.UserProfile.OrderBy(up => up.Id).ToList();
        }

        public bool UserProfileExists(int id)
        {
            return _context.UserProfile.Any(up => up.Id == id);
        }

        public bool CreateUserProfile(UserProfile userProfile)
        {
            
            _context.UserProfile.Add(userProfile);

            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateUserProfile(UserProfile userProfile)
        {
            _context.UserProfile.Update(userProfile);
            
            return Save();
        }

        public bool DeleteUserProfile(UserProfile userProfile)
        {
            _context.UserProfile.Remove(userProfile);
            return Save();
        }

        public bool CheckPhoneNumberExists(string phoneNumber)
        {
            return _context.UserProfile.Any(up => up.PhoneNumber == phoneNumber);
        }



        public async Task<bool> UpdateUserPreferences(int userId, UserProfileDto preferences)
        {
            var userProfile = await _context.UserProfile.FirstOrDefaultAsync(up => up.Id == userId);
            if (userProfile == null) return false;

            if (preferences.LanguagePreference != null)
            {
                userProfile.LanguagePreference = preferences.LanguagePreference;
            }
        

            return Save(); 
        }

        public bool UpdateUserStatus(int userId, bool isDisabled)
        {
            var user = _context.UserProfile.FirstOrDefault(p => p.Id == userId);
            if (user != null)
            {
                user.IsDisabled = isDisabled;
                _context.SaveChanges();
                return true;
            }
            return false;
        }


    }
}
