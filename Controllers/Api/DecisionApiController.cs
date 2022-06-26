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
        private readonly IDecisionService _decisionService;
        private readonly ICarService _carService;
        public DecisionApiController(IDecisionService service, ICarService carService)
        {
            _decisionService = service;
            _carService = carService;
        }
        
        [HttpPost]
        [Route("favoritkan")]
        public async Task<CommonApiResponseDto> Favoritkan(DecisionRequestDto dto)
        {
            Guid id = Guid.Parse(dto.CarId);
            _carService.Favoritkan(id, dto.UUID);

            CommonApiResponseDto response = new CommonApiResponseDto
            {
                Status = ResponseStatus.RESPONSE_SUCCESS,
            };

            return await Task.FromResult<CommonApiResponseDto>(response);
        }
        
        [HttpPost]
        public async Task<CommonApiResponseDto> ProfileMatching(DecisionRequestDto dto)
        {
            var criteriaDto = _decisionService.CreateCriteriaDto(dto);
            var carDecisionList = _decisionService.CreateCarDecisionDto(dto.UUID, dto.Weight);

            var result = _decisionService.ProfileMatching(criteriaDto, carDecisionList);
            
            var decisionResultDto = new DecisionResultDto
            {
                Id = Guid.NewGuid(),
                Result = JsonConvert.SerializeObject(result)
            };

            _decisionService.SaveResult(decisionResultDto);

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
            var result = _decisionService.GetResult(id);

            CommonApiResponseDto response = new CommonApiResponseDto
            {
                Status = ResponseStatus.RESPONSE_SUCCESS,
                Data = result
            };

            return await Task.FromResult<CommonApiResponseDto>(response);
        }  
    }
}