using System.Collections.Generic;
using CarSee.Dtos;
using CarSee.Extensions.Attributes;
using CarSee.Extensions.Util;
using Microsoft.AspNetCore.Http;

namespace CarSee.ViewModels
{
    public class CarViewModel : CarDto
    {
        [MaxNumberOfFile(5)]
        public List<IFormFile> ImageFile { get; set; }
        public string[] ImageFileNames { get; set; }

        public static CarViewModel CreateFromCarDto(CarDto car)
        {
            var carResponse = new CarViewModel
            {
                Id = car.Id,
                Name = car.Name,
                Price = car.Price,
                Brand = car.Brand,
                ProductionYear = car.ProductionYear,
                Condition = car.Condition,
                Description = car.Description,
                Mileage = car.Mileage,
                ImageFileName = car.ImageFileName,
                ImageFileNames = new string[]{},
                SellerName = car.SellerName,
                SellerPhoneNumber = car.SellerPhoneNumber
            };

            if(!car.ImageFileName.IsNullOrEmpty())
                carResponse.ImageFileNames = car.ImageFileName.Split(';');

            return carResponse;
        }
    }

}