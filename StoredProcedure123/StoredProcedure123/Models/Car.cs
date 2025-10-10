using System.ComponentModel.DataAnnotations;

namespace StoredProcedure123.Models
{
    public class Car
    {
        [Key]
        public Guid ID { get; set; }
        public string Brand { get; set; }
        public int Horsepower { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
    }
}
