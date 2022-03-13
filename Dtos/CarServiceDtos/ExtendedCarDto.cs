using CarSee.Dtos;
using Microsoft.AspNetCore.Http;

namespace CarSee.Dtos
{
    public class ExtendedCarDto : CarDto
    {
        public IFormFile ImageFile { get; set; }
        public int PageCount { get; set; }
        public int CurrentPageIndex { get; set; }

        public static ExtendedCarDto CreateFromCarDto(CarDto car)
        {
            var carResponse = new ExtendedCarDto
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

            return carResponse;
        }
    }
}