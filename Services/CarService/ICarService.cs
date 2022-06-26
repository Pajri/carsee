using System;
using System.Collections.Generic;
using CarSee.Dtos;

namespace CarSee.Services.CarService
{
    public interface ICarService
    {
        public (List<CarDto>, int) GetCar(int? page, int? pageSize, string carName = null);
        public CarDto GetDetailCar(Guid carId);
        public CarDto InsertCar(CarDto car, Guid? id);
        public CarDto EditCar(CarDto car);
        public bool IsCarExists(Guid id);
        public CarDto DeleteCar(Guid id);
        public CarDto DeleteCar(string Id);
        public void Favoritkan(Guid carId, string UUID);
        public List<CarDto> GetDecisionCarList(string UUID);
    }
}