using System.Collections.Generic;
using System.Threading.Tasks;
using CarSee.Dtos;
using CarSee.Services.CarService;
using Microsoft.AspNetCore.Mvc;

namespace CarSee.Controllers.Api
{
    [Route("api/car")]
    [ApiController]
    public class CarApiController : ControllerBase
    {   
        private readonly ICarService _carService;
        public CarApiController(ICarService carService)
        {
            _carService = carService;
        }
        
        [HttpGet]
        public async Task<List<CarDto>> Get()
        {
            var carList = _carService.GetAllCar();
            return carList;
        }
    }
}