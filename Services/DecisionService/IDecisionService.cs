using System.Collections.Generic;
using CarSee.Dtos;

namespace CarSee.Services.DecisionService
{
    public interface IDecisionService
    {
        public List<CarDecisionDto> ProfileMatching(CriteriaDto criteria, List<CarDecisionDto> carList);
        public CriteriaDto CreateCriteriaDto(DecisionRequestDto dto);
        public List<CarDecisionDto> CreateCarDecisionDto(List<CarDto> carList);
    }
}