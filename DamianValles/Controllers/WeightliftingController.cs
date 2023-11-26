using Microsoft.AspNetCore.Mvc;
using WeightLifting.Models.DTOs;
using WeightLiftingLibrary.Business.Interfaces;
using WeightLifting.Library.Logger;
using Microsoft.AspNetCore.Authorization;

namespace WeightLifting.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WeightliftingController : ControllerBase
    {
        private IWeightLiftingService _weightLiftingService;
        private readonly ILoggerService _logger;

        public WeightliftingController(IWeightLiftingService weightLiftingService, ILoggerService logger)
        {
            _weightLiftingService = weightLiftingService;
            _logger = logger;
        }


        [HttpGet("GetTotals")]
        public async Task<ActionResult<IEnumerable<WeightLiftingResponseDTO>>> GetTotals()
        {
            _logger.Info("GetTotals started");
            var result = await _weightLiftingService.GetTotals();
            _logger.Info("GetTotals finished");
            return Ok(result);
        }

        
        [HttpGet("GetNumberAttemptsById/{athleteId}")]
        public async Task<ActionResult<IEnumerable<WeightLiftingResponseDTO>>> GetNumberAttemptsById(int athleteId)
        {
            _logger.Info("GetNumberAttemptsById started");
            var result = await _weightLiftingService.GetNumberAttemptsById(athleteId);
            
            if (result.Nombre == null)
            {
                return NotFound(new { Message = $"No athletes were found with the ID {athleteId}" });
            }

            _logger.Info("GetNumberAttemptsById finished");
            return Ok(result);
        }

        
        [HttpPost("InsertAttempt")]
        public async Task<IActionResult> InsertAthleteAttemp([FromBody] AthleteAttemptRequestDTO athleteAttempt)
        {
            _logger.Info("InsertAthleteAttemp started");

            var result = await _weightLiftingService.InsertAthleteAttempt(athleteAttempt);

            if (result)
            {
                _logger.Info("InsertAthleteAttemp finished");

                return Ok(new { Mensaje = "Record successfully inserted" });
            }

            _logger.Warn("InsertAthleteAttemp finished with errors");

            return UnprocessableEntity("The athlete exceeded the number of attempts allowed.");

        }

    }
}
