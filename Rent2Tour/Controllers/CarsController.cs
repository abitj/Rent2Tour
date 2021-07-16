using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Rent2Tour.Models;

namespace Rent2Tour.Controllers
{
    public class CarsController : Controller
    {
        private Rent2TourContext db = new Rent2TourContext();

        // GET: Cars
        //public ActionResult Index()
        //{
        //    var car = db.car.Include(c => c.category);
        //    return View(car.ToList());
        //}

        [HttpGet]
        public async Task<ActionResult> Index(string SearchWord)
        {
            var cars = from c in db.car
                            select c;

            if (!String.IsNullOrEmpty(SearchWord))
            {
                cars = cars.Where(c => c.Make.Contains(SearchWord));
            }

            return View(await cars.ToListAsync());
        }


        [HttpPost]
        public string Index(string SearchWord, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + SearchWord;
        }
        // GET: Cars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.car.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // GET: Cars/Create
        public ActionResult Create()
        {
            ViewBag.categID = new SelectList(db.category, "ID", "Name");
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CarNo,Make,Model,Available,Image,categID")] Car car, HttpPostedFileBase[] Image)
        {
            if (ModelState.IsValid)
            {
                foreach (HttpPostedFileBase file in Image)
                {
                    try
                    {
                        int count = file.ContentLength;
                    }
                    catch
                    {
                        car.Image = "Images/JW.jpg";
                    }
                    int MaxContentLength = 1024 * 512 * 1; //0.5 MB
                    string[] AllowedFileExtensions = new string[] { ".jpg", ".JPG", ".txt", ".gif", ".png", ".PNG", ".pdf" };



                    if (file == null)
                    {
                        //ModelState.AddModelError("File", "Please Upload Your file");
                        //ViewBag.message = "Please pick a file for upload!";
                        car.Image = "Images/JW.jpg";
                    }
                    else if (!AllowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
                    {
                        ModelState.AddModelError("File", "Please use file of type: " + string.Join(", ", AllowedFileExtensions));
                        TempData["Message"] = "Wrong file Type";
                        return RedirectToAction("index");
                    }



                    else if (file.ContentLength > MaxContentLength)
                    {
                        ModelState.AddModelError("File", "Your file is too large, maximum allowed size is: " + 512 + " KB");
                        TempData["Message"] = "Your file is too large, maximum allowed size is: " + 512 + " KB";
                        return RedirectToAction("index");
                    }
                    else
                    {
                        var filename = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Images/"), filename);
                        file.SaveAs(path);
                        car.Image = "/Images/" + filename;
                    }
                    db.car.Add(car);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            ViewBag.categID = new SelectList(db.category, "ID", "Name", car.categID);
            return View(car);
        }

        // GET: Cars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.car.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            ViewBag.categID = new SelectList(db.category, "ID", "Name", car.categID);
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CarNo,Make,Model,Available,Image,categID")] Car car)
        {
            if (ModelState.IsValid)
            {
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.categID = new SelectList(db.category, "ID", "Name", car.categID);
            return View(car);
        }

        // GET: Cars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.car.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Car car = db.car.Find(id);
            db.car.Remove(car);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
