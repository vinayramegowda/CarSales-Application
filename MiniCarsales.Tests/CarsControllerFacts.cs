using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniCarsales.Controllers;
using MiniCarsales.Models;
using MiniCarsales.Services;
using NSubstitute;
using Xunit;

namespace MiniCarsales.Tests
{
    public class CarsControllerFacts
    {

        [Fact]
        public void Gets_AllCars_Success()
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

            var mockVehicleService = Substitute.For<IVehicleService>();
            mockVehicleService.GetAll(VehicleType.Car).Returns(vehicles);

            var mockLogger = Substitute.For<ILogger<CarsController>>();

            var sut = new CarsController(mockVehicleService, mockLogger);

            // act
            var results = sut.Get().ToList();

            // assert
            Assert.NotNull(results);
            Assert.Equal(2, results.Count);
            Assert.Equal(1, results[0].Id);
            Assert.Equal(2, results[1].Id);
            Assert.Equal("Holden", results[0].Make);
            Assert.Equal("Subaru", results[1].Make);
        }

        [Fact]
        public void Posts_NewCar_IsValid_Success()
        {
            // arrange
            var car = new Car
            {
                Id = 1,
                Make = "Holden",
                Model = "Commodore",
                VehicleType = VehicleType.Car,
                CarBodyType = CarBodyType.Sedan,
                Engine = "V6",
                NumberOfDoors = 4,
                NumberOfWheels = 3
            };

            var mockVehicleService = Substitute.For<IVehicleService>();
            var mockLogger = Substitute.For<ILogger<CarsController>>();

            var sut = new CarsController(mockVehicleService, mockLogger);

            // act
            var result = sut.Post(car);
            var okResult = result as OkObjectResult;

            // assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void Posts_NewCar_MissingRequiredPropertyNumberOfDoors_ExpectsBadRequest()
        {
            // arrange
            var car = new Car
            {
                Id = 1,
                Make = "Holden",
                Model = "Commodore",
                VehicleType = VehicleType.Car,
                CarBodyType = CarBodyType.Sedan,
                Engine = "V6",
                NumberOfWheels = 3
            };

            var mockVehicleService = Substitute.For<IVehicleService>();
            var mockLogger = Substitute.For<ILogger<CarsController>>();

            var sut = new CarsController(mockVehicleService, mockLogger);
            sut.ModelState.AddModelError("NumberOfDoors", "Required");

            // act
            var result = sut.Post(car);
            var badRequestResult = result as BadRequestObjectResult;

            // assert
            Assert.NotNull(badRequestResult);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public void Deletes_ExistingCar_Success()
        {
            // arrange
            var mockVehicleService = Substitute.For<IVehicleService>();
            var mockLogger = Substitute.For<ILogger<CarsController>>();

            var sut = new CarsController(mockVehicleService, mockLogger);

            // act
            var result = sut.Delete(1);
            var okResult = result as OkResult;

            // assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

    }
}