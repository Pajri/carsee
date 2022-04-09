using System.Collections.Generic;

namespace CarSee.Dtos
{
    public class CriteriaDto
    { 
        public CoreFactor CoreFactor { get; set; }
        public SecondaryFactor SecondaryFactor { get; set; }

        public float CoreFactorRate {get;set;}
        public float SecondaryFactorRate {get;set;}
    }

    public class CoreFactor
    {
        public PriceCriteria PriceCriteria { get; set; }
        public YearMadeCriteria YearMadeCriteria { get; set; }
        
        public ConditionCriteria ConditionCriteria { get; set; }
    }

    public class SecondaryFactor
    {
        public BrandCriteria BrandCriteria { get; set; }
        
        public MileageCriteria MileageCriteria { get; set; }
    }
}

