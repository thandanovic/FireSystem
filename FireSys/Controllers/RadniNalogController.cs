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
using System.Collections;
using DevExpress.Web.Mvc;
using DevExpress.Web;
using FireSys.Helpers;

namespace FireSys.Controllers
{
    public class RadniNalogController : Controller
    {
        private FireSysModel db = new FireSysModel();

        // GET: RadniNalog
        public ActionResult Index()
        {
            //var radniNalogs = db.RadniNalogs.Include(r => r.Lokacija);
            //return View(radniNalogs.ToList());

            Session["WorkingSheetModel"] = GetSheets();
            return View(Session["WorkingSheetModel"]);
        }

        public ActionResult RadniNalogGridView()
        {
            Session["WorkingSheetModel"] = GetSheets();
            return PartialView("RadniNalogGridView", Session["WorkingSheetModel"]);
        }

        public IEnumerable GetSheets()
        {
            FireSysModel DB = new FireSysModel();
            return DB.RadniNalogs.ToList();
        }

        public ActionResult ExportTo(string OutputFormat)
        {
            var model = Session["WorkingSheetModel"];

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
            settings.Name = "radniNalogGrid";
            settings.CallbackRouteValues = new { Controller = "RadniNalog", Action = "RadniNalogGridView" };
            settings.Width = System.Web.UI.WebControls.Unit.Percentage(100);

            settings.KeyFieldName = "RadniNalogId";

            settings.Settings.ShowFilterRow = true;

            settings.CommandColumn.Visible = true;
            settings.CommandColumn.ShowSelectCheckbox = true;

            settings.SettingsExport.ExportSelectedRowsOnly = true;

            settings.Columns.Add(column =>
            {
                column.FieldName = "RadniNalogId";
                column.Caption = "ID";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "DatumNaloga";
                column.Caption = "Datum naloga";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "BrojNaloga";
                column.Caption = "Broj naloga";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "BrojNalogaMjesec";
                column.Caption = "Mjesec";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "BrojNalogaGodina";
                column.Caption = "Godina";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "DatumKreiranja";
                column.Caption = "Datum kreiranja";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Aparati";
                column.Caption = "Aparati";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Hidranti";
                column.Caption = "Hidranti";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Komentar";
                column.Caption = "Komentar";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "BrojHidranata";
                column.Caption = "Broj hidranata";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "BrojAparata";
                column.Caption = "Broj aparata";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Narucilac";
                column.Caption = "Naručilac";
            });

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.Landscape = true;

            settings.SettingsExport.PageHeader.Center = "Powered by dostah";
            settings.SettingsExport.ReportHeader = "Vatrositemi";

            //settings.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;

            settings.CommandColumn.SelectAllCheckboxMode = GridViewSelectAllCheckBoxMode.Page;

            settings.Settings.ShowFilterRowMenu = true;
            settings.CommandColumn.ShowClearFilterButton = true;
            settings.SettingsPager.EnableAdaptivity = true;

            settings.CommandColumn.ShowNewButton = false;
            settings.CommandColumn.ShowDeleteButton = false;
            settings.CommandColumn.ShowEditButton = false;

            return settings;
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
                radniNalog.DatumKreiranja = DateTime.Now;
                db.RadniNalogs.Add(radniNalog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LokacijaId = new SelectList(db.Lokacijas, "LokacijaId", "Naziv", radniNalog.LokacijaId);
            //ViewBag.KlijentId = new SelectList(db.Klijents, "KlijentId", "Naziv", radniNalog.Klije)
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
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    RadniNalog radniNalog = db.RadniNalogs.Find(id);
        //    if (radniNalog == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(radniNalog);
        //}

        //// POST: RadniNalog/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    RadniNalog radniNalog = db.RadniNalogs.Find(id);
        //    db.RadniNalogs.Remove(radniNalog);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        public ActionResult Delete(int id)
        {
            try
            {
                RadniNalog rn = db.RadniNalogs.Find(id);
                db.RadniNalogs.Remove(rn);
                db.SaveChanges();
                //return RedirectToAction("Index");
                return Json("Success");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return RedirectToAction("Index");
            }
        }

        //public ActionResult Report(int id)
        //{
        //    RadniNalogReport report = new RadniNalogReport();
        //    report.Parameters["ID"].Value = id;
        //    report.RequestParameters = false;
        //    report.CreateDocument();

        //    return View(report);
        //}

        [HttpPost]
        public JsonResult Report(int id)
        {
            RadniNalogReport report = new RadniNalogReport();
            report.Parameters["ID"].Value = id;
            report.RequestParameters = false;
            report.CreateDocument();
            //report.PrintingSystem.Document.AutoFitToPagesWidth = 1;

            return Json(new
            {
                content = GridViewHelper.RenderRazorViewToString(this, "~/Views/RadniNalog/Report.cshtml", report)
            });
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
