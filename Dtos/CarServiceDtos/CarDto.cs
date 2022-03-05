using System;
using System.ComponentModel.DataAnnotations;

namespace CarSee.Dtos
{
    public class CarDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Brand { get; set; }
        
        [Display(Name="Production Year")]
        public int ProductionYear { get; set; }
        public float Condition { get; set; }
        public string Description { get; set; }
        public int Mileage { get; set; }
        public string ImageFileName { get; set; }
        
        
    }
}