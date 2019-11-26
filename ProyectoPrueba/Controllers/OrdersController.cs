using ProyectoPrueba.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProyectoPrueba.Controllers
{
    public class OrdersController : Controller
    {
        private NorthwindEntities bd = new NorthwindEntities();
        // GET: Orders
        public ActionResult Index()
        {
            var OrderList = bd.Orders.Take(15);
            return View(OrderList);
        }

        // GET: Orders/Details/5
        public ActionResult Detalle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var orderDetalle = bd.Orders.Find(id);


            if (orderDetalle == null)
            {
                return HttpNotFound();
            }

            return PartialView(orderDetalle);
        }


        // GET: Orders/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Orders/Edit/5
        [HttpPost]
        public ActionResult Confirmar(Orders order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            try
            {
                if (ModelState.IsValid) {
                    var objectOrder = bd.Orders.Find(order.OrderID);

                    objectOrder.Status = 1;
                    objectOrder.DateConfirmation = order.DateConfirmation;
                    objectOrder.Coments = order.Coments;
                    bd.Entry(objectOrder).State = System.Data.Entity.EntityState.Modified;
                    bd.SaveChanges();
                }
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

  

    }
}
