using System.Collections.Generic;
using System.Linq;
using MiniCarsales.Models;
using MiniCarsales.Services;
using Xunit;

namespace MiniCarsales.Tests
{
    public class VehicleServiceFacts
    {
        [Fact]
        public void Save_NewCar_VehicleSavedToDatabase_Success()
        {
            // arrange
            var car = new Car
            {
                Make = "Subaru",
                Model = "WRX",
                VehicleType = VehicleType.Car,
                CarBodyType = CarBodyType.Sedan,
                Engine = "4 Cylinder Boxer",
                NumberOfDoors = 4,
                NumberOfWheels = 4
            };

            var sut = GetSut(new List<Vehicle>());

            // act
            sut.SaveOrUpdate(car);

            // assert
            var cars = sut.GetAll(VehicleType.Car);

            Assert.NotNull(cars);
            Assert.Single(cars);
            Assert.Equal(car, cars[0]);
        }

        [Fact]
        public void Save_UpdatedCar_CarInDatabaseUpdated_Success()
        {
            // arrange
            var car = new Car
            {
                Id = 1,
                Make = "Subaru",
                Model = "WRX",
                VehicleType = VehicleType.Car,
                CarBodyType = CarBodyType.Sedan,
                Engine = "4 Cylinder Boxer",
                NumberOfDoors = 4,
                NumberOfWheels = 4
            };

            var sut = GetSut(new List<Vehicle>{car});
            
            var updatedCar = new Car
            {
                Id = 1,
                Make = "Subaru",
                Model = "WRX",
                VehicleType = VehicleType.Car,
                CarBodyType = CarBodyType.Hatchback,
                Engine = "4 Cylinder Boxer",
                NumberOfDoors = 5,
                NumberOfWheels = 4
            };

            // act
            sut.SaveOrUpdate(updatedCar);

            // assert
            var cars = sut.GetAll(VehicleType.Car);

            Assert.NotNull(cars);
            Assert.Equal(1, cars.Count);
            Assert.True(((Car)cars[0]).CarBodyType == CarBodyType.Hatchback);
        }

        [Fact]
        public void Delete_CarExists_DeleteSuccess()
        {
            // arrange
            var vehicles = new List<Vehicle>
            {
                new Car
                {
                    Id = 1,
                    Make = "Holden",
                    Model = "Commodore",
                    VehicleType = VehicleType.Car,
                    CarBodyType = CarBodyType.Sedan,
                    Engine = "V6",
                    NumberOfDoors = 4,
                    NumberOfWheels = 3
                },
                new Car
                {
                    Id = 2,
                    Make = "Subaru",
                    Model = "WRX",
                    VehicleType = VehicleType.Car,
                    CarBodyType = CarBodyType.Sedan,
                    Engine = "4 Cylinder Boxer",
                    NumberOfDoors = 4,
                    NumberOfWheels = 4
                }
            };

            var sut = GetSut(vehicles);

            // act
            sut.Delete(1);

            // assert
            var results = sut.GetAll(VehicleType.Car);

            Assert.NotNull(results);
            Assert.Single(results);
            Assert.True(results[0].Id == 2);
        }

        [Fact]
        public void GetAll_AllNonDeletedVehicles_Success()
        {
            // arrange
            var vehicles = new List<Vehicle>
            {
                new Car
                {
                    Id = 1,
                    Make = "Holden",
                    Model = "Commodore",
                    VehicleType = VehicleType.Car,
                    CarBodyType = CarBodyType.Sedan,
                    Engine = "V6",
                    NumberOfDoors = 4,
                    NumberOfWheels = 3
                },
                new Car
                {
                    Id = 2,
                    Make = "Subaru",
                    Model = "WRX",
                    VehicleType = VehicleType.Car,
                    CarBodyType = CarBodyType.Sedan,
                    Engine = "4 Cylinder Boxer",
                    NumberOfDoors = 4,
                    NumberOfWheels = 4
                }
            };

            var sut = GetSut(vehicles);

            // act
            var results = sut.GetAll(VehicleType.Car);

            // assert
            Assert.NotNull(results);
            Assert.Equal(2, results.Count);
            Assert.True(results.Single(p => p.Id == 1) is Car);
            Assert.True(results.Single(p => p.Id == 2) is Car);
        }

        static IVehicleService GetSut(List<Vehicle> vehicles)
        {
            return new VehicleService(vehicles);
        }
    }
}
