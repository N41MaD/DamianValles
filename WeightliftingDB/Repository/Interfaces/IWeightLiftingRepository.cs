using WeightLifting.Models.DTOs;
using WeightLifting.Persistance.Model;

namespace WeightLifting.Persistance.Repository.Interfaces
{
    public interface IWeightLiftingRepository
    {
        Task<IEnumerable<Athletes>> GetAthletes();
        Task<Athletes?> GetAthleteByID(int athleteID);
        Task<int> GetHighestStart(int athleteID);
        Task<int> GetHighestPush(int athleteID);
        Task<Dictionary<string, int>> GetNumberAttemptsById(int athleteID);
        Task InsertStartAttempt(AthleteAttemptRequestDTO request);
        Task InsertPushAttempt(AthleteAttemptRequestDTO request);
        Task<bool> ExistAthlete(int athleteID);
        Task CreateAthlete(AthleteAttemptRequestDTO request);

    }
}
