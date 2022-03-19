using System.Collections.Generic;
using CarSee.Dtos;

namespace CarSee.Services.DecisionService
{
    public interface IDecisionService
    {
        public List<CarDto> ProfileMatching(CriteriaDto criteria, List<CarDto> carList);
    }
}