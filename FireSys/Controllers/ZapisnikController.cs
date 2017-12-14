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
    public class ZapisnikController : Controller
    {
        private FireSysModel db = new FireSysModel();

        // GET: Zapisnik
        public ActionResult Index()
        {
            var zapisniks = db.Zapisniks.Include(z => z.Lokacija).Include(z => z.RadniNalog).Include(z => z.RadniNalog1).Include(z => z.RadniNalog2).Include(z => z.ZapisnikTip);
            return View(zapisniks.ToList().Take(10));
        }

        // GET: Zapisnik/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zapisnik zapisnik = db.Zapisniks.Find(id);
            if (zapisnik == null)
            {
                return HttpNotFound();
            }
            return View(zapisnik);
        }

        // GET: Zapisnik/Create
        public ActionResult Create()
        {
            ViewBag.LokacijaId = new SelectList(db.Lokacijas, "LokacijaId", "Naziv");
            ViewBag.IzRadnogNalogaId = new SelectList(db.RadniNalogs, "RadniNalogId", "Komentar");
            ViewBag.KreiraniRadniNalogId = new SelectList(db.RadniNalogs, "RadniNalogId", "Komentar");
            ViewBag.RadniNalogId = new SelectList(db.RadniNalogs, "RadniNalogId", "Komentar");
            ViewBag.ZapisnikTipId = new SelectList(db.ZapisnikTips, "ZapisnikTipId", "Naziv");
            return View();
        }

        // POST: Zapisnik/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ZapisnikId,BrojZapisnika,BrojZapisnikaGodina,DatumKreiranja,RadniNalogId,Validan,DatumBrisanja,BrojZapisnikaMjesec,DatumZapisnika,ZapisnikTipId,LokacijaId,PregledIzvrsioId,Napomena,KorisnikKreiraoId,Fakturisano,KorisnikKontrolisaoId,BrojFakture,DokumentacijaPrisutna,LokacijaHidranata,HidrantPrikljucenNa,IzRadnogNalogaId,KreiraniRadniNalogId")] Zapisnik zapisnik)
        {
            if (ModelState.IsValid)
            {
                db.Zapisniks.Add(zapisnik);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LokacijaId = new SelectList(db.Lokacijas, "LokacijaId", "Naziv", zapisnik.LokacijaId);
            ViewBag.IzRadnogNalogaId = new SelectList(db.RadniNalogs, "RadniNalogId", "Komentar", zapisnik.IzRadnogNalogaId);
            ViewBag.KreiraniRadniNalogId = new SelectList(db.RadniNalogs, "RadniNalogId", "Komentar", zapisnik.KreiraniRadniNalogId);
            ViewBag.RadniNalogId = new SelectList(db.RadniNalogs, "RadniNalogId", "Komentar", zapisnik.RadniNalogId);
            ViewBag.ZapisnikTipId = new SelectList(db.ZapisnikTips, "ZapisnikTipId", "Naziv", zapisnik.ZapisnikTipId);
            return View(zapisnik);
        }

        // GET: Zapisnik/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zapisnik zapisnik = db.Zapisniks.Find(id);
            if (zapisnik == null)
            {
                return HttpNotFound();
            }
            ViewBag.LokacijaId = new SelectList(db.Lokacijas, "LokacijaId", "Naziv", zapisnik.LokacijaId);
            ViewBag.IzRadnogNalogaId = new SelectList(db.RadniNalogs, "RadniNalogId", "Komentar", zapisnik.IzRadnogNalogaId);
            ViewBag.KreiraniRadniNalogId = new SelectList(db.RadniNalogs, "RadniNalogId", "Komentar", zapisnik.KreiraniRadniNalogId);
            ViewBag.RadniNalogId = new SelectList(db.RadniNalogs, "RadniNalogId", "Komentar", zapisnik.RadniNalogId);
            ViewBag.ZapisnikTipId = new SelectList(db.ZapisnikTips, "ZapisnikTipId", "Naziv", zapisnik.ZapisnikTipId);
            return View(zapisnik);
        }

        // POST: Zapisnik/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ZapisnikId,BrojZapisnika,BrojZapisnikaGodina,DatumKreiranja,RadniNalogId,Validan,DatumBrisanja,BrojZapisnikaMjesec,DatumZapisnika,ZapisnikTipId,LokacijaId,PregledIzvrsioId,Napomena,KorisnikKreiraoId,Fakturisano,KorisnikKontrolisaoId,BrojFakture,DokumentacijaPrisutna,LokacijaHidranata,HidrantPrikljucenNa,IzRadnogNalogaId,KreiraniRadniNalogId")] Zapisnik zapisnik)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zapisnik).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LokacijaId = new SelectList(db.Lokacijas, "LokacijaId", "Naziv", zapisnik.LokacijaId);
            ViewBag.IzRadnogNalogaId = new SelectList(db.RadniNalogs, "RadniNalogId", "Komentar", zapisnik.IzRadnogNalogaId);
            ViewBag.KreiraniRadniNalogId = new SelectList(db.RadniNalogs, "RadniNalogId", "Komentar", zapisnik.KreiraniRadniNalogId);
            ViewBag.RadniNalogId = new SelectList(db.RadniNalogs, "RadniNalogId", "Komentar", zapisnik.RadniNalogId);
            ViewBag.ZapisnikTipId = new SelectList(db.ZapisnikTips, "ZapisnikTipId", "Naziv", zapisnik.ZapisnikTipId);
            return View(zapisnik);
        }

        // GET: Zapisnik/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zapisnik zapisnik = db.Zapisniks.Find(id);
            if (zapisnik == null)
            {
                return HttpNotFound();
            }
            return View(zapisnik);
        }

        // POST: Zapisnik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Zapisnik zapisnik = db.Zapisniks.Find(id);
            db.Zapisniks.Remove(zapisnik);
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
