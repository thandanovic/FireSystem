using DevExpress.Web.Mvc;
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
using DevExpress.Web;
using Newtonsoft.Json;
using System.Collections;
using FireSys.Helpers;
using System.Web.UI;
using System.Web.Routing;
using System.Data.SqlClient;
using FireSys.Attributes;

namespace FireSys.Controllers
{
    [AuthorizeRoles("user")]
    public class HidrantsController : BaseController
    {
        public ActionResult Index()
        {
            Session["HidrantsModel"] = GetHidrants();
            return View(Session["HidrantsModel"]);
        }

        public ActionResult HidrantsGridView()
        {
            Session["HidrantsModel"] = GetHidrants();
            return PartialView("HidrantsGridView", Session["HidrantsModel"]);
        }

        public IEnumerable GetHidrants()
        {
            FireSysModel DB = new FireSysModel();
            return DB.Hidrants.ToList();
        }

        public ActionResult ExportTo(string OutputFormat)
        {
            var model = Session["HidrantsModel"];

            switch (OutputFormat.ToUpper())
            {
                case "CSV":
                    return GridViewExtension.ExportToCsv(GridViewHelper.GetHidrantsView(null), model);
                case "PDF":
                    return GridViewExtension.ExportToPdf(GetGridSettings(), model);
                case "RTF":
                    return GridViewExtension.ExportToRtf(GridViewHelper.GetHidrantsView(null), model);
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

            settings.Name = "hidrantsGrid";
            settings.CallbackRouteValues = new { Controller = "Hidrants", Action = "HidrantsGridView" };
            settings.Width = System.Web.UI.WebControls.Unit.Percentage(100);

            settings.KeyFieldName = "HidrantId";

            settings.Settings.ShowFilterRow = true;

            settings.CommandColumn.Visible = true;
            settings.CommandColumn.ShowSelectCheckbox = true;

            settings.SettingsExport.ExportSelectedRowsOnly = true;

            settings.Columns.Add(column =>
            {
                column.FieldName = "HidrantId";
                column.Caption = "ID";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Oznaka";
                column.Caption = "Oznaka";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Protok";
                column.Caption = "Protok";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "HidrostatickiPritisak";
                column.Caption = "Hidrostatički pritisak";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "HidrodinamickiPritisak";
                column.Caption = "Hidrodinamički pritisak";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "HidrantTip.Naziv";
                column.Caption = "Tip";

                column.ColumnType = MVCxGridViewColumnType.ComboBox;
                var comboBoxProperties = column.PropertiesEdit as ComboBoxProperties;
                comboBoxProperties.DataSource = FireSys.Helpers.DataProvider.GetHidrantTypes();
                comboBoxProperties.TextField = "Naziv";
                comboBoxProperties.ValueField = "Naziv";
                comboBoxProperties.ValueType = typeof(string);
                comboBoxProperties.DropDownStyle = DropDownStyle.DropDown;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Instalacija.Naziv";
                column.Caption = "Instalacija";

                column.ColumnType = MVCxGridViewColumnType.ComboBox;
                var comboBoxProperties = column.PropertiesEdit as ComboBoxProperties;
                comboBoxProperties.DataSource = FireSys.Helpers.DataProvider.GetInstallations();
                comboBoxProperties.TextField = "Naziv";
                comboBoxProperties.ValueField = "Naziv";
                comboBoxProperties.ValueType = typeof(string);
                comboBoxProperties.DropDownStyle = DropDownStyle.DropDown;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Kompletnost.Naziv";
                column.Caption = "Kompletnost";

                column.ColumnType = MVCxGridViewColumnType.ComboBox;
                var comboBoxProperties = column.PropertiesEdit as ComboBoxProperties;
                comboBoxProperties.DataSource = FireSys.Helpers.DataProvider.GetKompletnost();
                comboBoxProperties.TextField = "Naziv";
                comboBoxProperties.ValueField = "Naziv";
                comboBoxProperties.ValueType = typeof(string);
                comboBoxProperties.DropDownStyle = DropDownStyle.DropDown;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Lokacija.Naziv";
                column.Caption = "Lokacija";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "PromjerMlaznice.Promjer";
                column.Caption = "Promjer mlaznice";
            });

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.Landscape = true;

            settings.SettingsExport.PageHeader.Center = "Vatrosistemi";
            settings.SettingsExport.ReportHeader = "Vatrositemi";

            settings.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;

            settings.CommandColumn.SelectAllCheckboxMode = GridViewSelectAllCheckBoxMode.Page;

            settings.Settings.ShowFilterRowMenu = true;
            settings.CommandColumn.ShowClearFilterButton = true;
            settings.SettingsPager.EnableAdaptivity = true;

            settings.CommandColumn.ShowNewButton = false;
            settings.CommandColumn.ShowDeleteButton = true;
            settings.CommandColumn.ShowEditButton = false;

            return settings;
        }

        // GET: Hidrants/Create
        public ActionResult Create()
        {
            ViewBag.HidrantTipId = new SelectList(DataProvider.DB.HidrantTips, "HidrantTipId", "Naziv");
            ViewBag.InstalacijaId = new SelectList(DataProvider.DB.Instalacijas, "InstalacijaId", "Naziv");
            ViewBag.KompletnostId = new SelectList(DataProvider.DB.Kompletnosts, "KompletnostId", "Naziv");
            ViewBag.LokacijaId = new SelectList(DataProvider.DB.Lokacijas, "LokacijaId", "Naziv");
            ViewBag.PromjerMlazniceId = new SelectList(DataProvider.DB.PromjerMlaznices, "PromjerMlazniceId", "PromjerMlazniceId");
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
                DataProvider.DB.Entry(hidrant).State = EntityState.Added;
                DataProvider.DB.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HidrantTipId = new SelectList(DataProvider.DB.HidrantTips, "HidrantTipId", "Naziv", hidrant.HidrantTipId);
            ViewBag.InstalacijaId = new SelectList(DataProvider.DB.Instalacijas, "InstalacijaId", "Naziv", hidrant.InstalacijaId);
            ViewBag.KompletnostId = new SelectList(DataProvider.DB.Kompletnosts, "KompletnostId", "Naziv", hidrant.KompletnostId);
            ViewBag.LokacijaId = new SelectList(DataProvider.DB.Lokacijas, "LokacijaId", "Naziv", hidrant.LokacijaId);
            ViewBag.PromjerMlazniceId = new SelectList(DataProvider.DB.PromjerMlaznices, "PromjerMlazniceId", "PromjerMlazniceId", hidrant.PromjerMlazniceId);
            return View(hidrant);
        }

        // GET: Hidrants/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hidrant hidrant = DataProvider.DB.Hidrants.Find(id);
            if (hidrant == null)
            {
                return HttpNotFound();
            }
            ViewBag.HidrantTipId = new SelectList(DataProvider.DB.HidrantTips, "HidrantTipId", "Naziv", hidrant.HidrantTipId);
            ViewBag.InstalacijaId = new SelectList(DataProvider.DB.Instalacijas, "InstalacijaId", "Naziv", hidrant.InstalacijaId);
            ViewBag.KompletnostId = new SelectList(DataProvider.DB.Kompletnosts, "KompletnostId", "Naziv", hidrant.KompletnostId);
            ViewBag.LokacijaId = new SelectList(DataProvider.DB.Lokacijas, "LokacijaId", "Naziv", hidrant.LokacijaId);
            ViewBag.PromjerMlazniceId = new SelectList(DataProvider.DB.PromjerMlaznices, "PromjerMlazniceId", "PromjerMlazniceId", hidrant.PromjerMlazniceId);
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
                DataProvider.DB.Entry(hidrant).State = EntityState.Modified;
                DataProvider.DB.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HidrantTipId = new SelectList(DataProvider.DB.HidrantTips, "HidrantTipId", "Naziv", hidrant.HidrantTipId);
            ViewBag.InstalacijaId = new SelectList(DataProvider.DB.Instalacijas, "InstalacijaId", "Naziv", hidrant.InstalacijaId);
            ViewBag.KompletnostId = new SelectList(DataProvider.DB.Kompletnosts, "KompletnostId", "Naziv", hidrant.KompletnostId);
            ViewBag.LokacijaId = new SelectList(DataProvider.DB.Lokacijas, "LokacijaId", "Naziv", hidrant.LokacijaId);
            ViewBag.PromjerMlazniceId = new SelectList(DataProvider.DB.PromjerMlaznices, "PromjerMlazniceId", "PromjerMlazniceId", hidrant.PromjerMlazniceId);
            return View(hidrant);
        }

        public ActionResult Delete(int id)
        {
            try
            {
                Hidrant hidrant = DataProvider.DB.Hidrants.Find(id);
                DataProvider.DB.Hidrants.Remove(hidrant);
                int i = DataProvider.DB.SaveChanges();
                //return RedirectToAction("Index");
                return Json("Success");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
