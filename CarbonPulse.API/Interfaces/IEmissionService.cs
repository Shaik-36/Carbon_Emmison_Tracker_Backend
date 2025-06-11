using CarbonPulse.API.DTOs;

namespace CarbonPulse.API.Interfaces
{
    public interface IEmissionService
    {
        Task<List<EmissionDto>> GetEmissions(string username);
        Task AddEmission(string username, EmissionDto emissionDto);
    }
}
