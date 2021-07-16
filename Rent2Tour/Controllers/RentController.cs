using Rent2Tour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rent2Tour.Controllers
{
    public class RentController : Controller
    {
        // GET: Rent
        Rent2TourContext db = new Rent2TourContext();
        // GET: RentBook
        public ActionResult RentACar()
        {
            ViewBag.categID = new SelectList(db.category.ToList(), "ID", "Name");
            ViewBag.carMake = new SelectList(db.car.ToList(), "Make", "Make");
            ViewBag.carModel = new SelectList(db.car.ToList(), "Model", "Model");
            ViewBag.custID = new SelectList(db.customer.ToList(), "ID", "CustName");
            ViewBag.carID = new List<SelectListItem>
            {
                new SelectListItem{Text="Select",Value="Select" }
            };
            return View();
        }

        [HttpPost]
        public ActionResult RentACar(Rent rent)
        {
            return View();

        }

            public JsonResult RentJson(int id)
        {
            var cars = db.car.Where(x => x.categID == id).Select(x => new
            {
                Name = x.Make,
                ID = x.Make
            });
            return Json(cars, JsonRequestBehavior.AllowGet);
        }


        public JsonResult RentJsonMake(string make,int categ)
        {
            var cars = db.car.Where(x => x.categID == categ && x.Make==make).Select(x => new
            {
                Name = x.Model,
                ID = x.Model
            });
            return Json(cars, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public ActionResult Save(Rent rent)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.rent.Add(rent);
        //        var car = db.car.Where(x => x.CarNo == rent.carID);
        //        db.SaveChanges();
        //        return RedirectToAction("RentACar");
        //    }
        //    return View(rent);

        //}

        [HttpPost]
        public ActionResult Save(Rent rent)
        {
            db.rent.Add(rent);
            db.SaveChanges();
            return RedirectToAction("RentACar");
        }

    }
}
