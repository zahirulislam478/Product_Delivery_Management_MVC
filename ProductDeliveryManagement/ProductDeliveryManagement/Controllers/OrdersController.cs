using ProductDeliveryManagement.Models;
using ProductDeliveryManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;
using X.PagedList.Mvc;
using System.Data.Entity;

namespace ProductDeliveryManagement.Controllers
{
    public class OrdersController : Controller
    {
        DeliveryManagementDbContext db = new DeliveryManagementDbContext();
        // GET: Orders
        public ActionResult Index(int page=1)
        {
            var data = db.Orders
                .AsEnumerable()
                 .OrderBy(x => x.OrderId)
                .Select(o => new OrderViewModel
                {
                    OrderId = o.OrderId,
                    OrderDate = o.OrderDate,
                    ProductId = o.ProductId,
                    Product = o.Product,
                    Quantity = o.Quantity,
                    CustomerName = o.CustomerName,
                    CustomerAddress = o.CustomerAddress,
                    CustomerPhone = o.CustomerPhone,
                    OrderStatus = getStatus(o)

                })
               
                .ToPagedList(page, 5);
            ViewBag.DeliverPeople = db.DeliveryPeople.ToList();
            return View(data);
        }
        public ActionResult Create()
        {
            ViewBag.Products = db.Products.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Order order)
        {
            if(ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Products = db.Products.ToList();
            return View(order);
        }
        [HttpPost]
        public ActionResult Assign(int OrderId, int DeilveryPersonId)
        {
            var order = db.Orders.Include(x=> x.OrderDelivery).FirstOrDefault(x => x.OrderId == OrderId);
            if (order == null) return new HttpNotFoundResult();
            order.OrderDelivery = new OrderDelivery { DeliveryPersonId = DeilveryPersonId };
            db.SaveChanges();
            return Json(new { success = true });
        }
        private OrderStatus getStatus(Order order)
        {
            var od =db.OrdersDeliveries.FirstOrDefault(x => x.OrderDeliveryId == order.OrderId);
            if (od == null) return OrderStatus.Pending;
            if (od.DeliveryPersonId != 0 && !od.IsDelivered) return OrderStatus.AssignedToDeliver;
            else return OrderStatus.AssignedToDeliver;

                
        }
    }
}