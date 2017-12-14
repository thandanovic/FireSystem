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
using Microsoft.AspNet.Identity;
using FireSys.Manager;
using Microsoft.Owin.Logging;

namespace FireSys.Controllers
{
    public class LokacijaController : BaseController
    {
        
        private FireSysModel db = new FireSysModel();
        private LokacijaManager lokacijaManager = new LokacijaManager();
        // GET: Lokacijas
        public ActionResult Index()
        {
            var lokacijas = db.Lokacijas.Include(l => l.Klijent).Include(l => l.LokacijaVrsta).Include(l => l.Mjesto).Include(l => l.Regija);
            return View(lokacijas.ToList());
        }       

        // GET: Lokacijas/Create
        public ActionResult Create()
        {
            ViewBag.KlijentId = new SelectList(db.Klijents, "KlijentId", "Naziv");
            ViewBag.LokacijaVrstaId = new SelectList(db.LokacijaVrstas, "LokacijaVrstaId", "Naziv");
            ViewBag.MjestoId = new SelectList(db.Mjestoes, "MjestoId", "Naziv");
            ViewBag.RegijaId = new SelectList(db.Regijas, "RegijaId", "Naziv");
            Lokacija lokacija = new Lokacija();
            lokacija.KorisnikKreiraoId = User.Identity.GetUserId();
            return View(lokacija);
        }

        // POST: Lokacijas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LokacijaId,Naziv,Adresa,MjestoId,RegijaId,Kontakt,Komentar,DatumKreiranja,DatumBrisanja,Obrisano,KorisnikKreiraoId,KlijentId,LokacijaVrstaId")] Lokacija lokacija)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    lokacijaManager.Add(lokacija);
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = Common.Constants.GenericErrorMessage;
                //log error to some logger
                log.Error(Common.Constants.GenericErrorMessage, ex);
            }
           
            ViewBag.KlijentId = new SelectList(db.Klijents, "KlijentId", "Naziv", lokacija.KlijentId);
            ViewBag.LokacijaVrstaId = new SelectList(db.LokacijaVrstas, "LokacijaVrstaId", "Naziv", lokacija.LokacijaVrstaId);
            ViewBag.MjestoId = new SelectList(db.Mjestoes, "MjestoId", "Naziv", lokacija.MjestoId);
            ViewBag.RegijaId = new SelectList(db.Regijas, "RegijaId", "Naziv", lokacija.RegijaId);
            return View(lokacija);
        }

        // GET: Lokacijas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lokacija lokacija = db.Lokacijas.Find(id);
            if (lokacija == null)
            {
                return HttpNotFound();
            }
            ViewBag.KlijentId = new SelectList(db.Klijents, "KlijentId", "Naziv", lokacija.KlijentId);
            ViewBag.LokacijaVrstaId = new SelectList(db.LokacijaVrstas, "LokacijaVrstaId", "Naziv", lokacija.LokacijaVrstaId);
            ViewBag.MjestoId = new SelectList(db.Mjestoes, "MjestoId", "Naziv", lokacija.MjestoId);
            ViewBag.RegijaId = new SelectList(db.Regijas, "RegijaId", "Naziv", lokacija.RegijaId);
            return View(lokacija);
        }

        // POST: Lokacijas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LokacijaId,Naziv,Adresa,MjestoId,RegijaId,Kontakt,Komentar,DatumKreiranja,KorisnikKreiraoId,DatumBrisanja,Obrisano,KlijentId,LokacijaVrstaId")] Lokacija lokacija)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(lokacija).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = Common.Constants.GenericErrorMessage;
                //log error to some logger
                log.Error(Common.Constants.GenericErrorMessage, ex);
            }
            ViewBag.KlijentId = new SelectList(db.Klijents, "KlijentId", "Naziv", lokacija.KlijentId);
            ViewBag.LokacijaVrstaId = new SelectList(db.LokacijaVrstas, "LokacijaVrstaId", "Naziv", lokacija.LokacijaVrstaId);
            ViewBag.MjestoId = new SelectList(db.Mjestoes, "MjestoId", "Naziv", lokacija.MjestoId);
            ViewBag.RegijaId = new SelectList(db.Regijas, "RegijaId", "Naziv", lokacija.RegijaId);
            return View(lokacija);
        }
        
        // POST: Lokacijas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Lokacija lokacija = db.Lokacijas.Find(id);
                db.Lokacijas.Remove(lokacija);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = Common.Constants.GenericErrorMessage;
                //log error to some logger
                log.Error(Common.Constants.GenericErrorMessage, ex);
            }
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
