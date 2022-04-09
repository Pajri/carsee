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
        
        [HttpPost]
        public async Task<List<CarDecisionDto>> ProfileMatching(DecisionRequestDto dto)
        {
            var criteriaDto = _service.CreateCriteriaDto(dto);
            var carDecisionList = _service.CreateCarDecisionDto(dto.CarList);

            var result = _service.ProfileMatching(criteriaDto, carDecisionList);
 
            return await Task.FromResult<List<CarDecisionDto>>(result);
        }
    }
}