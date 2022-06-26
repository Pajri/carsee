using System;
using System.Collections.Generic;
using CarSee.Dtos;
using CarSee.Dtos.Decision;

namespace CarSee.Services.DecisionService
{
    public interface IDecisionService
    {
        public List<CarDecisionDto> ProfileMatching(CriteriaDto criteria, List<CarDecisionDto> carList);
        public CriteriaDto CreateCriteriaDto(DecisionRequestDto dto);
        public List<CarDecisionDto> CreateCarDecisionDto(List<CarDto> carList, WeightRequestDto weight);
        public void SaveResult(DecisionResultDto result);
        public List<CarDecisionDto> GetResult(Guid id);
    }
}