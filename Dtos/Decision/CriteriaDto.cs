using System.Collections.Generic;

namespace CarSee.Dtos
{
    public class CriteriaDto
    { 
        public List<ICriteria> CoreFactor { get; set; }
        public List<ICriteria> SecondaryFactor { get; set; }

        public float CoreFactorRate {get;set;}
        public float SecondaryFactorrate {get;set;}
    }
}