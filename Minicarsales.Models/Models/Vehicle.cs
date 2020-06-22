using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MiniCarsales.Models
{
    public class Vehicle
    {
        /// <summary>
        /// The id of the vehicle
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Type of vehicle.
        /// </summary>
        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        public VehicleType VehicleType { get; set; }

        /// <summary>
        /// The make of this vehicle.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Make { get; set; }

        /// <summary>
        /// The model of this vehicle.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Model { get; set; }

        /// <summary>
        /// The engine used in this vehicle.
        /// </summary>
        [Required]
        [StringLength(40)]
        public string Engine { get; set; }
    }
}
