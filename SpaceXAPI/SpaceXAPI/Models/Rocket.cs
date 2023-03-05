using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SpaceXAPI.Models
{
    public class Rocket
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Range(0, 200)]
        public double Height { get; set; }

        [JsonIgnore]
        public ICollection<Launch> Launches { get; set; }
    }
}