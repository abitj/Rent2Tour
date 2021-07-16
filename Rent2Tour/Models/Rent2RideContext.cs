using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Rent2Tour.Models
{
    public class Rent2TourContext : DbContext
    {
        public DbSet<Category> category { get; set; }
        public DbSet<Car> car { get; set; }
        public DbSet<Customer> customer { get; set; }
        public DbSet<Rent> rent { get; set; }

        //public System.Data.Entity.DbSet<Rent2Tour.Models.CaRent> caRents { get; set; }
    }
}