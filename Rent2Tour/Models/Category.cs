using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Rent2Tour.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string CustName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string SearchWord { get; set; }

    }

    public class Car
    {
        public int ID{ get; set; }
        public string  CarNo{ get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public bool Available { get; set; }
        public string Image { get; set; }
        [ForeignKey("category")]
        public int categID { get; set; }
        public Category category { get; set; }
        public string SearchWord { get; set; }
    }

    public class Rent
    {
        public int ID { get; set; }
        
        [ForeignKey("car")]
        public int? carID { get; set; }
        public Car car { get; set; }
        
        [ForeignKey("category")]
        public int? categID { get; set; }
        public Category category { get; set; }

        [ForeignKey("customer")]
        public int? custID { get; set; }
        public Customer customer { get; set; }

        public int Fee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


    }
}
