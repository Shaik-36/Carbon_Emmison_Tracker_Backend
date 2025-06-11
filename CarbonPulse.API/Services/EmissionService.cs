using CarbonPulse.API.DTOs;
using CarbonPulse.API.Interfaces;
using CarbonPulse.API.Models;
using AutoMapper;

namespace CarbonPulse.API.Services
{
    public class EmissionService : IEmissionService
    {
        private readonly IMapper _mapper;

        private static List<Emission> _emissions = new();

        public EmissionService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Task<List<EmissionDto>> GetEmissions(string username)
        {
            var user = AuthService.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
                throw new Exception("User not found");

            var emissions = _emissions
                .Where(e => e.UserId == user.Id)
                .ToList();

            var result = _mapper.Map<List<EmissionDto>>(emissions);

            return Task.FromResult(result);
        }

        public Task AddEmission(string username, EmissionDto emissionDto)
        {
            var user = AuthService.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
                throw new Exception("User not found");

            var emission = _mapper.Map<Emission>(emissionDto);
            emission.User = user;
            emission.UserId = user.Id;

            _emissions.Add(emission);

            return Task.CompletedTask;
        }
    }
}
