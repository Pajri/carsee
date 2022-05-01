using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarSee.Dtos;
using CarSee.Entities;
using CarSee.EntityFramework;

namespace CarSee.Services.FeedbackService
{
    public class FeedbackService : IFeedbackService
    {
        private readonly ApplicationDbContext _ctx;
        public FeedbackService(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<bool> InsertFeedback(FeedbackDto dto)
        {
            try
            {
                var feedbackEntity = new Feedback
                {
                    Id = Guid.NewGuid(),
                    Comment = dto.Comment,
                    Rating = dto.Rating
                };

                _ctx.Feedback.Add(feedbackEntity);
                _ctx.SaveChanges();

                return await Task.FromResult<bool>(true);
            }
            catch (System.Exception)
            {

                throw;
            }

        }

        public async Task<List<FeedbackDto>> GetAllFeedback()
        {
            List<FeedbackDto> feedbacklist = new List<FeedbackDto>();
            foreach (var feedback in _ctx.Feedback)
            {
                feedbacklist.Add(new FeedbackDto
                {
                    Id = feedback.Id,
                    Comment = feedback.Comment,
                    Rating = feedback.Rating
                });
            };

            return await Task.FromResult<List<FeedbackDto>>(feedbacklist);

        }
    }
}