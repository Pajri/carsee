using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarSee.Dtos.Decision
{
    public class WeightRequestDto
    {
        public string[] Mileage { get; set; }
        public string[] Condition { get; set; }
        public string[] YearMade { get; set; }
        public string[] Brand { get; set; }
        public string[] Price { get; set; }
    }
}
