using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarSee.Dtos
{
   public class DecisionRequestDto
   {
       public List<CarDto> CarList { get; set; }
       
       public CriteriaRequestDto Criteria {get; set; }
   }
}