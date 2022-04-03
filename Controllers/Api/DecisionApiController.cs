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
        public async Task<List<CarDto>> ProfileMatching(DecisionRequestDto dto, List<CarDto> carList)
        {
            var criteriaDto = _service.CreateCriteriaDto(dto);
            var carDecisionList = _service.CreateCarDecisionDto(carList);

            var result = _service.ProfileMatching(criteriaDto, carDecisionList);
 
            return await Task.FromResult<List<CarDto>>(result);
        }
    }
}