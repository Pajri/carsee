using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarSee.Dtos;
using CarSee.Services.CarService;
using CarSee.ViewModels;
using Microsoft.AspNetCore.Http;
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
        public async Task<CarApiListingViewModel> Get(int? page, int? pageSize, string carName)
        {
            var (carList, total) = _carService.GetCar(page, pageSize, carName);
            
            decimal _pageSize =  (pageSize == null) ? 10 : (decimal) pageSize.Value;
            int _page =  (page == null) ? 0 : page.Value;
            decimal _total = (decimal) total;

            var carViewModel = new CarApiListingViewModel
            {
                CarList = carList,
                PageCount = (int) Math.Ceiling(_total/_pageSize),
                CurrentPageIndex = _page,
                SearchParam = carName
            };

            return carViewModel;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<CarApiDto> Post(CarApiDto car)
        {
            CarDto _car = car as CarDto;
            var result = _carService.InsertCar(_car,null);

            var carApiResponseDto = result as CarApiDto;
            return carApiResponseDto;
        }
    }
}