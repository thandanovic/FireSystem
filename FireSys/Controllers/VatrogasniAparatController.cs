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
    public class VatrogasniAparatController : Controller
    {
        private FireSysModel db = new FireSysModel();

        // GET: VatrogasniAparat
        public ActionResult Index()
        {
            var vatrogasniAparats = db.VatrogasniAparats.Include(v => v.EvidencijskaKartica).Include(v => v.Ispravnost).Include(v => v.Lokacija).Include(v => v.VatrogasniAparatTip).Include(v => v.VatrogasniAparatVrsta);
            return View(vatrogasniAparats.ToList());
        }

        // GET: VatrogasniAparat/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VatrogasniAparat vatrogasniAparat = db.VatrogasniAparats.Find(id);
            if (vatrogasniAparat == null)
            {
                return HttpNotFound();
            }
            return View(vatrogasniAparat);
        }

        // GET: VatrogasniAparat/Create
        public ActionResult Create()
        {
            ViewBag.EvidencijskaKarticaId = new SelectList(db.EvidencijskaKarticas, "EvidencijskaKarticaId", "BrojEvidencijskeKartice");
            ViewBag.IspravnostId = new SelectList(db.Ispravnosts, "IspravnostId", "Naziv");
            ViewBag.LokacijaId = new SelectList(db.Lokacijas, "LokacijaId", "Naziv");
            ViewBag.VatrogasniAparatTipId = new SelectList(db.VatrogasniAparatTips, "VatrogasniAparatTipId", "Naziv");
            ViewBag.VatrogasniAparatVrstaId = new SelectList(db.VatrogasniAparatVrstas, "VatrogasniAparatVrstaId", "Naziv");
            return View();
        }

        // POST: VatrogasniAparat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VatrogasniAparatId,VatrogasniAparatTipId,BrojaAparata,GodinaProizvodnje,EvidencijskaKarticaId,Napomena,LokacijaId,IspravnostId,IspitivanjeVrijediDo,DatumKreiranja,Obrisan,KorisnikKreiraoId,VatrogasniAparatVrstaId")] VatrogasniAparat vatrogasniAparat)
        {
            if (ModelState.IsValid)
            {
                db.VatrogasniAparats.Add(vatrogasniAparat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EvidencijskaKarticaId = new SelectList(db.EvidencijskaKarticas, "EvidencijskaKarticaId", "BrojEvidencijskeKartice", vatrogasniAparat.EvidencijskaKarticaId);
            ViewBag.IspravnostId = new SelectList(db.Ispravnosts, "IspravnostId", "Naziv", vatrogasniAparat.IspravnostId);
            ViewBag.LokacijaId = new SelectList(db.Lokacijas, "LokacijaId", "Naziv", vatrogasniAparat.LokacijaId);
            ViewBag.VatrogasniAparatTipId = new SelectList(db.VatrogasniAparatTips, "VatrogasniAparatTipId", "Naziv", vatrogasniAparat.VatrogasniAparatTipId);
            ViewBag.VatrogasniAparatVrstaId = new SelectList(db.VatrogasniAparatVrstas, "VatrogasniAparatVrstaId", "Naziv", vatrogasniAparat.VatrogasniAparatVrstaId);
            return View(vatrogasniAparat);
        }

        // GET: VatrogasniAparat/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VatrogasniAparat vatrogasniAparat = db.VatrogasniAparats.Find(id);
            if (vatrogasniAparat == null)
            {
                return HttpNotFound();
            }
            ViewBag.EvidencijskaKarticaId = new SelectList(db.EvidencijskaKarticas, "EvidencijskaKarticaId", "BrojEvidencijskeKartice", vatrogasniAparat.EvidencijskaKarticaId);
            ViewBag.IspravnostId = new SelectList(db.Ispravnosts, "IspravnostId", "Naziv", vatrogasniAparat.IspravnostId);
            ViewBag.LokacijaId = new SelectList(db.Lokacijas, "LokacijaId", "Naziv", vatrogasniAparat.LokacijaId);
            ViewBag.VatrogasniAparatTipId = new SelectList(db.VatrogasniAparatTips, "VatrogasniAparatTipId", "Naziv", vatrogasniAparat.VatrogasniAparatTipId);
            ViewBag.VatrogasniAparatVrstaId = new SelectList(db.VatrogasniAparatVrstas, "VatrogasniAparatVrstaId", "Naziv", vatrogasniAparat.VatrogasniAparatVrstaId);
            return View(vatrogasniAparat);
        }

        // POST: VatrogasniAparat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VatrogasniAparatId,VatrogasniAparatTipId,BrojaAparata,GodinaProizvodnje,EvidencijskaKarticaId,Napomena,LokacijaId,IspravnostId,IspitivanjeVrijediDo,DatumKreiranja,Obrisan,KorisnikKreiraoId,VatrogasniAparatVrstaId")] VatrogasniAparat vatrogasniAparat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vatrogasniAparat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EvidencijskaKarticaId = new SelectList(db.EvidencijskaKarticas, "EvidencijskaKarticaId", "BrojEvidencijskeKartice", vatrogasniAparat.EvidencijskaKarticaId);
            ViewBag.IspravnostId = new SelectList(db.Ispravnosts, "IspravnostId", "Naziv", vatrogasniAparat.IspravnostId);
            ViewBag.LokacijaId = new SelectList(db.Lokacijas, "LokacijaId", "Naziv", vatrogasniAparat.LokacijaId);
            ViewBag.VatrogasniAparatTipId = new SelectList(db.VatrogasniAparatTips, "VatrogasniAparatTipId", "Naziv", vatrogasniAparat.VatrogasniAparatTipId);
            ViewBag.VatrogasniAparatVrstaId = new SelectList(db.VatrogasniAparatVrstas, "VatrogasniAparatVrstaId", "Naziv", vatrogasniAparat.VatrogasniAparatVrstaId);
            return View(vatrogasniAparat);
        }

        // GET: VatrogasniAparat/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VatrogasniAparat vatrogasniAparat = db.VatrogasniAparats.Find(id);
            if (vatrogasniAparat == null)
            {
                return HttpNotFound();
            }
            return View(vatrogasniAparat);
        }

        // POST: VatrogasniAparat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VatrogasniAparat vatrogasniAparat = db.VatrogasniAparats.Find(id);
            db.VatrogasniAparats.Remove(vatrogasniAparat);
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
