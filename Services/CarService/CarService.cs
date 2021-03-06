using System;
using System.Collections.Generic;
using CarSee.Dtos;
using CarSee.Constants;
using CarSee.Entities;
using CarSee.EntityFramework;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace CarSee.Services.CarService
{
    public class CarService : ICarService
    {
        private readonly ApplicationDbContext _ctx;
        private readonly ApplicationIdentityDbContext _identityCtx;
        public CarService(ApplicationDbContext ctx, ApplicationIdentityDbContext identityCtx)
        {
            _ctx = ctx;
            _identityCtx = identityCtx;
        }

        public List<CarDto> GetDecisionCarList(string UUID)
        {
            var carListData = _ctx.Car.Where(c => c.UUID == UUID);

            List<CarDto> carList = new List<CarDto>();
            foreach (var item in carListData)
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
                    ImageFileName = item.ImageFileName,
                    SellerName = item.SellerName,
                    SellerPhoneNumber = item.SellerPhoneNumber,
                    UUID = item.UUID
                };

                bool isJsonValid = true;
                try
                {
                    carItem.ImageFileNameArr = JsonConvert.DeserializeObject<string[]>(carItem.ImageFileName);
                }
                catch (Exception)
                {
                    isJsonValid = false;
                }

                carList.Add(carItem);
            }

            return carList;
        }

        public (List<CarDto>, int) GetCar(int? pageParam, int? pageSizeParam, string carName = null)
        {
            int page = (pageParam == null) ? 0 : pageParam.Value-1;
            int pageSize = (pageSizeParam == null) ? 10 : pageSizeParam.Value;
            int total = 0;

            var carQuery = _ctx.Car.AsQueryable();
            if (carName != null) carQuery = carQuery.Include(u => u.User).Where(n => n.Name.ToLower().Contains(carName.ToLower()));
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
                    ImageFileName = item.ImageFileName,
                    SellerName = item.SellerName,
                    SellerPhoneNumber = item.SellerPhoneNumber,
                    UUID = item.UUID
                };

                bool isJsonValid = true;
                try
                {
                    carItem.ImageFileNameArr = JsonConvert.DeserializeObject<string[]>(carItem.ImageFileName);
                }
                catch (Exception)
                {
                    isJsonValid = false;
                }

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
                ImageFileName = car.ImageFileName,
                SellerName = car.SellerName,
                SellerPhoneNumber = car.SellerPhoneNumber,
                UUID = car.UUID
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
                    CreatedDate = DateTime.Now,
                    UserId = car.UserId,
                    SellerName = car.SellerName,
                    SellerPhoneNumber = car.SellerPhoneNumber,
                    UUID = car.UUID
                };

                //if (car.UserId != null && car.UserId != "")
                //{
                //    var user = _identityCtx.Users.Where(u => u.Id == car.UserId).SingleOrDefault();
                //    carToStore.User = user;
                //}

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
                    ImageFileName = car.ImageFileName,
                    UserId = car.UserId
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
                storedCar.SellerName = car.SellerName;
                storedCar.SellerPhoneNumber =  car.SellerPhoneNumber;

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
                    ImageFileName = storedCar.ImageFileName,
                    SellerName = storedCar.SellerName,
                    SellerPhoneNumber = storedCar.SellerPhoneNumber,
                    UUID = storedCar.UUID
                };

                return updatedCar;
            }
            catch (System.Exception ex)
            {
                throw;
            }

        }

        public CarDto DeleteCar(string Id)
        {
            if (Id == null || Id == "") return null;

            try
            {
                var storedCar = _ctx.Car.Find(new Guid(Id));
                _ctx.Car.Remove(storedCar);
                if (storedCar == null) throw new Exception(Common.Error.ERROR_NOT_FOUND);

                _ctx.SaveChanges();

                var deleted = new CarDto
                {
                    Id = storedCar.Id,
                    Price = storedCar.Price,
                    Brand = storedCar.Brand,
                    ProductionYear = storedCar.ProductionYear,
                    Condition = storedCar.Condition,
                    Description = storedCar.Description,
                    Mileage = storedCar.Mileage,
                    ImageFileName = storedCar.ImageFileName,
                    SellerName = storedCar.SellerName,
                    SellerPhoneNumber = storedCar.SellerPhoneNumber,
                    UUID = storedCar.UUID
                };

                return deleted;
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
                ImageFileName = storedCar.ImageFileName,
                SellerName = storedCar.SellerName,
                SellerPhoneNumber = storedCar.SellerPhoneNumber,
                UUID = storedCar.UUID
            };

            return car;
        }

        public void Favoritkan(Guid carId, string UUID)
        {
            var car = _ctx.Car.Where(c => c.Id == carId).FirstOrDefault();
            car.UUID = UUID;
            _ctx.Update(car);
            _ctx.SaveChanges();
        }
    }
}