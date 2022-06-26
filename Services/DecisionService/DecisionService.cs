using System.Collections.Generic;
using System.Linq;
using CarSee.Dtos;
using CarSee.EntityFramework;
using CarSee.Services.DecisionService;
using CarSee.Entities;
using System;
using Newtonsoft.Json;
using CarSee.Constants;
using CarSee.Dtos.Decision;

namespace CarSee.Services.DecisionService
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
                car.PriceCriteria.Gap = criteria.CoreFactor
                    .PriceCriteria.CalculateGap(car.PriceCriteria.GetWeightValue());
                car.PriceCriteria.ConvertGap();

                car.YearMadeCriteria.Gap = criteria.CoreFactor
                    .YearMadeCriteria.CalculateGap(car.YearMadeCriteria.GetWeightValue());
                car.YearMadeCriteria.ConvertGap();

                car.ConditionCriteria.Gap = criteria.CoreFactor
                    .ConditionCriteria.CalculateGap(car.ConditionCriteria.GetWeightValue());
                car.ConditionCriteria.ConvertGap();

                //secondary
                car.BrandCriteria.Gap = criteria.SecondaryFactor
                    .BrandCriteria.CalculateGap(car.BrandCriteria.GetWeightValue());
                car.BrandCriteria.ConvertGap();

                car.MileageCriteria.Gap = criteria.SecondaryFactor
                    .MileageCriteria.CalculateGap(car.MileageCriteria.GetWeightValue());
                car.MileageCriteria.ConvertGap();

                /*calculate ncf and nsf*/
                car.NCF = (car.PriceCriteria.MappedGap + car.YearMadeCriteria.MappedGap + car.BrandCriteria.MappedGap)/3;
                car.NSF = (car.ConditionCriteria.MappedGap + car.MileageCriteria.MappedGap)/2;

                /*calclate NT*/
                car.NT = criteria.CoreFactorRate*car.NCF + criteria.SecondaryFactorRate*car.NSF;

                if(car.NT >=5) car.MatchingLabel = MatchingLabels.MATCHING_SANGAT_COCOK;
                else if(car.NT >= 4) car.MatchingLabel = MatchingLabels.MATCHING_COCOK;
                else if(car.NT >= 3) car.MatchingLabel = MatchingLabels.MATCHING_CUKUP_COCOK;
                else car.MatchingLabel = MatchingLabels.MATCHING_KURANG_COCOK;

            }

            return  carList.OrderByDescending(o => o.NT).ToList();
        }

        public CriteriaDto CreateCriteriaDto(DecisionRequestDto dto)
        {
            int mileage = int.Parse(dto.Criteria.Mileage);
            int condition = int.Parse(dto.Criteria.Condition);
            int price = int.Parse(dto.Criteria.Price);
            int yearMade = int.Parse(dto.Criteria.YearMade);
            int brand = int.Parse(dto.Criteria.Brand);

            CriteriaDto criteriaDto = new CriteriaDto();
            criteriaDto.CoreFactor = new CoreFactor()
            {
                ConditionCriteria = new ConditionCriteria(condition),
                PriceCriteria = new PriceCriteria(price),
                YearMadeCriteria = new YearMadeCriteria(yearMade)
            };

            criteriaDto.SecondaryFactor = new SecondaryFactor
            {
                BrandCriteria = new BrandCriteria(brand),
                MileageCriteria = new MileageCriteria(mileage)
            };

            criteriaDto.CoreFactorRate = 0.6f;
            criteriaDto.SecondaryFactorRate = 0.4f;

            return criteriaDto;
        }

        public List<CarDecisionDto> CreateCarDecisionDto(string UUID, WeightRequestDto weight)
        {
            var carList = _ctx.Car.Where(c => c.UUID == UUID);

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

                carDecision.PriceCriteria = new PriceCriteria(car.Price, weight.Price);
                carDecision.YearMadeCriteria = new YearMadeCriteria(car.ProductionYear, weight.YearMade);
                carDecision.ConditionCriteria = new ConditionCriteria(car.Condition, weight.Condition);
                carDecision.BrandCriteria = new BrandCriteria(car.Brand, weight.Brand);
                carDecision.MileageCriteria = new MileageCriteria(car.Mileage, weight.Mileage);

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