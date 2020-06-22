using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MiniCarsales.Models
{
    public class Car : Vehicle
    {
        /// <summary>
        /// The number of doors on this car.
        /// </summary>
        [Required]
        [Range(1, 10)]
        public int NumberOfDoors { get; set; }

        /// <summary>
        /// Number of wheels on this car.
        /// </summary>
        [Required]
        [Range(3, 10)]
        public int NumberOfWheels { get; set; }

        /// <summary>
        /// The body type of this car.
        /// </summary>
        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        public CarBodyType CarBodyType { get; set; }
    }
}
