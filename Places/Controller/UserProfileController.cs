using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Places.Dto;
using Places.Interfaces;
using Places.Models;
using Places.Repository;


namespace Places.Controller
{

    
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        public static string GenerateRandomPassword(int length = 12)
        {
            const string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_~-";
            Random random = new Random();
            char[] chars = new char[length];

            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(validChars.Length)];
            }

            return new string(chars);
        }

        private readonly IUserProfileRepository _userProfileRepository;
   
        private readonly IMapper _mapper;
        public UserProfileController(IUserProfileRepository userProfileRepository , IMapper mapper)
        {
            _userProfileRepository = userProfileRepository;
            _mapper = mapper;
        }

        [HttpPost("UpdateCredit/{userProfileId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCredit(int userProfileId, [FromBody] CreditUpdateDto creditUpdate)
        {
            if (!_userProfileRepository.UserProfileExists(userProfileId))
                return NotFound("User profile not found.");

            if (creditUpdate == null || creditUpdate.Amount <= 0)
            {
                return BadRequest("Invalid credit amount.");
            }

            var userProfile =  _userProfileRepository.GetUserProfile(userProfileId);

            // Update the credits
            userProfile.Credit += creditUpdate.Amount;

            // Add any necessary business logic here. For example, you might want to ensure that credits don't go negative.

            bool isUpdated = _userProfileRepository.UpdateUserProfile(userProfile);
            if (!isUpdated)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong while updating the credits.");
            }

            // Optionally return the updated user profile or just an OK status
            var updatedUserProfileDto = _mapper.Map<UserProfileDto>(userProfile);
            return Ok(updatedUserProfileDto);
        }

        [HttpPost("DeductCredit/{userProfileId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeductCredit(int userProfileId)
        {
            if (!_userProfileRepository.UserProfileExists(userProfileId))
                return NotFound();

            var userProfile = _userProfileRepository.GetUserProfile(userProfileId);

            // Check if the user has enough credits to deduct
            if (userProfile.Credit < 1)
            {
                ModelState.AddModelError("", "Insufficient credits.");
                return BadRequest(ModelState);
            }

            // Deduct 1 credit from the user's profile
            userProfile.Credit -= 1;

            if (!_userProfileRepository.UpdateUserProfile(userProfile))
            {
                ModelState.AddModelError("", "Something went wrong deducting credit.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        [HttpGet("{currentUserId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserProfile>))]
        public async Task<IActionResult> GetUserProfiles(int currentUserId)
        {
            try
            {
                var userProfiles = _userProfileRepository.GetUserProfiles().Where(up => up.Id != currentUserId);

                
                var userProfileDtos = new List<UserProfileDto>();
                foreach (var userProfile in userProfiles)
                {
                  
              


                  
                    var userProfileDto = new UserProfileDto
                    {
                        Id = userProfile.Id,
                        FirstName = userProfile.FirstName,
                        LastName = userProfile.LastName,
                        Username = userProfile.Username,
                        PhoneNumber = userProfile.PhoneNumber,
                        Email = userProfile.Email,
                   
                       
                        ProfilePicture = userProfile.ProfilePicture,
                    
                     
                    };

                    userProfileDtos.Add(userProfileDto);
                }

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(userProfileDtos);
            }
            catch (Exception ex)
            {
                // Handle any errors and return an appropriate response
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("details/{userProfileId}")]
        [ProducesResponseType(200, Type = typeof(UserProfile))]
        [ProducesResponseType(400)]
        public IActionResult GetUserProfile(int userProfileId)
        {
            if (!_userProfileRepository.UserProfileExists(userProfileId))
                return NotFound();

            var userProfile = _mapper.Map<UserProfileDto>(_userProfileRepository.GetUserProfile(userProfileId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(userProfile);
        }
        [HttpGet("getFurnizori")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetUserProfilesFurnizori()
        {
            
            var userProfiles = _userProfileRepository.GetUserProfilesFurnizori();

            if (userProfiles == null)
                return NotFound();

          
            var furnizoriDtoList = userProfiles.Select(up => new GetFurnizorDto
            {
                Id = up.Id,
                FirstName = up.FirstName,
                LastName = up.LastName,
                Username = up.Username,
                PhoneNumber = up.PhoneNumber,
                Email = up.Email,
                Rol = up.Rol,
                DateAccountCreation = up.DateAccountCreation,
                LanguagePreference = up.LanguagePreference,
                Credit = up.Credit,
                ProfilePicture = up.ProfilePicture,
                IsDisabled = up.IsDisabled,
                Company = up.Company
            }).ToList();

          
            return Ok(furnizoriDtoList);
        }


        [HttpGet("CheckIfPhoneNumberExists")]
      
        public IActionResult CheckIfPhoneNumberExists(string phoneNumber)
        {
            if (!_userProfileRepository.CheckPhoneNumberExists(phoneNumber))
                return NoContent();

       

            return Ok();
        }


        [HttpGet("GetUserProfileByPhone/{phoneNumber}")]
        [ProducesResponseType(200, Type = typeof(UserProfile))]
        [ProducesResponseType(400)]
        public IActionResult GetUserProfileByPhone(string phoneNumber)
        {
           
            var userProfile = _mapper.Map<UserProfileDto>(_userProfileRepository.GetUserProfileByPhone(phoneNumber));

            if(userProfile == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(userProfile);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUserProfile([FromBody] UserProfileDto userProfileCreate)
        {
            if (userProfileCreate == null)
                return BadRequest(ModelState);

            var userProfileExist = _userProfileRepository.GetUserProfiles().Where(up => up.PhoneNumber == userProfileCreate.PhoneNumber || up.Email == userProfileCreate.Email).FirstOrDefault();

            if (userProfileExist != null)
            {
                ModelState.AddModelError("", "User Profile already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userProfileMap = _mapper.Map<UserProfile>(userProfileCreate);
            userProfileMap.DateAccountCreation = DateTime.Now;
            userProfileMap.Credit = 0;
            userProfileMap.LanguagePreference = "en";
            userProfileMap.Password = GenerateRandomPassword();
            userProfileMap.IsDisabled = false;

            if (!_userProfileRepository.CreateUserProfile( userProfileMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }

        [HttpPut("{userProfileId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUserProfile(int userProfileId, [FromBody] UserProfileDto updatedUserProfile)
        {
            if (updatedUserProfile == null)
                return BadRequest(ModelState);

            if (userProfileId != updatedUserProfile.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_userProfileRepository.UserProfileExists(userProfileId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userProfileToBeUpdated = _userProfileRepository.GetUserProfile(userProfileId);

            // Update FirstName if provided
            if (!string.IsNullOrWhiteSpace(updatedUserProfile.FirstName))
            {
                userProfileToBeUpdated.FirstName = updatedUserProfile.FirstName;
            }

            // Update LastName if provided
            if (!string.IsNullOrWhiteSpace(updatedUserProfile.LastName))
            {
                userProfileToBeUpdated.LastName = updatedUserProfile.LastName;
            }

            // Update Username if provided
            if (!string.IsNullOrWhiteSpace(updatedUserProfile.Username))
            {
                userProfileToBeUpdated.Username = updatedUserProfile.Username;
            }

            // Update PhoneNumber if provided
            if (!string.IsNullOrWhiteSpace(updatedUserProfile.PhoneNumber))
            {
                userProfileToBeUpdated.PhoneNumber = updatedUserProfile.PhoneNumber;
            }

            // Update Email if provided
            if (!string.IsNullOrWhiteSpace(updatedUserProfile.Email))
            {
                userProfileToBeUpdated.Email = updatedUserProfile.Email;
            }

            // Update ProfilePicture if provided
            if (updatedUserProfile.ProfilePicture != null)
            {
                userProfileToBeUpdated.ProfilePicture = updatedUserProfile.ProfilePicture;
            }

            if (!_userProfileRepository.UpdateUserProfile(userProfileToBeUpdated))
            {
                ModelState.AddModelError("", "Something went wrong updating the user profile");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        [HttpDelete("{userProfileId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUserProfile(int userProfileId)
        {
            if (!_userProfileRepository.UserProfileExists(userProfileId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            return NoContent();
        }

        [HttpPost("UpdateUserImage/{userProfileId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUserImage(int userProfileId, IFormFile imageFile)
        {
            if (!_userProfileRepository.UserProfileExists(userProfileId))
                return NotFound();

            if (imageFile == null || imageFile.Length <= 0)
            {
                ModelState.AddModelError("Image", "Please provide a valid image file.");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Read the image data from the uploaded file into a byte array
            byte[] imageData;
            using (var stream = new MemoryStream())
            {
                imageFile.CopyTo(stream);
                imageData = stream.ToArray();
            }

            // Update the user's profile with the image data
            var userProfile = _userProfileRepository.GetUserProfile(userProfileId);
            userProfile.ProfilePicture = imageData; // Assuming you have a property for profile image in your UserProfile model

            if (!_userProfileRepository.UpdateUserProfile(userProfile))
            {
                ModelState.AddModelError("", "Something went wrong updating user image.");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }


        [HttpPost("UpdateUserNotificationToken/{userProfileId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUserNotificationToken(int userProfileId,string notificationToken)
        {
            if (!_userProfileRepository.UserProfileExists(userProfileId))
                return NotFound();

            var userProfile = _userProfileRepository.GetUserProfile(userProfileId);
      

            if (!_userProfileRepository.UpdateUserProfile(userProfile))
            {
                ModelState.AddModelError("", "Something went wrong updating user image.");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }


        [HttpPost("reset-password/{userId}")]
        public async Task<IActionResult> ResetPassword(int userId, [FromBody] ResetPasswordDto resetPasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _userProfileRepository.GetUserProfile(userId);
            if (user == null)
            {
                return NotFound(new { message = "Utilizatorul nu a fost găsit." });
            }

         
            if (user.Password != resetPasswordDto.OldPassword)
            {
               
                return BadRequest(new { message = "Parola veche nu este corectă." });
            }

        
            user.Password = resetPasswordDto.NewPassword;
            try
            {
                
                 _userProfileRepository.UpdateUserProfile(user);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, new { message = "A apărut o eroare la actualizarea parolei." });
            }
            return Ok();

        }

        [HttpPut("{userId}/isDisabled")]
        public IActionResult UpdateUserStatus(int userId, [FromBody] bool isDisabled)
        {
            try
            {
                var result = _userProfileRepository.UpdateUserStatus(userId, isDisabled);
                if (result)
                {
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
