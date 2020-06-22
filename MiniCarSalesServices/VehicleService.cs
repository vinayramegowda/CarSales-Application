using MiniCarsales.Models;
using System.Collections.Generic;
using System.Linq;

namespace MiniCarsales.Services
{
    public class VehicleService : IVehicleService
    {
        readonly List<Vehicle> _vehicles;

        /// <summary>
        /// Constructor.
        /// </summary>
        public VehicleService()
        {
            _vehicles = new List<Vehicle>();
        }

        /// <summary>
        /// Constructor. Mainly used for unit testing.
        /// </summary>
        /// <param name="vehicle">The list of vehicles to initialise this service with.</param>
        public VehicleService(List<Vehicle> vehicle)
        {
            _vehicles = vehicle;
        }

        /// <summary>
        /// If the given vehicle already exists in memory, then it will update it. Otherwise the vehicle will be added as new.
        /// </summary>
        /// <param name="vehicle">The vehicle to add.</param>
        public void SaveOrUpdate(Vehicle vehicle)
        {
            if (vehicle.Id == 0)
            {
                Save(vehicle);
            }
            else
            {
                Update(vehicle);
            }
        }

        /// <summary>
        /// Deletes the vehicle with the given id from memory.
        /// </summary>
        /// <param name="id">The id of the vehicle to delete.</param>
        public void Delete(long id)
        {
            _vehicles.RemoveAll(vehicle => vehicle.Id == id);
        }

        /// <summary>
        /// Gets all vehicles saved in memory.
        /// </summary>
        /// <returns>All vehicles saved in memory.</returns>
        public IList<Vehicle> GetAll(VehicleType type)
        {
            return _vehicles.Where(v => v.VehicleType == type).ToList();
        }

        /// <summary>
        /// Updates the person in memory with the given person.
        /// </summary>
        /// <param name="vehicle">The updated person.</param>
        void Update(Vehicle vehicle)
        {
            var storedVehicle = _vehicles.Single(v => v.Id == vehicle.Id);
            _vehicles.Remove(storedVehicle);
            _vehicles.Add(vehicle);
        }

        /// <summary>
        /// Adds the given person to the in memory collection.
        /// </summary>
        /// <param name="vehicle">The person to add.</param>
        void Save(Vehicle vehicle)
        {
            vehicle.Id = _vehicles.Count == 0 ? 1 : _vehicles.Max(v => v.Id) + 1;
            _vehicles.Add(vehicle);
        }
    }
}
