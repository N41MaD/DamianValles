using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WeightLifting.Models.DTOs;
using WeightLifting.Persistance;
using WeightLifting.Persistance.Model;
using WeightLifting.Persistance.Repository.Interfaces;

namespace WeightLiftingPersistance.Repository
{
    public class WeightLiftingRepository : IWeightLiftingRepository
    {
        private readonly WeightliftingContext _context;
        private IMapper _mapper;

        public WeightLiftingRepository(WeightliftingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Athletes>> GetAthletes() =>  await _context.Athletes.ToListAsync();
        public async Task<Athletes?> GetAthleteByID(int athleteID) => await _context.Athletes.FirstOrDefaultAsync(x => x.AthleteID == athleteID);

        public async Task<int> GetHighestStart(int athleteID)
        {
            var result = await _context.StartAttempts.Where(x => x.AthleteID == athleteID).MaxAsync(x => (int?)x.Value);

            return result ?? 0;
        }

        public async Task<int> GetHighestPush(int athleteID)
        {
            var result = await _context.PushAttempts.Where(x => x.AthleteID == athleteID).MaxAsync(x => (int?)x.Value);

            return result ?? 0;
        }

        public async Task<Dictionary<string, int>> GetNumberAttemptsById(int athleteID)
        {
            return new Dictionary<string, int>
            {
                { "StartAttempt", await _context.StartAttempts.Where(x => x.AthleteID == athleteID).CountAsync() },
                { "PushAttempt", await _context.PushAttempts.Where(x => x.AthleteID == athleteID).CountAsync() }
            };
        }

        public async Task<bool> ExistAthlete(int athleteID)
        {
            return await _context.Athletes.AnyAsync(x => x.AthleteID == athleteID);
        }

        public async Task CreateAthlete(AthleteAttemptRequestDTO request)
        {
            var athlete = new Athletes
            {
                Nombre = request.Nombre,
                Pais = request.Pais,

            };

            await _context.Athletes.AddAsync(athlete);

            await _context.SaveChangesAsync();
        }

        public async Task InsertStartAttempt(AthleteAttemptRequestDTO request)
        {
            await InsertAttempt(request, "StartAttempt");
        }

        public async Task InsertPushAttempt(AthleteAttemptRequestDTO request)
        {
            await InsertAttempt(request, "PushAttempt");
        }

        private async Task InsertAttempt(AthleteAttemptRequestDTO request, string attemptType)
        {
            try
            {
                var attempts = await GetNumberAttemptsById(request.AthleteID);

                if (attempts[attemptType] > 2)
                {
                    throw new InvalidOperationException($"El atleta {request.AthleteID} superó la cantidad de intentos permitidos");
                }

                var attempt = new AttemptDTO
                {
                    AthleteID = request.AthleteID,
                    Value = request.Arranque,
                    AttemptNumber = attempts[attemptType] + 1
                };

                switch (attemptType)
                {
                    case "StartAttempt":
                        _context.StartAttempts.Add(_mapper.Map<StartAttempts>(attempt));
                        break;

                    case "PushAttempt":
                        _context.PushAttempts.Add(_mapper.Map<PushAttempts>(attempt));
                        break;

                    default:
                        throw new NotSupportedException($"Tipo de intento no válido: {attemptType}");
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
