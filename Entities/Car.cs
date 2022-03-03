using System;

namespace CarSee.Entities
{
    public class Car
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Brand { get; set; }
        public int ProductionYear { get; set; }
        public float Condition { get; set; }
        public string Description { get; set; }
        public int Mileage { get; set; }        
    }
}