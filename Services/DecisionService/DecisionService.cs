using System.Collections.Generic;
using CarSee.Dtos;
using CarSee.Services.DecisionService;

namespace MyNamespace
{
    public class DecisionService : IDecisionService
    {
        public List<CarDto> ProfileMatching(CriteriaDto criteria, List<CarDecisionDto> carList)
        {
            
            return null;
        }

        public CriteriaDto CreateCriteriaDto(DecisionRequestDto dto)
        {
            CriteriaDto criteriaDto = new CriteriaDto();
            criteriaDto.CoreFactor = new List<ICriteria>();
            criteriaDto.SecondaryFactor = new List<ICriteria>();

            criteriaDto.CoreFactor.Add(new PriceCriteria(dto.Criterna.Price));
            criteriaDto.CoreFactor.Add(new YearMadeCriteria(dto.Criterna.YearMade));
            criteriaDto.CoreFactor.Add(new ConditionCriteria(dto.Criterna.Condition));

            criteriaDto.SecondaryFactor.Add(new BrandCriteria(dto.Criterna.Brand));
            criteriaDto.SecondaryFactor.Add(new MileageCriteria(dto.Criterna.Mileage));

            criteriaDto.CoreFactorRate = 0.6f;
            criteriaDto.SecondaryFactorRate = 0.4f;

            return criteriaDto;
        }

        public List<CarDecisionDto> CreateCarDecisionDto(List<CarDto> carList)
        {   
            var carDecisionList = new List<CarDecisionDto>();
            foreach (var car in carList)
            {
                var carDecision = new CarDecisionDto
                {
                    Id = car.Id,
                    Name = car.Name,
                    Price = car.Price,
                    Brand = car.Brand,
                    ProductionYear = car.ProductionYear,
                    Condition = car.Condition,
                    Description = car.Description,
                    Mileage = car.Mileage,
                    ImageFileName = car.ImageFileName,
                    UserId = car.UserId,
                    Criteria = new List<ICriteria>()
                };

                
                carDecision.Criteria.Add(new PriceCriteria(car.Price));
                carDecision.Criteria.Add(new YearMadeCriteria(car.ProductionYear));
                carDecision.Criteria.Add(new ConditionCriteria(car.Condition));
                carDecision.Criteria.Add(new BrandCriteria(car.Brand));
                carDecision.Criteria.Add(new MileageCriteria(car.Mileage));

                carDecisionList.Add(carDecision);

            }
            return carDecisionList;
        }

    }
}