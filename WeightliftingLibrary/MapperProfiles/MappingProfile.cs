using AutoMapper;
using WeightLifting.Models.DTOs;
using WeightLifting.Persistance.Model;

namespace WeightLifting.Library.MapperProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AthletesDTO, Athletes>().ReverseMap();
            CreateMap<PushAttemptsDTO, PushAttempts>().ReverseMap();
            CreateMap<StartAttemptsDTO, StartAttempts>().ReverseMap();
            CreateMap<AttemptDTO, StartAttempts>();
            CreateMap<AttemptDTO, PushAttempts>();
        }
    }
}
