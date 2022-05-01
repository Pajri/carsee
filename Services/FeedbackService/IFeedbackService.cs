using System;
using System.Threading.Tasks;
using CarSee.Dtos;
using CarSee.Entities;
using CarSee.EntityFramework;

namespace CarSee.Services.FeedbackService
{
    public interface IFeedbackService
    {       
        Task<bool> InsertFeedback(FeedbackDto dto);
    }
}