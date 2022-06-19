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
            float condition = float.Parse(dto.CriteriaArr[DecisionRequestDto.IDX_CONDITION]);
            long mileage = long.Parse(dto.CriteriaArr[DecisionRequestDto.IDX_MILEAGE]);
            double price = double.Parse(dto.CriteriaArr[DecisionRequestDto.IDX_PRICE]);
            int yearmade = int.Parse(dto.CriteriaArr[DecisionRequestDto.IDX_YEARMADE]);

            int conditionWeight = int.Parse(dto.WeightArr[DecisionRequestDto.IDX_CONDITION]);
            int mileageWeight = int.Parse(dto.WeightArr[DecisionRequestDto.IDX_MILEAGE]);
            int priceWeight = int.Parse(dto.WeightArr[DecisionRequestDto.IDX_PRICE]);
            int yearmadeWeight = int.Parse(dto.WeightArr[DecisionRequestDto.IDX_YEARMADE]);
            int brandWeight = int.Parse(dto.WeightArr[DecisionRequestDto.IDX_BRAND]);

            dto.Criteria = new CriteriaRequestDto
            {
                Brand = dto.CriteriaArr[DecisionRequestDto.IDX_BRAND],
                Condition = condition,
                Mileage = mileage,
                Price = price,
                YearMade = yearmade
            };

            dto.Weight = new WeightRequestDto
            {
                Condition = conditionWeight,
                Brand = brandWeight,
                Mileage = mileageWeight,
                Price = priceWeight,
                YearMade = yearmadeWeight
            };

            var criteriaDto = _service.CreateCriteriaDto(dto);
            var carDecisionList = _service.CreateCarDecisionDto(dto.CarList);

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