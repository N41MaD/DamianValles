using WeightLifting.Models.DTOs;
using WeightLifting.Persistance.Model;

namespace WeightLifting.Library.Helpers
{
    static public class HelperLibrary
    {
        public static AthleteAttemptResponseDTO CreateAthleteAttemptResponseDTO(Athletes athlete, Dictionary<string, int> attemps)
        {
            return new AthleteAttemptResponseDTO
            {
                Nombre = athlete.Nombre,
                Pais = athlete.Pais,
                IntentosArranque = attemps["StartAttempt"],
                IntentosEnvion = attemps["PushAttempt"]
            };
        }

        public static WeightLiftingResponseDTO CreateResponseEntity(AthletesDTO athlete, int start, int push)
        {

            return new WeightLiftingResponseDTO
            {
                Nombre = athlete.Nombre,
                Arranque = start,
                Envion = push,
                Pais = athlete.Pais,
                TotalPeso = start + push
            };
        }
    }
}
