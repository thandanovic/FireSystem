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

namespace FireSys.Controllers
{
    public class VatrogasniAparatController : Controller
    {
        private FireSysModel db = new FireSysModel();

        public ActionResult Index()
        {
            Session["AparatModel"] = GetAparate();
            return View(Session["AparatModel"]);
        }

        public ActionResult AparatGridView()
        {
            Session["AparatModel"] = GetAparate();
            return PartialView("AparatGridView", Session["AparatModel"]);
        }

        public IEnumerable GetAparate()
        {
            FireSysModel DB = new FireSysModel();
            return DB.VatrogasniAparats.Include(v => v.EvidencijskaKartica).Include(v => v.Ispravnost).Include(v => v.Lokacija).Include(v => v.VatrogasniAparatTip).Include(v => v.VatrogasniAparatVrsta).ToList();
        }

        public ActionResult ExportTo(string OutputFormat)
        {
            var model = Session["AparatModel"];

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
            settings.CallbackRouteValues = new { Controller = "VatrogasniAparat", Action = "AparatGridView" };
            settings.Width = System.Web.UI.WebControls.Unit.Percentage(100);

            settings.KeyFieldName = "VatrogasniAparatId";

            settings.Settings.ShowFilterRow = true;

            settings.CommandColumn.Visible = true;
            settings.CommandColumn.ShowSelectCheckbox = true;

            settings.SettingsExport.ExportSelectedRowsOnly = true;
            settings.SettingsPager.Mode = GridViewPagerMode.ShowPager;
            settings.SettingsPager.PageSize = 100;

            settings.Columns.Add(column =>
            {
                column.FieldName = "VatrogasniAparatId";
                column.Caption = "ID";
            });

            //VatrogasniAparatTipId
            settings.Columns.Add(column =>
            {
                column.FieldName = "VatrogasniAparatTip.Naziv";
                column.Caption = "Tip";

                column.ColumnType = MVCxGridViewColumnType.ComboBox;
                var comboBoxProperties = column.PropertiesEdit as ComboBoxProperties;
                comboBoxProperties.DataSource = FireSys.Helpers.DataProvider.GetTipAparata();
                comboBoxProperties.TextField = "Naziv";
                comboBoxProperties.ValueField = "Naziv";
                comboBoxProperties.ValueType = typeof(string);
                comboBoxProperties.DropDownStyle = DropDownStyle.DropDown;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "BrojaAparata";
                column.Caption = "Broj aparata";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "GodinaProizvodnje";
                column.Caption = "Godina proizvodnje";
            });

            //EvidencijskaKarticaId
            settings.Columns.Add(column =>
            {
                column.FieldName = "EvidencijskaKartica.Broj";
                column.Caption = "EK broj";

                column.ColumnType = MVCxGridViewColumnType.ComboBox;
                var comboBoxProperties = column.PropertiesEdit as ComboBoxProperties;
                comboBoxProperties.DataSource = FireSys.Helpers.DataProvider.GetEvidencijskeKartice();
                comboBoxProperties.TextField = "Broj";
                comboBoxProperties.ValueField = "Broj";
                comboBoxProperties.ValueType = typeof(string);
                comboBoxProperties.DropDownStyle = DropDownStyle.DropDown;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Napomena";
                column.Caption = "Napomena";
            });

            //LokacijaId
            settings.Columns.Add(column =>
            {
                column.FieldName = "Lokacija.Naziv";
                column.Caption = "Lokacija";

                column.ColumnType = MVCxGridViewColumnType.ComboBox;
                var comboBoxProperties = column.PropertiesEdit as ComboBoxProperties;
                comboBoxProperties.DataSource = FireSys.Helpers.DataProvider.GetLocations();
                comboBoxProperties.TextField = "Naziv";
                comboBoxProperties.ValueField = "Naziv";
                comboBoxProperties.ValueType = typeof(string);
                comboBoxProperties.DropDownStyle = DropDownStyle.DropDown;
            });

            //IspravnostId
            settings.Columns.Add(column =>
            {
                column.FieldName = "Ispravnost.Naziv";
                column.Caption = "Ispravnost";

                column.ColumnType = MVCxGridViewColumnType.ComboBox;
                var comboBoxProperties = column.PropertiesEdit as ComboBoxProperties;
                comboBoxProperties.DataSource = FireSys.Helpers.DataProvider.GetIspravnost();
                comboBoxProperties.TextField = "Naziv";
                comboBoxProperties.ValueField = "Naziv";
                comboBoxProperties.ValueType = typeof(string);
                comboBoxProperties.DropDownStyle = DropDownStyle.DropDown;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "IspitivanjeVrijediDo";
                column.Caption = "Ispitivanje vrijedi do";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "DatumKreiranja";
                column.Caption = "Datum kreiranja";
                column.PropertiesEdit.DisplayFormatString = "dd.MM.yyyy";

            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Obrisan";
                column.Caption = "Obrisan";
                column.ColumnType = MVCxGridViewColumnType.CheckBox;
            });

            //*Kreirao KorisnikKreiraoId

            //VatrogasniAparatVrstaId
            settings.Columns.Add(column =>
            {
                column.FieldName = "VatrogasniAparatVrsta.Naziv";
                column.Caption = "Vrsta aparata";

                column.ColumnType = MVCxGridViewColumnType.ComboBox;
                var comboBoxProperties = column.PropertiesEdit as ComboBoxProperties;
                comboBoxProperties.DataSource = FireSys.Helpers.DataProvider.GetVrstaAparata();
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
