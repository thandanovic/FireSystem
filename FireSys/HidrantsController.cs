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

namespace FireSys
{
    public class HidrantsController : Controller
    {
        private FireSysModel db = new FireSysModel();

        // GET: Hidrants
        public ActionResult Index()
        {
            var hidrants = db.Hidrants.Include(h => h.HidrantTip).Include(h => h.Instalacija).Include(h => h.Kompletnost).Include(h => h.Lokacija).Include(h => h.PromjerMlaznice);
            return View(hidrants.ToList());
        }

        // GET: Hidrants/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hidrant hidrant = db.Hidrants.Find(id);
            if (hidrant == null)
            {
                return HttpNotFound();
            }
            return View(hidrant);
        }

        // GET: Hidrants/Create
        public ActionResult Create()
        {
            ViewBag.HidrantTipId = new SelectList(db.HidrantTips, "HidrantTipId", "Naziv");
            ViewBag.InstalacijaId = new SelectList(db.Instalacijas, "InstalacijaId", "Naziv");
            ViewBag.KompletnostId = new SelectList(db.Kompletnosts, "KompletnostId", "Naziv");
            ViewBag.LokacijaId = new SelectList(db.Lokacijas, "LokacijaId", "Naziv");
            ViewBag.PromjerMlazniceId = new SelectList(db.PromjerMlaznices, "PromjerMlazniceId", "PromjerMlazniceId");
            return View();
        }

        // POST: Hidrants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HidrantId,Oznaka,InstalacijaId,HidrantTipId,HidrostatickiPritisak,HidrodinamickiPritisak,Protok,KompletnostId,LokacijaId,PromjerMlazniceId")] Hidrant hidrant)
        {
            if (ModelState.IsValid)
            {
                db.Hidrants.Add(hidrant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HidrantTipId = new SelectList(db.HidrantTips, "HidrantTipId", "Naziv", hidrant.HidrantTipId);
            ViewBag.InstalacijaId = new SelectList(db.Instalacijas, "InstalacijaId", "Naziv", hidrant.InstalacijaId);
            ViewBag.KompletnostId = new SelectList(db.Kompletnosts, "KompletnostId", "Naziv", hidrant.KompletnostId);
            ViewBag.LokacijaId = new SelectList(db.Lokacijas, "LokacijaId", "Naziv", hidrant.LokacijaId);
            ViewBag.PromjerMlazniceId = new SelectList(db.PromjerMlaznices, "PromjerMlazniceId", "PromjerMlazniceId", hidrant.PromjerMlazniceId);
            return View(hidrant);
        }

        // GET: Hidrants/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hidrant hidrant = db.Hidrants.Find(id);
            if (hidrant == null)
            {
                return HttpNotFound();
            }
            ViewBag.HidrantTipId = new SelectList(db.HidrantTips, "HidrantTipId", "Naziv", hidrant.HidrantTipId);
            ViewBag.InstalacijaId = new SelectList(db.Instalacijas, "InstalacijaId", "Naziv", hidrant.InstalacijaId);
            ViewBag.KompletnostId = new SelectList(db.Kompletnosts, "KompletnostId", "Naziv", hidrant.KompletnostId);
            ViewBag.LokacijaId = new SelectList(db.Lokacijas, "LokacijaId", "Naziv", hidrant.LokacijaId);
            ViewBag.PromjerMlazniceId = new SelectList(db.PromjerMlaznices, "PromjerMlazniceId", "PromjerMlazniceId", hidrant.PromjerMlazniceId);
            return View(hidrant);
        }

        // POST: Hidrants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HidrantId,Oznaka,InstalacijaId,HidrantTipId,HidrostatickiPritisak,HidrodinamickiPritisak,Protok,KompletnostId,LokacijaId,PromjerMlazniceId")] Hidrant hidrant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hidrant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HidrantTipId = new SelectList(db.HidrantTips, "HidrantTipId", "Naziv", hidrant.HidrantTipId);
            ViewBag.InstalacijaId = new SelectList(db.Instalacijas, "InstalacijaId", "Naziv", hidrant.InstalacijaId);
            ViewBag.KompletnostId = new SelectList(db.Kompletnosts, "KompletnostId", "Naziv", hidrant.KompletnostId);
            ViewBag.LokacijaId = new SelectList(db.Lokacijas, "LokacijaId", "Naziv", hidrant.LokacijaId);
            ViewBag.PromjerMlazniceId = new SelectList(db.PromjerMlaznices, "PromjerMlazniceId", "PromjerMlazniceId", hidrant.PromjerMlazniceId);
            return View(hidrant);
        }

        // GET: Hidrants/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hidrant hidrant = db.Hidrants.Find(id);
            if (hidrant == null)
            {
                return HttpNotFound();
            }
            return View(hidrant);
        }

        // POST: Hidrants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hidrant hidrant = db.Hidrants.Find(id);
            db.Hidrants.Remove(hidrant);
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
