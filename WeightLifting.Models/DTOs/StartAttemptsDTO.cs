namespace WeightLifting.Models.DTOs
{
    public class StartAttemptsDTO
    {
        public int AttemptID { get; set; }
        public int AthleteID { get; set; }
        public int Arranque { get; set; }
        public int Envion { get; set; }
        public int TotalPeso { get; set; }
        public int AttemptNumber { get; set; }

        public virtual AthletesDTO Athlete { get; set; }
    }
}
