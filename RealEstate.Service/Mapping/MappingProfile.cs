using AutoMapper;
using RealEstate.Data.Models;
using RealEstate.Service.ViewModels;

namespace RealEstate.Service.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDetail>();

            CreateMap<UserRegistration, User>();
            
        }
        
    }
}