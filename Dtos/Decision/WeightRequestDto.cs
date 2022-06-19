using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarSee.Dtos.Decision
{
    public class WeightRequestDto
    {
        public int Mileage { get; set; }
        public int Condition { get; set; }
        public int YearMade { get; set; }
        public int Brand { get; set; }
        public int Price { get; set; }
    }
}
