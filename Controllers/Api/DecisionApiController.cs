using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarSee.Dtos;
using CarSee.Services.CarService;
using CarSee.Services.DecisionService;
using CarSee.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
            
            var decisionResultDto = new DecisionResultDto
            {
                Id = Guid.NewGuid(),
                Result = JsonConvert.SerializeObject(result)
            };

            _service.SaveResult(decisionResultDto);

            return await Task.FromResult<List<CarDecisionDto>>(result);
        }

        [HttpGet]
        public async Task<List<CarDecisionDto>> GetDecisionById([FromQuery] Guid id)
        {
            var result = _service.GetResult(id);

            return await Task.FromResult<List<CarDecisionDto>>(result);
        }

        
    }
}