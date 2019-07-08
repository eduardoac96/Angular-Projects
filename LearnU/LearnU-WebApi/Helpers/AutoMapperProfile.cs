using AutoMapper;
using LearnU_DomainEntities.Users;
using LearnU_WebApi.Models;

namespace LearnU_WebApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Users, UserDisplayDTO>();
            CreateMap<UserMaintenanceDTO, Users>();
        }

    }
}
