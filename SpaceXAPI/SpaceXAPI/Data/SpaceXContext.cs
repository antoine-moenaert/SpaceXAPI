using Microsoft.EntityFrameworkCore;
using SpaceXAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceXAPI.Data
{
    public class SpaceXContext : DbContext
    {
        public SpaceXContext(DbContextOptions<SpaceXContext> options): base(options)
        {
        }

        public DbSet<Launch> Launches { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Rocket> Rockets { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<LaunchContract> LaunchContracts { get; set; }
    }
}
