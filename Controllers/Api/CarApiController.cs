using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CarSee.Constants;
using CarSee.Dtos;
using CarSee.Services.CarService;
using CarSee.Utility.StorageProvider;
using CarSee.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CarSee.Controllers.Api
{
    [Route("api/car")]
    [ApiController]
    public class CarApiController : ControllerBase
    {   
        private readonly ICarService _carService;
        private readonly IStorageProvider _storageProvider;
        public CarApiController(ICarService carService, IStorageProvider storageProvider)
        {
            _carService = carService;
            _storageProvider = storageProvider;
        }
        
        [HttpGet]
        public async Task<CommonApiResponseDto> Get(int? page, int? pageSize, string carName)
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

            CommonApiResponseDto response = new CommonApiResponseDto
            {
                Status = ResponseStatus.RESPONSE_SUCCESS,
                Data = carViewModel
            };

            return response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<CommonApiResponseDto> Post(CarApiDto car)
        {
            CarDto _car = car as CarDto;
            List<string> imageFileNameList = new List<string>();

            foreach (var b64 in car.ImageBase64)
            {
                var providerResponse = await _storageProvider.Save(b64);
                var fileName = providerResponse.FileName;
                imageFileNameList.Add(fileName);
            }

            if(imageFileNameList.Count() > 0)
            {
                _car.ImageFileName = JsonConvert.SerializeObject(imageFileNameList);
            }

            var result = _carService.InsertCar(_car,null);

            var carApiResponseDto = result as CarApiDto;

            CommonApiResponseDto response = new CommonApiResponseDto
            {
                Status = ResponseStatus.RESPONSE_SUCCESS,
                Data = carApiResponseDto
            };
            return response;
        }

        [HttpPost]
        [Route("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<CommonApiResponseDto> Post(string id)
        {
            var result = _carService.DeleteCar(id);

            var carApiResponseDto = result as CarApiDto;

            CommonApiResponseDto response = new CommonApiResponseDto
            {
                Status = ResponseStatus.RESPONSE_SUCCESS,
                Data = result
            };
            return response;
        }
    }
}