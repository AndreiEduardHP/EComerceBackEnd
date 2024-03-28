using Places.Dto;
using Places.Models;

namespace Places.Interfaces
{
    public interface IUserProfileRepository
    {
        ICollection<UserProfile> GetUserProfiles();
        ICollection<UserProfile> GetUserProfilesFurnizori();
        UserProfile GetUserProfile(int id);

        UserProfile GetUserProfileByEmail(string email);
        UserProfile GetUserProfileByPhone(string phoneNumber);
        bool UserProfileExists(int id);
        bool CreateUserProfile(UserProfile userProfile);
        bool UpdateUserProfile(UserProfile userProfile);
        bool DeleteUserProfile(UserProfile userProfile);
        bool Save();
        bool CheckPhoneNumberExists(string phoneNumber);
        bool UpdateUserStatus(int userId, bool isDisabled);

        Task<bool> UpdateUserPreferences(int userId, UserProfileDto preferences);
    }
}
