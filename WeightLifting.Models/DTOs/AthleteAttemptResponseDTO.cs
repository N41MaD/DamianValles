using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeightLifting.Models.DTOs
{
    public class AthleteAttemptResponseDTO
    {
        public string Nombre { get; set; }
        public string Pais { get; set; }
        public int IntentosArranque { get; set; }
        public int IntentosEnvion { get; set; }
    }
}
