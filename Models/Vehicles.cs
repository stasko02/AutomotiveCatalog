using Microsoft.EntityFrameworkCore;

namespace AutomotiveCatalog.Models
{
    public class Vehicles
    {  
        public int ID { get; set; } 
        public string Maker { get; set; }
        public string Model { get; set; }
        public int ProductionYear { get; set; }
        public int Displacement { get; set; }
        public int Power { get; set; }
        public int TopSpeed { get; set; }
        public Vehicles() { }

    }
}
