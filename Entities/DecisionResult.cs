using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CarSee.Entities
{
    public class DecisionResult
    {
        public Guid Id { get; set; }
        [Column(TypeName = "jsonb")]
        public string Result { get; set; }

    }
}