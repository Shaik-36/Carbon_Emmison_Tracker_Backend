using AutoMapper;
using CarbonPulse.API.DTOs;
using CarbonPulse.API.Models;

namespace CarbonPulse.API.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<EmissionDto, Emission>();
            CreateMap<Emission, EmissionDto>();
        }
    }
}
