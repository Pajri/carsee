using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarSee.Constants;
using CarSee.Dtos;
using CarSee.Services.CarService;
using CarSee.Services.FeedbackService;
using CarSee.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarSee.Controllers.Api
{
    [Route("api/feedback")]
    [ApiController]
    public class FeedbackApiController : ControllerBase
    {   
        private readonly IFeedbackService _feedbackService;
        public FeedbackApiController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<CommonApiResponseDto> Post(FeedbackApiDto feedback)
        {
            var dto = feedback as FeedbackDto;
            var result = await _feedbackService.InsertFeedback(dto);

            CommonApiResponseDto response = new CommonApiResponseDto
            {
                Status = ResponseStatus.RESPONSE_SUCCESS,
                Data = result
            };

            return response;
        }

        [HttpGet]
        public async Task<CommonApiResponseDto> Get()
        {
            var result = await _feedbackService.GetAllFeedback();

            List<FeedbackApiDto> feedbackList = new List<FeedbackApiDto>();
            foreach (var feedback in result)
            {
                feedbackList.Add(new FeedbackApiDto
                {
                    Id = feedback.Id,
                    Comment = feedback.Comment,
                    Rating = feedback.Rating
                });
            }

            CommonApiResponseDto response = new CommonApiResponseDto
            {
                Status = ResponseStatus.RESPONSE_SUCCESS,
                Data = feedbackList
            };

            return response;
        }
    }
}