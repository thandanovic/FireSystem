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
using DevExpress.Web.Mvc;
using FireSys.Helpers;
using System.Collections;
using DevExpress.Web;

namespace FireSys.Controllers
{
    public class ZapisnikController : Controller
    {
        private FireSysModel db = new FireSysModel();

        public ActionResult Index()
        {
            Session["ZapisnikModel"] = GetZapisniks();
            return View(Session["ZapisnikModel"]);
        }

        public ActionResult ZapisnikGridView()
        {
            Session["ZapisnikModel"] = GetZapisniks();
            return PartialView("ZapisnikGridView", Session["ZapisnikModel"]);
        }

        public IEnumerable GetZapisniks()
        {
            FireSysModel DB = new FireSysModel();
            return DB.Zapisniks.Include(z => z.Lokacija).Include(z => z.RadniNalog).Include(z => z.RadniNalog1).Include(z => z.RadniNalog2).Include(z => z.ZapisnikTip).ToList();
        }

        public ActionResult ExportTo(string OutputFormat)
        {
            var model = Session["ZapisnikModel"];

            switch (OutputFormat.ToUpper())
            {           
                case "PDF":
                    return GridViewExtension.ExportToPdf(GetGridSettings(), model);             
                case "XLS":
                    return GridViewExtension.ExportToXls(GetGridSettings(), model);
                case "XLSX":
                    return GridViewExtension.ExportToXlsx(GetGridSettings(), model);
                default:
                    return RedirectToAction("Index");
            }
        }

        public GridViewSettings GetGridSettings()
        {
            var settings = new GridViewSettings();

            settings.Name = "zapisnikGrid";
            settings.CallbackRouteValues = new { Controller = "Zapisnik", Action = "ZapisnikGridView" };
            settings.Width = System.Web.UI.WebControls.Unit.Percentage(100);

            settings.KeyFieldName = "ZapisnikId";

            settings.Settings.ShowFilterRow = true;

            settings.CommandColumn.Visible = true;
            settings.CommandColumn.ShowSelectCheckbox = true;

            settings.SettingsExport.ExportSelectedRowsOnly = true;
            settings.SettingsPager.Mode = GridViewPagerMode.ShowPager;
            settings.SettingsPager.PageSize = 100;

            settings.Columns.Add(column =>
            {
                column.FieldName = "ZapisnikId";
                column.Caption = "ID";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "BrojZapisnika";
                column.Caption = "Broj zapisnika";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "BrojZapisnikaMjesec";
                column.Caption = "Broj zapisnika mjesec";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "BrojZapisnikaGodina";
                column.Caption = "Broj zapisnika godina";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "DatumKreiranja";
                column.Caption = "Datum kreiranja";
                column.PropertiesEdit.DisplayFormatString = "dd.MM.yyyy";

            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "DatumBrisanja";
                column.Caption = "Datum brisanja";
                column.PropertiesEdit.DisplayFormatString = "dd.MM.yyyy";

            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "DatumZapisnika";
                column.Caption = "Datum zapisnika";
                column.PropertiesEdit.DisplayFormatString = "dd.MM.yyyy";

            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Validan";
                column.Caption = "Validan";
                column.ColumnType = MVCxGridViewColumnType.CheckBox;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Napomena";
                column.Caption = "Napomena";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Fakturisano";
                column.Caption = "Fakturisano";
                column.ColumnType = MVCxGridViewColumnType.CheckBox;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "BrojFakture";
                column.Caption = "Broj fakture";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "DokumentacijaPrisutna";
                column.Caption = "Dokumentacija";
                column.ColumnType = MVCxGridViewColumnType.CheckBox;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "LokacijaHidranata";
                column.Caption = "Lokacija hidranata";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "HidrantPrikljucenNa";
                column.Caption = "Hidrant priključen na";
            });



            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.Landscape = true;

            settings.SettingsExport.PageHeader.Center = "Powered by dostah";
            settings.SettingsExport.ReportHeader = "Vatrositemi";

            settings.CommandColumn.SelectAllCheckboxMode = GridViewSelectAllCheckBoxMode.Page;

            settings.Settings.ShowFilterRowMenu = true;
            settings.CommandColumn.ShowClearFilterButton = true;
            settings.SettingsPager.EnableAdaptivity = true;

            settings.CommandColumn.ShowNewButton = false;
            settings.CommandColumn.ShowDeleteButton = false;
            settings.CommandColumn.ShowEditButton = false;

            settings.Settings.ShowFilterRow = true;

            return settings;
        }


        //// GET: Zapisnik
        //public ActionResult Index()
        //{
        //    var zapisniks = db.Zapisniks.Include(z => z.Lokacija).Include(z => z.RadniNalog).Include(z => z.RadniNalog1).Include(z => z.RadniNalog2).Include(z => z.ZapisnikTip);
        //    return View(zapisniks.ToList().Take(10));
        //}

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
