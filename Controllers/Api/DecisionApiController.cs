using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarSee.Dtos;
using CarSee.Services.CarService;
using CarSee.Services.DecisionService;
using CarSee.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CarSee.Controllers.Api
{
    [Route("api/decision")]
    [ApiController]
    public class DecisionApiController : ControllerBase
    {   
        private readonly IDecisionService _service;
        public DecisionApiController(IDecisionService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<CarApiListingViewModel> ProfileMatching(CriteriaRequestDto criteria, List<CarDto> carList)
        {
            
           
            List<ICriteria> core = new List<ICriteria>();
            core.Add(new PriceCriteria(criteria.Price));
            core.Add(new YearMadeCriteria(criteria.YearMade));
            core.Add(new ConditionCriteria(criteria.Condition));

            List<ICriteria> secondary = new List<ICriteria>();
            secondary.Add(new BrandCriteria(criteria.Brand));
            secondary.Add(new MileageCriteria(criteria.Mileage));

            CriteriaDto criteriaDto = new CriteriaDto()
            {
                CoreFactor = core,
                SecondaryFactor = secondary,
                CoreFactorRate = 0.6f,
                SecondaryFactorrate = 0.4f
            };

            _service.ProfileMatching(criteriaDto, carList);
            
           return null;
        }
    }
}