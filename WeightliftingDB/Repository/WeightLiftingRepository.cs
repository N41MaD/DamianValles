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
        public async Task<Athletes?> GetAthleteByName(string name) => await _context.Athletes.FirstOrDefaultAsync(x => x.Nombre == name);

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

        public async Task<int> ExistAthlete(string name)
        {
            var athlete = await _context.Athletes.FirstOrDefaultAsync(x => x.Nombre.ToLower() == name.ToLower());

            if (athlete is null)
                return -1;

            return athlete.AthleteID;
        }

        public async Task<int> CreateAthlete(AthleteAttemptRequestDTO request)
        {
            var athlete = new Athletes
            {
                Nombre = request.Nombre,
                Pais = request.Pais,
            };

            await _context.Athletes.AddAsync(athlete);

            await _context.SaveChangesAsync();

            var athleteCreated = await GetAthleteByName(request.Nombre);

            return athleteCreated.AthleteID;
        }

        public async Task InsertStartAttempt(AthleteAttemptRequestDTO request, int id)
        {
            await InsertAttempt(request, id, "StartAttempt");
        }

        public async Task InsertPushAttempt(AthleteAttemptRequestDTO request, int id)
        {
            await InsertAttempt(request, id, "PushAttempt");
        }

        private async Task InsertAttempt(AthleteAttemptRequestDTO request, int id, string attemptType)
        {
            try
            {
                var athlete = await GetAthleteByName(request.Nombre);

                var attempts = await GetNumberAttemptsById(id);

                if (attempts[attemptType] > 2)
                {
                    throw new InvalidOperationException($"El atleta {athlete.Nombre} superó la cantidad de intentos permitidos");
                }

                var attempt = new AttemptDTO
                {
                    AthleteID = id,
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
