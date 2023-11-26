using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeightLifting.Models.DTOs
{
    public class AttemptDTO
    {
        public int AthleteID { get; set; }
        public int Value { get; set; }
        public int AttemptNumber { get; set; }
    }
}
