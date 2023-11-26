using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WeightLifting.Persistance.Model
{
    public class PushAttempts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AttemptID { get; set; }
        public int AthleteID { get; set; }
        public int Value {  get; set; }
        public int AttemptNumber { get; set; }

        [ForeignKey("AthleteID")]
        public virtual Athletes Athlete { get; set; }

    }
}
