using ProductDeliveryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;

namespace ProductDeliveryManagement.Controllers
{
    public class DeliveryPeopleController : Controller
    {
        DeliveryManagementDbContext db = new DeliveryManagementDbContext();
        // GET: DeliveryPeople
        public ActionResult Index(int page=1)
        {
            return View(db.DeliveryPeople.OrderBy(x => x.DeliveryPersonId).ToPagedList(page, 5));
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(DeliveryPerson person)
        {
            if(ModelState.IsValid)
            {
                db.DeliveryPeople.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(person);
        }
    }
}