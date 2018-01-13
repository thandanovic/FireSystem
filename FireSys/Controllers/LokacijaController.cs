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
using System.Collections;
using DevExpress.Web.Mvc;
using DevExpress.Web;

namespace FireSys.Controllers
{
    public class LokacijaController : BaseController
    {
        
        private FireSysModel db = new FireSysModel();

        public ActionResult Index()
        {
            Session["LokacijaModel"] = GetLokacije();
            return View(Session["LokacijaModel"]);
        }

        public ActionResult LokacijaGridView()
        {
            Session["LokacijaModel"] = GetLokacije();
            return PartialView("LokacijaGridView", Session["LokacijaModel"]);
        }

        public IEnumerable GetLokacije()
        {
            FireSysModel DB = new FireSysModel();
            return DB.Lokacijas.Include(l => l.Klijent).Include(l => l.LokacijaVrsta).Include(l => l.Mjesto).Include(l => l.Regija).ToList();
        }

        public ActionResult ExportTo(string OutputFormat)
        {
            var model = Session["LokacijaModel"];

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

            settings.Name = "lokacijaGrid";
            settings.CallbackRouteValues = new { Controller = "Lokacija", Action = "LokacijaGridView" };
            settings.Width = System.Web.UI.WebControls.Unit.Percentage(100);

            settings.KeyFieldName = "LokacijaId";

            settings.Settings.ShowFilterRow = true;

            settings.CommandColumn.Visible = true;
            settings.CommandColumn.ShowSelectCheckbox = true;

            settings.SettingsExport.ExportSelectedRowsOnly = true;
            settings.SettingsPager.Mode = GridViewPagerMode.ShowPager;
            settings.SettingsPager.PageSize = 100;

            settings.Columns.Add(column =>
            {
                column.FieldName = "LokacijaId";
                column.Caption = "ID";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Naziv";
                column.Caption = "Naziv";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Adresa";
                column.Caption = "Adresa";
            });

            //MjestoId
            settings.Columns.Add(column =>
            {
                column.FieldName = "Mjesto.Naziv";
                column.Caption = "Mjesto";

                column.ColumnType = MVCxGridViewColumnType.ComboBox;
                var comboBoxProperties = column.PropertiesEdit as ComboBoxProperties;
                comboBoxProperties.DataSource = FireSys.Helpers.DataProvider.GetMjesta();
                comboBoxProperties.TextField = "Naziv";
                comboBoxProperties.ValueField = "Naziv";
                comboBoxProperties.ValueType = typeof(string);
                comboBoxProperties.DropDownStyle = DropDownStyle.DropDown;
            });

            //RegijaId
            settings.Columns.Add(column =>
            {
                column.FieldName = "Regija.Naziv";
                column.Caption = "Regija";

                column.ColumnType = MVCxGridViewColumnType.ComboBox;
                var comboBoxProperties = column.PropertiesEdit as ComboBoxProperties;
                comboBoxProperties.DataSource = FireSys.Helpers.DataProvider.GetRegions();
                comboBoxProperties.TextField = "Naziv";
                comboBoxProperties.ValueField = "Naziv";
                comboBoxProperties.ValueType = typeof(string);
                comboBoxProperties.DropDownStyle = DropDownStyle.DropDown;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Kontakt";
                column.Caption = "Kontakt";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Komentar";
                column.Caption = "Komentar";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "DatumKreiranja";
                column.Caption = "Datum kreiranja";
                column.PropertiesEdit.DisplayFormatString = "dd.MM.yyyy";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "KorisnikKreiraoId";
                column.Caption = "Kreirao";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "DatumBrisanja";
                column.Caption = "Datum brisanja";
                column.PropertiesEdit.DisplayFormatString = "dd.MM.yyyy";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Obrisano";
                column.Caption = "Obrisano";
                column.ColumnType = MVCxGridViewColumnType.CheckBox;
            });

            //KlijentId
            settings.Columns.Add(column =>
            {
                column.FieldName = "Klijent.Naziv";
                column.Caption = "Klijent";

                column.ColumnType = MVCxGridViewColumnType.ComboBox;
                var comboBoxProperties = column.PropertiesEdit as ComboBoxProperties;
                comboBoxProperties.DataSource = FireSys.Helpers.DataProvider.GetClients();
                comboBoxProperties.TextField = "Naziv";
                comboBoxProperties.ValueField = "Naziv";
                comboBoxProperties.ValueType = typeof(string);
                comboBoxProperties.DropDownStyle = DropDownStyle.DropDown;
            });

            //LokacijaVrstaId
            settings.Columns.Add(column =>
            {
                column.FieldName = "LokacijaVrsta.Naziv";
                column.Caption = "Lokacija";

                column.ColumnType = MVCxGridViewColumnType.ComboBox;
                var comboBoxProperties = column.PropertiesEdit as ComboBoxProperties;
                comboBoxProperties.DataSource = FireSys.Helpers.DataProvider.GetLokacijaVrsta();
                comboBoxProperties.TextField = "Naziv";
                comboBoxProperties.ValueField = "Naziv";
                comboBoxProperties.ValueType = typeof(string);
                comboBoxProperties.DropDownStyle = DropDownStyle.DropDown;
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





        private LokacijaManager lokacijaManager = new LokacijaManager();      

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

        public JsonResult GetLokacijeByKlijent(int klijentId)
        {
            SelectList lokacije1 = new SelectList(lokacijaManager.Find(x => x.KlijentId == klijentId).ToList(), "LokacijaId", "Naziv");
            return new JsonResult() { Data = lokacije1, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
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
