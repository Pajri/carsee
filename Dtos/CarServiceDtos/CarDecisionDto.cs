using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarSee.Dtos
{
    public class CarDecisionDto : CarDto
    {
        public List<ICriteria> Criteria { get; set; }
    }
}