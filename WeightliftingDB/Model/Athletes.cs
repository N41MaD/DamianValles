using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeightLifting.Persistance.Model
{
    public class Athletes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AthleteID { get; set; }
        public string Nombre { get; set; }
        public string Pais { get; set; }
        public ICollection<PushAttempts>  PushAttempts {  get; set; }
        public ICollection<StartAttempts> StartupAttempts { get; set; }
    }
}
