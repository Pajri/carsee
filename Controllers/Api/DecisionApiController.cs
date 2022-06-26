using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarSee.Constants;
using CarSee.Dtos;
using CarSee.Dtos.Decision;
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
        public async Task<CommonApiResponseDto> ProfileMatching(DecisionRequestDto dto)
        {
            var criteriaDto = _service.CreateCriteriaDto(dto);
            var carDecisionList = _service.CreateCarDecisionDto(dto.UUID, dto.Weight);

            var result = _service.ProfileMatching(criteriaDto, carDecisionList);
            
            var decisionResultDto = new DecisionResultDto
            {
                Id = Guid.NewGuid(),
                Result = JsonConvert.SerializeObject(result)
            };

            _service.SaveResult(decisionResultDto);

            CommonApiResponseDto response = new CommonApiResponseDto
            {
                Status = ResponseStatus.RESPONSE_SUCCESS,
                Data = result
            };

            return await Task.FromResult<CommonApiResponseDto>(response);
        }

        [HttpGet]
        public async Task<CommonApiResponseDto> GetDecisionById([FromQuery] Guid id)
        {
            var result = _service.GetResult(id);

            CommonApiResponseDto response = new CommonApiResponseDto
            {
                Status = ResponseStatus.RESPONSE_SUCCESS,
                Data = result
            };

            return await Task.FromResult<CommonApiResponseDto>(response);
        }  
    }
}