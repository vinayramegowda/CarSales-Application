using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniCarsales.Models;
using MiniCarsales.Services;

namespace MiniCarsales.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        readonly IVehicleService _vehicleService;
        readonly ILogger<CarsController> _logger;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="vehicleService">The vehicle service.</param>
        /// <param name="logger">The logger for this controller.</param>
        public CarsController(IVehicleService vehicleService, ILogger<CarsController> logger)
        {
            _vehicleService = vehicleService;
            _logger = logger;
        }

        /// <summary>
        /// Gets all cars stored in memory.
        /// </summary>
        /// <returns>All cars stored in memory.</returns>
        [HttpGet]
        public IEnumerable<Car> Get()
        {
            var cars = _vehicleService.GetAll(VehicleType.Car);

            _logger.LogInformation($"Retrieving all cars (count: {cars.Count})");

            return cars.Select(vehicle => vehicle as Car);
        }

        /// <summary>
        /// Saves the given car to the vehicles list in memory.
        /// </summary>
        /// <param name="car">The car to be saved, sent by the client.</param>
        /// <returns>BadRequest if the car sent by the client is not valid, otherwise Ok.</returns>
        [HttpPost]
        public IActionResult Post([FromBody] Car car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _logger.LogInformation($"Saving vehicle {car}");
            _vehicleService.SaveOrUpdate(car);

            return Ok(car);
        }

        /// <summary>
        /// Deletes the car with the given id from memory.
        /// </summary>
        /// <param name="id">The id of the car to delete.</param>
        /// <returns>HTTP 200 code.</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation($"Deleting vehicle with id {id}");
            _vehicleService.Delete(id);

            return Ok();
        }
    }
}
