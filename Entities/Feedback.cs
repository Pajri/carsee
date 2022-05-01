using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CarSee.Entities
{
    public class Feedback
    {
        public Guid Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }

    }
}