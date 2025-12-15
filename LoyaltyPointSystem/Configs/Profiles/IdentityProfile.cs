using AutoMapper;
using LoyaltyPointSystem.Features.Identity;
using LoyaltyPointSystem.Features.Identity.DTOs;

namespace LoyaltyPointSystem.Configs.Profiles;

public class IdentityProfile : Profile
{
    public IdentityProfile()
    {
       CreateMap<RegisterCommand, User>();
       CreateMap<User, UserProfileDto>()
           .ReverseMap();
    }
}