using System;
using System.Collections.Generic;
using CarSee.Dtos;

namespace CarSee.Services.CarService
{
    public interface ICarService
    {
        public List<CarDto> GetAllCar();
        public CarDto GetDetailCar(Guid carId);
        public CarDto InsertCar(CarDto car, Guid? id);
        public CarDto EditCar(CarDto car);
        public bool IsCarExists(Guid id);
        public CarDto DeleteCar(Guid id);
    }
}