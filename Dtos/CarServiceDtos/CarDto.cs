using System;
using System.ComponentModel.DataAnnotations;
using CarSee.Extensions.Attributes;
using Newtonsoft.Json.Linq;

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

        [MaxNumberOfFileName(5)]
        public string ImageFileName { get; set; }
        public string[] ImageFileNameArr { get; set; }

        public string UserId { get; set; }

        [Display(Name="Seller Name")]
        public string SellerName { get; set; }

        [RegularExpression(@"(\+62 ((\d{3}([ -]\d{3,})([- ]\d{4,})?)|(\d+)))|(\(\d+\) \d+)|\d{3}( \d+)+|(\d+[ -]\d+)|\d+",
                            ErrorMessage = "Invalid phone number format")]
        
        [Display(Name="Seller Phone Number")]
        public string SellerPhoneNumber { get; set; }
        public string UUID { get; set; }
    }
}