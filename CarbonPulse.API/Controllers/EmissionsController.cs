using CarbonPulse.API.DTOs;
using CarbonPulse.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarbonPulse.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]  // 🔒 Protected
    public class EmissionsController : ControllerBase
    {
        private readonly IEmissionService _emissionService;

        public EmissionsController(IEmissionService emissionService)
        {
            _emissionService = emissionService;
        }

        [HttpPost]
        public async Task<IActionResult> AddEmission(EmissionDto dto)
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            if (username == null) return Unauthorized();

            await _emissionService.AddEmission(username, dto);
            return Ok("Emission added.");
        }

        [HttpGet]
        public async Task<ActionResult<List<EmissionDto>>> GetEmissions()
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            if (username == null) return Unauthorized();

            var emissions = await _emissionService.GetEmissions(username);
            return Ok(emissions);
        }
    }
}
