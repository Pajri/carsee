using System.Collections.Generic;
using System.Linq;
using CarSee.Dtos;
using CarSee.EntityFramework;
using CarSee.Services.DecisionService;
using CarSee.Entities;
using System;
using Newtonsoft.Json;

namespace MyNamespace
{
    public class DecisionService : IDecisionService
    {
        private ApplicationDbContext _ctx;
        public DecisionService(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public List<CarDecisionDto> ProfileMatching(CriteriaDto criteria, List<CarDecisionDto> carList)
        {
            
            foreach (var car in carList)
            {
                /*calculate gap*/
                //core 
                // car.PriceCriteria
                //     .CalculateGap(criteria.CoreFactor.PriceCriteria.MapCriteria());
                // car.YearMadeCriteria
                //     .CalculateGap(criteria.CoreFactor.YearMadeCriteria.MapCriteria());
                // car.ConditionCriteria
                //     .CalculateGap(criteria.CoreFactor.ConditionCriteria.MapCriteria());

                car.PriceCriteria.Gap = criteria.CoreFactor
                    .PriceCriteria.CalculateGap(car.PriceCriteria.MapCriteria());
                car.YearMadeCriteria.Gap = criteria.CoreFactor
                    .YearMadeCriteria.CalculateGap(car.YearMadeCriteria.MapCriteria());
                car.ConditionCriteria.Gap = criteria.CoreFactor
                    .ConditionCriteria.CalculateGap(car.ConditionCriteria.MapCriteria());
                    
                //secondary
                // car.BrandCriteria
                //     .CalculateGap(criteria.SecondaryFactor.BrandCriteria.MapCriteria());
                // car.MileageCriteria
                //     .CalculateGap(criteria.SecondaryFactor.MileageCriteria.MapCriteria());

                car.BrandCriteria.Gap = criteria.SecondaryFactor
                    .BrandCriteria.CalculateGap(car.BrandCriteria.MapCriteria());
                car.MileageCriteria.Gap = criteria.SecondaryFactor
                    .MileageCriteria.CalculateGap(car.MileageCriteria.MapCriteria());

                /*calculate ncf and nsf*/
                car.NCF = (car.PriceCriteria.MappedGap + car.YearMadeCriteria.MappedGap + car.ConditionCriteria.MappedGap)/3;
                car.NSF = (car.BrandCriteria.MappedGap + car.MileageCriteria.MappedGap);

                /*calclate NT*/
                car.NT = criteria.CoreFactorRate*car.NCF + criteria.SecondaryFactorRate*car.NSF;
            }

            return  carList.OrderByDescending(o => o.NT).ToList();
        }

        public CriteriaDto CreateCriteriaDto(DecisionRequestDto dto)
        {
            CriteriaDto criteriaDto = new CriteriaDto();
            criteriaDto.CoreFactor = new CoreFactor()
            {
                ConditionCriteria = new ConditionCriteria(dto.Criteria.Condition),
                PriceCriteria = new PriceCriteria(dto.Criteria.Price),
                YearMadeCriteria = new YearMadeCriteria(dto.Criteria.YearMade)
            };

            criteriaDto.SecondaryFactor = new SecondaryFactor
            {
                BrandCriteria = new BrandCriteria(dto.Criteria.Brand),
                MileageCriteria = new MileageCriteria(dto.Criteria.Mileage)
            };

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
                    UserId = car.UserId
                };

                carDecision.PriceCriteria = new PriceCriteria(car.Price);
                carDecision.YearMadeCriteria = new YearMadeCriteria(car.ProductionYear);
                carDecision.ConditionCriteria = new ConditionCriteria(car.Condition);
                carDecision.BrandCriteria = new BrandCriteria(car.Brand);
                carDecision.MileageCriteria = new MileageCriteria(car.Mileage);

                carDecisionList.Add(carDecision);

            }
            return carDecisionList;
        }

        public void SaveResult(DecisionResultDto result)
        {
            Guid id = Guid.NewGuid();
            if(result.Id != Guid.Empty) id = result.Id;
            var decisionResult = new DecisionResult()
            {
                Id = id,
                Result = result.Result
            };
            _ctx.Add(decisionResult);
            _ctx.SaveChanges();
        }

        public List<CarDecisionDto> GetResult(Guid id)
        {
            var result = _ctx.DecisionResults.Where(d => d.Id == id).FirstOrDefault();
            if(result == null) return null;

            var decisionResult = JsonConvert.DeserializeObject<List<CarDecisionDto>>(result.Result);
            return decisionResult;
        }
    }
}