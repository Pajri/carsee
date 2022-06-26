using CarSee.Dtos.Decision;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarSee.Dtos
{
   public class DecisionRequestDto
   {
        public CriteriaRequestDto Criteria {get; set; }
        public WeightRequestDto Weight { get; set; }
        public string UUID { get; set; }
        public Guid CarId { get; set; }
        public string[] CriteriaArr { get; set; }
        public string[] WeightArr { get; set; }
    }
}