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
        Task InsertStartAttempt(AthleteAttemptRequestDTO request, int id);
        Task InsertPushAttempt(AthleteAttemptRequestDTO request, int id);
        Task<int> ExistAthlete(string name);
        Task<int> CreateAthlete(AthleteAttemptRequestDTO request);
        Task<Athletes?> GetAthleteByName(string name);

    }
}
