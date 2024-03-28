using AutoMapper;
using Places.Dto;
using Places.Models;

namespace Places.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserProfile, UserProfileDto>();
            CreateMap<UserProfileDto, UserProfile>();
            CreateMap<UserProfile, GetFurnizorDto>();
            CreateMap<GetFurnizorDto, UserProfile>();
        }
        
    }
}
