using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

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
        public string ImageFileName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserId { get; set; }
        public string Uid { get; set; }

        public string SellerName { get; set; }
        public string SellerPhoneNumber { get; set; }

    }
}