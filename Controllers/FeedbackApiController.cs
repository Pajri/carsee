using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<bool> Post(FeedbackApiDto feedback)
        {
            var dto = feedback as FeedbackDto;
            var result = await _feedbackService.InsertFeedback(dto);

            return result;
        }

        [HttpGet]
        public async Task<List<FeedbackApiDto>> Get()
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

            return feedbackList;
        }
    }
}