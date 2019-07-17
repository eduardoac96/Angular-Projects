using AutoMapper;
using StoreU_DomainEntities.Users;
using StoreU_WebApi.Model;

namespace StoreU_WepApi.Helpers
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {
            base.CreateMap<UserDto, Users>().ReverseMap();

        } 
    }
}
