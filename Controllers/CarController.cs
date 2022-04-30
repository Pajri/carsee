using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarSee.Entities;
using CarSee.EntityFramework;
using CarSee.Services.CarService;
using CarSee.Dtos;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Extensions.Options;
using CarSee.Utility.Settings;
using CarSee.ViewModels;
using CarSee.Utility.StorageProvider;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Authorization;

namespace CarSee.Controllers
{
    [Authorize(Roles = Constants.Roles.ROLE_ADMIN)]
    public class CarController : Controller
    {
        private readonly ICarService _carService;
        private readonly IStorageProvider _storageProvider;
        private readonly StorageProviderConfig _options;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CarController(
            ICarService carService, 
            IStorageProvider storageProvider,
            IOptions<StorageProviderConfig> options,
            IHttpContextAccessor httpContextAccessor)
        {
            _carService = carService;
            _storageProvider = storageProvider;
            _options = options.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: Car
        [HttpGet]
        public async Task<IActionResult> Index(int? page, int? pageSize, string carName)
        {
            var (storedCarList, total) = _carService.GetCar(page, pageSize, carName);
            var carResponseList = new List<ExtendedCarDto>();

            foreach (var car in storedCarList)
            {
                var carViewModel = ExtendedCarDto.CreateFromCarDto(car);
                carResponseList.Add(carViewModel);
            }

            decimal _pageSize =  (pageSize == null) ? 10 : (decimal) pageSize.Value;
            int _page =  (page == null) ? 0 : page.Value;
            decimal _total = (decimal) total;

            var CarViewModel = new CarListingViewModel
            {
                CarList = carResponseList,
                PageCount = (int) Math.Ceiling(_total/_pageSize),
                CurrentPageIndex = _page,
                SearchParam = carName
            };

            return View(CarViewModel);
        }

        // GET: Car/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
           
            var car = _carService.GetDetailCar((Guid) id);
            if (car == null) return NotFound();

            var carViewModel = ExtendedCarDto.CreateFromCarDto(car); 
            return View(carViewModel);
        }

        // GET: Car/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Car/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]  
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Name,Price,Brand,ProductionYear,Condition,Description,Mileage,ImageFile")] CarViewModel car
        )
        {   
            var userId = _httpContextAccessor.HttpContext.User.Identity.GetUserId();
            var carDto = new CarDto()
            {
                Id = car.Id,
                Name = car.Name,
                Price = car.Price,
                Brand = car.Brand,
                ProductionYear = car.ProductionYear,
                Condition = car.Condition,
                Description = car.Description,
                Mileage = car.Mileage,
                UserId = userId
            };

            if (ModelState.IsValid)
            {
                if(car.ImageFile != null)
                {
                    var storedImage = await _storageProvider.Save(car.ImageFile, true);
                    var fileNameArr = storedImage.Select(si => si.FileName).ToArray();
                    var fileNameString = String.Join(";",fileNameArr);

                    carDto.ImageFileName= fileNameString;
                }
                
                car.Id = Guid.NewGuid();
                var carResult = _carService.InsertCar(carDto, null);
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: Car/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var car = _carService.GetDetailCar((Guid) id);
            if (car == null) return NotFound();

            var carViewModel = CarViewModel.CreateFromCarDto(car);
            return View(carViewModel);
        }

        // POST: Car/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Price,Brand,ProductionYear,Condition,Description,Mileage,ImageFile")] CarViewModel car)
        {
            if (id != car.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    if(car.ImageFile != null)
                    {
                        var storedImage = await _storageProvider.Save(car.ImageFile.FirstOrDefault(), true);
                        car.ImageFileName= storedImage.FileName;
                    }
                    _carService.EditCar(car);
                }
                catch (DbUpdateConcurrencyException)
                {
                    var carExists = _carService.IsCarExists(car.Id);
                    if (!carExists) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: Car/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)  return NotFound();

            var car = _carService.GetDetailCar((Guid) id);
            if (car == null)  return NotFound();
            
            var carViewModel = CarViewModel.CreateFromCarDto(car);
            return View(carViewModel);
        }

        // POST: Car/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var car = _carService.DeleteCar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
