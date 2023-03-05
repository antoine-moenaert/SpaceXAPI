using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceXAPI.Models
{
    public class LaunchContract
    {
        public int Id { get; set; }

        [Required]
        public int LaunchId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        // Navigation Properties
        public Launch Launch { get; set; }
        public Customer Customer { get; set; }
    }
}
