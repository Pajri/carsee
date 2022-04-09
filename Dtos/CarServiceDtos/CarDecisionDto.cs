using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarSee.Dtos
{
    public class CarDecisionDto : CarDto
    {
        public BrandCriteria BrandCriteria { get; set; }
        
        public ConditionCriteria ConditionCriteria { get; set; }
        
        public MileageCriteria MileageCriteria { get; set; }
        
        public PriceCriteria PriceCriteria { get; set; }
        
        public YearMadeCriteria YearMadeCriteria { get; set; }
        
        public float NCF { get; set; }
        
        public float NSF { get; set; }
        public float NT { get; set; }
        
        
        
        
        
    }
}