using System;
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
    }
}