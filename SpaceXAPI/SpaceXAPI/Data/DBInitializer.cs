using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SpaceXAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceXAPI.Data
{
    public class DBInitializer
    {
        public static void Initialize(SpaceXContext context)
        {
           try
            {
                if (!context.Rockets.Any() ||
                    !context.Locations.Any() ||
                    !context.Launches.Any() ||
                    !context.Customers.Any() ||
                    !context.LaunchContracts.Any())
                    throw new Exception();
            }
            catch (Exception)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                fillDB();
            }

            void fillDB()
            {
                #region Rockets
                //List<Rocket> roList = new List<Rocket>();
                //if (!context.Rockets.Any())
                //{
                //    roList.Add(new Rocket()
                //    {
                //        Name = "Falcon 1",
                //        Height = 22.25
                //    });
                //    roList.Add(new Rocket()
                //    {
                //        Name = "Falcon 9",
                //        Height = 70
                //    });
                //    roList.Add(new Rocket()
                //    {
                //        Name = "Falcon Heavy",
                //        Height = 70
                //    });
                //    roList.Add(new Rocket()
                //    {
                //        Name = "Starship",
                //        Height = 118
                //    });

                //    foreach (Rocket ro in roList)
                //    {
                //        context.Rockets.Add(ro);
                //    }
                //    context.SaveChanges();
                //}
                //else
                //    roList = context.Rockets.ToList();
                #endregion

                List<Rocket> rockets;
                if (!context.Rockets.Any())
                {
                    rockets = new List<Rocket>()
                    {
                        new Rocket() { Name = "Falcon 1", Height = 22.25 },
                        new Rocket() { Name = "Falcon 9", Height = 70 },
                        new Rocket() { Name = "Falcon Heavy", Height = 70 },
                        new Rocket() { Name = "Starship", Height = 117 }
                    };

                    foreach (Rocket r in rockets)
                    {
                        context.Rockets.Add(r);
                    }
                    context.SaveChanges();
                }
                else
                    rockets = context.Rockets.ToList();

                #region Locations
                //List<Location> loList = new List<Location>();
                //if (!context.Locations.Any())
                //{
                //    loList.Add(new Location()
                //    {
                //        Name = "Vandenberg Air Force Base",
                //        Region = "California",
                //    });
                //    loList.Add(new Location()
                //    {
                //        Name = "Cape Canaveral",
                //        Region = "Florida",
                //    });
                //    loList.Add(new Location()
                //    {
                //        Name = "Boca Chica Village",
                //        Region = "Texas",
                //    });
                //    loList.Add(new Location()
                //    {
                //        Name = "Omelek Island",
                //        Region = "Marshall Islands",
                //    });

                //    foreach (Location lo in loList)
                //    {
                //        context.Locations.Add(lo);
                //    }
                //    context.SaveChanges();
                //}
                //else
                //    loList = context.Locations.ToList();
                #endregion

                List<Location> locations;
                if (!context.Locations.Any())
                {
                    locations = new List<Location>
                    {
                        new Location() { Name = "Vandenberg Air Force Base", Region = "California" },
                        new Location() { Name = "Cape Canaveral", Region = "Florida" },
                        new Location() { Name = "Boca Chica Village", Region = "Texas" },
                        new Location() { Name = "Omelek Island", Region = "Marshall Islands" }
                    };

                    foreach (Location l in locations)
                    {
                        context.Locations.Add(l);
                    }
                    context.SaveChanges();
                }
                else
                    locations = context.Locations.ToList();

                #region Launches
                //List<Launch> laList = new List<Launch>();
                //if (!context.Launches.Any())
                //{
                //    laList.Add(new Launch()
                //    {
                //        Flightnumber = 93,
                //        //Customers = "SpaceX",
                //        MissionName = "Starlink 6",
                //        Rocket = roList.Where(ro => ro.Name == "Falcon 9").First(),
                //        Location = loList.Where(lo => lo.Name == "Cape Canaveral").First()
                //    });
                //    laList.Add(new Launch()
                //    {
                //        Flightnumber = 92,
                //        //Customers = "SpaceX",
                //        MissionName = "Starlink 5",
                //        Rocket = roList.Where(ro => ro.Name == "Falcon 9").First(),
                //        Location = loList.Where(lo => lo.Name == "Cape Canaveral").First()
                //    });
                //    laList.Add(new Launch()
                //    {
                //        Flightnumber = 91,
                //        //Customers = "NASA",
                //        MissionName = "CRS-20",
                //        Rocket = roList.Where(ro => ro.Name == "Falcon 9").First(),
                //        Location = loList.Where(lo => lo.Name == "Cape Canaveral").First()
                //    });

                //    foreach (Launch la in laList)
                //    {
                //        context.Launches.Add(la);
                //    }
                //    context.SaveChanges();
                //}
                //else
                //    laList = context.Launches.ToList();
                #endregion

                List<Launch> launches;
                if (!context.Launches.Any())
                {
                    launches = new List<Launch>()
                    {
                        new Launch() { FlightNumber = 93, MissionName = "Starlink 6", Rocket = context.Rockets.FirstOrDefault(r => r.Name == "Falcon 9"),
                            Location = context.Locations.FirstOrDefault(l => l.Name == "Cape Canaveral") },
                        new Launch() { FlightNumber = 92, MissionName = "Starlink 5", Rocket = context.Rockets.FirstOrDefault(r => r.Name == "Falcon 9"),
                            Location = context.Locations.FirstOrDefault(l => l.Name == "Cape Canaveral") },
                        new Launch() { FlightNumber = 91, MissionName = "CRS-20", Rocket = context.Rockets.FirstOrDefault(r => r.Name == "Falcon 9"),
                            Location = context.Locations.FirstOrDefault(l => l.Name == "Cape Canaveral") },
                    };

                    foreach (Launch l in launches)
                    {
                        context.Launches.Add(l);
                    }
                    context.SaveChanges();
                }
                else
                    launches = context.Launches.ToList();

                #region Customers
                //List<Customer> cList = new List<Customer>();
                //if (!context.Customers.Any())
                //{
                //    cList.Add(new Customer()
                //    {
                //        Name = "NASA"
                //    });

                //    cList.Add(new Customer()
                //    {
                //        Name = "ESA"
                //    });

                //    cList.Add(new Customer()
                //    {
                //        Name = "SpaceX"
                //    });

                //    cList.Add(new Customer()
                //    {
                //        Name = "MIT"
                //    });

                //    cList.Add(new Customer()
                //    {
                //        Name = "SES"
                //    });

                //    foreach (Customer c in cList)
                //    {
                //        context.Customers.Add(c);
                //    };
                //}
                //else
                //    cList = context.Customers.ToList();
                #endregion

                List<Customer> customers;
                if (!context.Customers.Any())
                {
                    customers = new List<Customer>()
                    {
                        new Customer { Name = "NASA", PhoneNumber = "+32487107365", EmailAddress = "nasa@nasa.com" },
                        new Customer { Name = "ESA", PhoneNumber = "+32487107366" , EmailAddress = "esa@esa.com"},
                        new Customer { Name = "Spacex", PhoneNumber = "+32487107367", EmailAddress = "spacex@spacex.com" },
                        new Customer { Name = "MIT", PhoneNumber = "+32487107368", EmailAddress = "mit@mit.com" },
                        new Customer { Name = "SES", PhoneNumber = "+32487107369", EmailAddress = "ses@ses.com" }
                    };

                    foreach (Customer c in customers)
                    {
                        context.Customers.Add(c);
                    };
                    context.SaveChanges();
                }
                else
                    customers = context.Customers.ToList();

                List<LaunchContract> contracts;
                if (!context.LaunchContracts.Any())
                {
                    contracts = new List<LaunchContract>()
                    {
                        new LaunchContract { LaunchId = 1, CustomerId = 2 },
                        new LaunchContract { LaunchId = 3, CustomerId = 1 },
                        new LaunchContract { LaunchId = 2, CustomerId = 4 },
                        new LaunchContract { LaunchId = 2, CustomerId = 3 }
                    };

                    foreach (LaunchContract c in contracts)
                    {
                        context.LaunchContracts.Add(c);
                    }
                    context.SaveChanges();
                }
                else
                    contracts = context.LaunchContracts.ToList();
            }
        }
    }
}