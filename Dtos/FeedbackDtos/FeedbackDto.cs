using System;

namespace CarSee.Dtos
{
    public class FeedbackDto
    {
        public Guid Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}