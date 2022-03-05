using CarSee.Dtos;
using Microsoft.AspNetCore.Http;

namespace CarSee.ViewModels
{
    public class CarViewModel : CarDto
    {
        public IFormFile ImageFile { get; set; }

        public static CarViewModel CreateFromCarDto(CarDto car)
        {
            var carViewModel = new CarViewModel
            {
                Id = car.Id,
                Name = car.Name,
                Price = car.Price,
                Brand = car.Brand,
                ProductionYear = car.ProductionYear,
                Condition = car.Condition,
                Description = car.Description,
                Mileage = car.Mileage,
                ImageFileName = car.ImageFileName
            };

            return carViewModel;
        }
    }
    
}