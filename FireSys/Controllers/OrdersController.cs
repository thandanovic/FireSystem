using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FireSys.DB;
using FireSys.Entities;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace FireSys.Controllers
{
    public class OrdersController : Controller
    {
        private Repository db = new Repository();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.UserInfo).Include(o => o.Location);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "LocationName");
            ViewBag.UserInfoId = new SelectList(db.UserInfos, "UserInfoId", "Email");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,UserInfoId,LocationId,PaymentStatus,OrderStatus,ContainerId,Deadline,TotalPrice,Weight,CreatedBy,CreatedOn,UpdatedBy,UpdatedOn")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "LocationName", order.LocationId);
            ViewBag.UserInfoId = new SelectList(db.UserInfos, "UserInfoId", "Email", order.UserInfoId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "LocationName", order.LocationId);
            ViewBag.UserInfoId = new SelectList(db.UserInfos, "UserInfoId", "Email", order.UserInfoId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,UserInfoId,LocationId,PaymentStatus,OrderStatus,ContainerId,Deadline,TotalPrice,Weight,CreatedBy,CreatedOn,UpdatedBy,UpdatedOn")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "LocationName", order.LocationId);
            ViewBag.UserInfoId = new SelectList(db.UserInfos, "UserInfoId", "Email", order.UserInfoId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SendMail()
        {
            SendOrderEmail().Wait();
            return RedirectToAction("Index");
        }

        public async Task SendOrderEmail()
        {
            var apiKey = Environment.GetEnvironmentVariable("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");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("CaWeOrder@example.com", "FireSys-APM Terminal 1");
            var subject = "FireSys Order Status!";
            var to = new EmailAddress("tarik.handanovic@gmail.com", "Tarik");
            var plainTextContent = "Your order is proceeed. Current Status of order with number <strong> 123AV456 </strong> is:";
            var htmlContent = "<strong>Payment Status:</strong> Not Paid </br>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            
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
