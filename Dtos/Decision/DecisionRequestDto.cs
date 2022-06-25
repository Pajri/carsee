using CarSee.Dtos.Decision;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarSee.Dtos
{
   public class DecisionRequestDto
   {
        public const int IDX_MILEAGE = 0;
        public const int IDX_CONDITION = 1;
        public const int IDX_YEARMADE = 2;
        public const int IDX_BRAND = 3;
        public const int IDX_PRICE = 4;

        public List<CarDto> CarList { get; set; }
       
        public CriteriaRequestDto Criteria {get; set; }
        public WeightRequestDto Weight { get; set; }
        public string[] CriteriaArr { get; set; }
        public string[] WeightArr { get; set; }
    }
}