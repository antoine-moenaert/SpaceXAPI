using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SpaceXAPI.Models
{
    public class Launch
    {
        public int Id { get; set; }

        [Required]
        public int FlightNumber { get; set; }

        [Required]
        [StringLength(30)]
        public string MissionName { get; set; }

        public int LocationId { get; set; }
        public int RocketId { get; set; }
        public Rocket Rocket { get; set; }
        public Location Location { get; set; }

        [JsonIgnore]
        public ICollection<LaunchContract> LaunchContracts { get; set; }
    }
}
