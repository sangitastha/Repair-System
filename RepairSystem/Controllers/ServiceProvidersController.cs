using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RepairSystem.Models;

namespace RepairSystem.Controllers
{
    public class ServiceProvidersController : Controller
    {
        private RepairSystemEntities db = new RepairSystemEntities();

        // GET: ServiceProviders
        public ActionResult Index()
        {
            var serviceProviders = db.ServiceProviders.Include(s => s.Category).Include(s => s.CityZipCode).Include(s => s.Country).Include(s => s.CountryCity);
            return View(serviceProviders.ToList());
        }

        // GET: ServiceProviders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceProvider serviceProvider = db.ServiceProviders.Find(id);
            if (serviceProvider == null)
            {
                return HttpNotFound();
            }
            return View(serviceProvider);
        }

        // GET: ServiceProviders/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            ViewBag.ZipId = new SelectList(db.CityZipCodes, "ZipId", "ZipNumber");
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "CountryName");
            ViewBag.CityId = new SelectList(db.CountryCities, "CityId", "CityName");
            return View();
        }

        // POST: ServiceProviders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ServiceProviderId,FirstName,MiddleName,LastName,Email,Price,ContactNumber,CategoryId,Location,CountryId,CityId,ZipId")] ServiceProvider serviceProvider)
        {
            if (ModelState.IsValid)
            {
                db.ServiceProviders.Add(serviceProvider);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", serviceProvider.CategoryId);
            ViewBag.ZipId = new SelectList(db.CityZipCodes, "ZipId", "ZipNumber", serviceProvider.ZipId);
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "CountryName", serviceProvider.CountryId);
            ViewBag.CityId = new SelectList(db.CountryCities, "CityId", "CityName", serviceProvider.CityId);
            return View(serviceProvider);
        }

        // GET: ServiceProviders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceProvider serviceProvider = db.ServiceProviders.Find(id);
            if (serviceProvider == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", serviceProvider.CategoryId);
            ViewBag.ZipId = new SelectList(db.CityZipCodes, "ZipId", "ZipNumber", serviceProvider.ZipId);
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "CountryName", serviceProvider.CountryId);
            ViewBag.CityId = new SelectList(db.CountryCities, "CityId", "CityName", serviceProvider.CityId);
            return View(serviceProvider);
        }

        // POST: ServiceProviders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ServiceProviderId,FirstName,MiddleName,LastName,Email,Price,ContactNumber,CategoryId,Location,CountryId,CityId,ZipId")] ServiceProvider serviceProvider)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serviceProvider).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", serviceProvider.CategoryId);
            ViewBag.ZipId = new SelectList(db.CityZipCodes, "ZipId", "ZipNumber", serviceProvider.ZipId);
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "CountryName", serviceProvider.CountryId);
            ViewBag.CityId = new SelectList(db.CountryCities, "CityId", "CityName", serviceProvider.CityId);
            return View(serviceProvider);
        }

        // GET: ServiceProviders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceProvider serviceProvider = db.ServiceProviders.Find(id);
            if (serviceProvider == null)
            {
                return HttpNotFound();
            }
            return View(serviceProvider);
        }

        // POST: ServiceProviders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiceProvider serviceProvider = db.ServiceProviders.Find(id);
            db.ServiceProviders.Remove(serviceProvider);
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

        public ActionResult Search()
        {
            ServiceProvider serviceProvider = new ServiceProvider();
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", serviceProvider.CategoryId);
            ViewBag.ZipId = new SelectList(db.CityZipCodes, "ZipId", "ZipNumber", serviceProvider.ZipId);
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "CountryName", serviceProvider.CountryId);
            ViewBag.CityId = new SelectList(db.CountryCities, "CityId", "CityName", serviceProvider.CityId);
            return View(serviceProvider);
        }

        [HttpPost]
        public ActionResult Search(ServiceProvider serviceProvider)
        {
            var list = db.ServiceProviders.Where(x => x.CategoryId == serviceProvider.CategoryId &&
                                         x.CityId == serviceProvider.CityId &&
                                         x.CountryId == serviceProvider.CountryId &&
                                         x.ZipId == serviceProvider.ZipId
            ).ToList();
            return View("Index", list);
        }

        public JsonResult GetCities(int countryId)
        {
            var list =db.CountryCities.Where(x => x.CountryId == countryId).Select(x => new Model
            {
                Id = x.CityId,
                Name = x.CityName
            }).ToList();
            return Json(list,JsonRequestBehavior.AllowGet); ;
        }

        public JsonResult GetZips(int cityId)
        {
            var list = db.CityZipCodes.Where(x => x.CityId == cityId).Select(x => new Model
            {
                Id = x.ZipId,
                Name = x.ZipNumber
            }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

    }
}
