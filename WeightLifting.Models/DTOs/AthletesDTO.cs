namespace WeightLifting.Models.DTOs
{
    public class AthletesDTO
    {
        public int AthleteID { get; set; }
        public string Nombre { get; set; }
        public string Pais { get; set; }
        public ICollection<PushAttemptsDTO> Attempts { get; set; }
    }
}
