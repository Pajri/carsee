using System;
using System.Collections.Generic;
using CarSee.Dtos;
using CarSee.Constants;
using CarSee.Entities;
using CarSee.EntityFramework;
using System.Linq;

namespace CarSee.Services.CarService
{
    public class CarService : ICarService
    {
        private readonly ApplicationDbContext _ctx;
        public CarService(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public (List<CarDto>, int) GetCar(int? pageParam, int? pageSizeParam, string carName = null)
        {
            int page = (pageParam == null) ? 0 : pageParam.Value-1;
            int pageSize = (pageSizeParam == null) ? 10 : pageSizeParam.Value;
            int total = 0;

            var carQuery = _ctx.Car.AsQueryable();
            if(carName != null) carQuery =  carQuery.Where(n => n.Name.ToLower().Contains(carName.ToLower()));
            carQuery = carQuery.OrderByDescending(d => d.CreatedDate);
            total = carQuery.Count();
            
            carQuery = carQuery.Skip(pageSize*page).Take(pageSize);

            var carListResponse = carQuery.ToList();
            var carList = new List<CarDto>();
            foreach (var item in carListResponse)
            {
                var carItem = new CarDto()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price,
                    Brand = item.Brand,
                    ProductionYear = item.ProductionYear,
                    Condition = item.Condition,
                    Description = item.Description,
                    Mileage = item.Mileage,
                    ImageFileName = item.ImageFileName
                };

                carList.Add(carItem);
            }

            return (carList, total);
        }


        public CarDto GetDetailCar(Guid carId)
        {
            var car = _ctx.Car
                .FirstOrDefault(m => m.Id == carId);

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
                ImageFileName = car.ImageFileName
            };

            return carDto;
        }


        public CarDto InsertCar(CarDto car, Guid? id)
        {
            try
            {
                if (id == null) car.Id = Guid.NewGuid();
                
                Car carToStore = new Car()
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
                    CreatedDate = DateTime.Now
                };

                _ctx.Add(carToStore);
                _ctx.SaveChanges();

                var carResult = new CarDto
                {
                    Id = carToStore.Id,
                    Name = carToStore.Name,
                    Price = carToStore.Price,
                    Brand = carToStore.Brand,
                    ProductionYear = carToStore.ProductionYear,
                    Condition = carToStore.Condition,
                    Description = carToStore.Description,
                    Mileage = carToStore.Mileage,
                    ImageFileName = car.ImageFileName
                };

                return carResult;
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }
        
        public CarDto EditCar(CarDto car)
        {
            try
            {
                var storedCar = _ctx.Car.Find(car.Id);
                if (storedCar == null) throw new Exception(Common.Error.ERROR_NOT_FOUND);

                storedCar.Id = car.Id;
                storedCar.Name = car.Name;
                storedCar.Price = car.Price;
                storedCar.Brand = car.Brand;
                storedCar.ProductionYear = car.ProductionYear;
                storedCar.Condition = car.Condition;
                storedCar.Description = car.Description;
                storedCar.Mileage = car.Mileage;

                _ctx.Update(storedCar);
                _ctx.SaveChanges();

                var updatedCar = new CarDto
                {
                    Id = storedCar.Id,
                    Price = storedCar.Price,
                    Brand = storedCar.Brand,
                    ProductionYear = storedCar.ProductionYear,
                    Condition = storedCar.Condition,
                    Description = storedCar.Description,
                    Mileage = storedCar.Mileage,
                    ImageFileName = storedCar.ImageFileName
                };

                return updatedCar;
            }
            catch (System.Exception ex)
            {
                throw;
            }

        }

        public bool IsCarExists(Guid id)
        {
            var car = GetDetailCar(id);
            if (car == null) return false;
            return true;
        }
    
        public CarDto DeleteCar(Guid id)
        {
            var storedCar = _ctx.Car.Find(id);
            _ctx.Car.Remove(storedCar);
            _ctx.SaveChanges();

            var car = new CarDto
            {
                Id = storedCar.Id,
                Name = storedCar.Name,
                Price = storedCar.Price,
                Brand = storedCar.Brand,
                ProductionYear = storedCar.ProductionYear,
                Condition = storedCar.Condition,
                Description = storedCar.Description,
                Mileage = storedCar.Mileage,
                ImageFileName = storedCar.ImageFileName
            };

            return car;
        }
    }
}