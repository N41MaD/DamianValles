using WeightLifting.Models.DTOs;

namespace WeightLiftingLibrary.Business.Interfaces
{
    public interface IWeightLiftingService
    {
        Task<IEnumerable<AthletesDTO>> GetAthletes();
        Task<IEnumerable<WeightLiftingResponseDTO>> GetTotals();
        Task<AthleteAttemptResponseDTO> GetNumberAttemptsById(int athleteID);
        Task<bool> InsertAthleteAttempt(AthleteAttemptRequestDTO athleteAttempt);
    }
}
