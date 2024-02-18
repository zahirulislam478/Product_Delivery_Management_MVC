using ProductDeliveryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;
using X.PagedList.Mvc;


namespace ProductDeliveryManagement.Controllers
{
    public class ProductsController : Controller
    {
        DeliveryManagementDbContext db = new DeliveryManagementDbContext();
        // GET: Products
        public ActionResult Index(int page=1)
        {
            return View(db.Products.OrderBy(x=>x.ProductId).ToPagedList(page, 5));
        }
        public ActionResult Create() 
        { 
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if(ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }
    }
}