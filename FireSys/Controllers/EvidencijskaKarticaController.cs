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
    public class EvidencijskaKarticaController : Controller
    {
        private FireSysModel db = new FireSysModel();

        // GET: EvidencijskaKartica
        public ActionResult Index()
        {
            var evidencijskaKarticas = db.EvidencijskaKarticas.Include(e => e.EvidencijskaKarticaTip);
            return View(evidencijskaKarticas.ToList());
        }

        // GET: EvidencijskaKartica/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvidencijskaKartica evidencijskaKartica = db.EvidencijskaKarticas.Find(id);
            if (evidencijskaKartica == null)
            {
                return HttpNotFound();
            }
            return View(evidencijskaKartica);
        }

        // GET: EvidencijskaKartica/Create
        public ActionResult Create()
        {
            ViewBag.EvidencijskaKarticaTipId = new SelectList(db.EvidencijskaKarticaTips, "EvidencijskaKarticaTipId", "EvidencijskaKarticaTipNaziv");
            return View();
        }

        // POST: EvidencijskaKartica/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EvidencijskaKarticaId,BrojEvidencijskeKartice,UserId,Validna,Obrisana,EvidencijskaKarticaTipId,DatumZaduzenja")] EvidencijskaKartica evidencijskaKartica)
        {
            if (ModelState.IsValid)
            {
                db.EvidencijskaKarticas.Add(evidencijskaKartica);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EvidencijskaKarticaTipId = new SelectList(db.EvidencijskaKarticaTips, "EvidencijskaKarticaTipId", "EvidencijskaKarticaTipNaziv", evidencijskaKartica.EvidencijskaKarticaTipId);
            return View(evidencijskaKartica);
        }

        // GET: EvidencijskaKartica/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvidencijskaKartica evidencijskaKartica = db.EvidencijskaKarticas.Find(id);
            if (evidencijskaKartica == null)
            {
                return HttpNotFound();
            }
            ViewBag.EvidencijskaKarticaTipId = new SelectList(db.EvidencijskaKarticaTips, "EvidencijskaKarticaTipId", "EvidencijskaKarticaTipNaziv", evidencijskaKartica.EvidencijskaKarticaTipId);
            return View(evidencijskaKartica);
        }

        // POST: EvidencijskaKartica/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EvidencijskaKarticaId,BrojEvidencijskeKartice,UserId,Validna,Obrisana,EvidencijskaKarticaTipId,DatumZaduzenja")] EvidencijskaKartica evidencijskaKartica)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evidencijskaKartica).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EvidencijskaKarticaTipId = new SelectList(db.EvidencijskaKarticaTips, "EvidencijskaKarticaTipId", "EvidencijskaKarticaTipNaziv", evidencijskaKartica.EvidencijskaKarticaTipId);
            return View(evidencijskaKartica);
        }

        // GET: EvidencijskaKartica/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvidencijskaKartica evidencijskaKartica = db.EvidencijskaKarticas.Find(id);
            if (evidencijskaKartica == null)
            {
                return HttpNotFound();
            }
            return View(evidencijskaKartica);
        }

        // POST: EvidencijskaKartica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EvidencijskaKartica evidencijskaKartica = db.EvidencijskaKarticas.Find(id);
            db.EvidencijskaKarticas.Remove(evidencijskaKartica);
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
