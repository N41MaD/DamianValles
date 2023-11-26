using AutoMapper;
using WeightLifting.Models.DTOs;
using WeightLifting.Persistance.Repository.Interfaces;
using WeightLiftingLibrary.Business.Interfaces;
using WeightLifting.Library.Helpers;
using WeightLifting.Library.Logger;
using System.Runtime.CompilerServices;

namespace WeightLiftingLibrary.Business
{
    public class WeightLiftingService : IWeightLiftingService
    {
        private IWeightLiftingRepository _repository;
        private IMapper _mapper;
        private readonly ILoggerService _logger;

        public WeightLiftingService(IWeightLiftingRepository repository, IMapper mapper, ILoggerService logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<IEnumerable<AthletesDTO>> GetAthletes()
        {
            var result = await _repository.GetAthletes();

            return _mapper.Map<IEnumerable<AthletesDTO>>(result);
        }

        public async Task<IEnumerable<WeightLiftingResponseDTO>> GetTotals()
        {
            var response = new List<WeightLiftingResponseDTO>();

            try
            {
                _logger.Info("Getting athletes");
                var athletes = await GetAthletes();

                 foreach (var athlete in athletes)
                {
                    var start = await _repository.GetHighestStart(athlete.AthleteID);
                    var push = await _repository.GetHighestPush(athlete.AthleteID);

                    response.Add(HelperLibrary.CreateResponseEntity(athlete, start, push));
                }
            }
            catch (Exception ex )
            {
                _logger.Error(ex.Message);
            }

            return response.OrderByDescending(x => x.TotalPeso);
        }
        public async Task<AthleteAttemptResponseDTO> GetNumberAttemptsById(int athleteID)
        {
            try
            {
                var athlete = await _repository.GetAthleteByID(athleteID) ?? throw new InvalidOperationException($"No se encontró ningún atleta con el ID {athleteID}");

                var attemps = await _repository.GetNumberAttemptsById(athleteID);

                return HelperLibrary.CreateAthleteAttemptResponseDTO(athlete, attemps);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new AthleteAttemptResponseDTO();
            }
        }

        public async Task<bool> InsertAthleteAttempt(AthleteAttemptRequestDTO athleteAttempt)
        {
            var result = false;
            try
            {
                var id = await _repository.ExistAthlete(athleteAttempt.Nombre);

                if (id < 0)
                {
                    id = await _repository.CreateAthlete(athleteAttempt);
                }

                await _repository.InsertStartAttempt(athleteAttempt, id);
                await _repository.InsertPushAttempt(athleteAttempt, id);
                result = true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }

            return result;
        }
    }
}
