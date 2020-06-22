using MiniCarsales.Models;
using System.Collections.Generic;

namespace MiniCarsales.Services
{
    public interface IVehicleService
    {
        /// <summary>
        /// If the given vehicle already exists in memory, then it will update it. Otherwise the vehicle will be added as new.
        /// </summary>
        /// <param name="vehicle">The vehicle to add.</param>
        void SaveOrUpdate(Vehicle vehicle);

        /// <summary>
        /// Deletes the vehicle with the given id from memory.
        /// </summary>
        /// <param name="id">The id of the vehicle to delete.</param>
        void Delete(long id);

        /// <summary>
        /// Gets all vehicles saved in memory for the given vehicle type.
        /// </summary>
        /// <param name="type">The type of vehicle to retrieve.</param>
        /// <returns>All vehicles saved in memory that are of the required type.</returns>
        IList<Vehicle> GetAll(VehicleType type);
    }
}
