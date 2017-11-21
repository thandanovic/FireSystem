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

namespace FireSys.Controllers
{
    public class RadniNalogController : Controller
    {
        private FireSysModel db = new FireSysModel();

        // GET: RadniNalog
        public ActionResult Index()
        {
            var radniNalogs = db.RadniNalogs.Include(r => r.Lokacija);
            return View(radniNalogs.ToList());
        }

        // GET: RadniNalog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RadniNalog radniNalog = db.RadniNalogs.Find(id);
            if (radniNalog == null)
            {
                return HttpNotFound();
            }
            return View(radniNalog);
        }

        // GET: RadniNalog/Create
        public ActionResult Create()
        {
            ViewBag.LokacijaId = new SelectList(db.Lokacijas, "LokacijaId", "Naziv");
            return View();
        }

        // POST: RadniNalog/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RadniNalogId,LokacijaId,DatumNaloga,KorisnikKreiraiId,BrojNaloga,BrojNalogaMjesec,BrojNalogaGodina,DatumKreiranja,Aparati,Hidranti,Komentar,BrojHidranata,BrojAparata,Narucilac")] RadniNalog radniNalog)
        {
            if (ModelState.IsValid)
            {
                db.RadniNalogs.Add(radniNalog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LokacijaId = new SelectList(db.Lokacijas, "LokacijaId", "Naziv", radniNalog.LokacijaId);
            return View(radniNalog);
        }

        // GET: RadniNalog/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RadniNalog radniNalog = db.RadniNalogs.Find(id);
            if (radniNalog == null)
            {
                return HttpNotFound();
            }
            ViewBag.LokacijaId = new SelectList(db.Lokacijas, "LokacijaId", "Naziv", radniNalog.LokacijaId);
            return View(radniNalog);
        }

        // POST: RadniNalog/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RadniNalogId,LokacijaId,DatumNaloga,KorisnikKreiraiId,BrojNaloga,BrojNalogaMjesec,BrojNalogaGodina,DatumKreiranja,Aparati,Hidranti,Komentar,BrojHidranata,BrojAparata,Narucilac")] RadniNalog radniNalog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(radniNalog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LokacijaId = new SelectList(db.Lokacijas, "LokacijaId", "Naziv", radniNalog.LokacijaId);
            return View(radniNalog);
        }

        // GET: RadniNalog/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RadniNalog radniNalog = db.RadniNalogs.Find(id);
            if (radniNalog == null)
            {
                return HttpNotFound();
            }
            return View(radniNalog);
        }

        // POST: RadniNalog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RadniNalog radniNalog = db.RadniNalogs.Find(id);
            db.RadniNalogs.Remove(radniNalog);
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
