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

namespace FireSys.Controllers
{
    public class HidrantsController : Controller
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
                    return GridViewExtension.ExportToXls(GridViewHelper.GetHidrantsView(null), model);
                case "XLSX":
                    return GridViewExtension.ExportToXlsx(GridViewHelper.GetHidrantsView(null), model);
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

            //settings.Columns.Add(column =>
            //{
            //    column.FieldName = "Lokacija.Naziv";
            //    column.Caption = "Lokacija";

            //    column.ColumnType = MVCxGridViewColumnType.ComboBox;
            //    var comboBoxProperties = column.PropertiesEdit as ComboBoxProperties;
            //    comboBoxProperties.DataSource = FireSys.Helpers.DataProvider.GetLocations();
            //    comboBoxProperties.TextField = "Naziv";
            //    comboBoxProperties.ValueField = "Naziv";
            //    comboBoxProperties.ValueType = typeof(string);
            //    comboBoxProperties.DropDownStyle = DropDownStyle.DropDown;
            //});

            settings.Columns.Add(column =>
            {
                column.FieldName = "PromjerMlaznice.Promjer";
                column.Caption = "Promjer mlaznice";

                //column.Settings.AutoFilterCondition = AutoFilterCondition.Equals;
            });


            // Export-specific settings  
            //settings.SettingsExport.ExportSelectedRowsOnly = true;
            //settings.SettingsExport.FileName = "Report.pdf";
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.Landscape = true;

            settings.SettingsExport.PageHeader.Center = "Powered by dostah";
            settings.SettingsExport.ReportHeader = "Vatrositemi";



            //settings.SettingsExport.PrintSelectCheckBox = false;

            settings.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;


            //settings.SettingsBehavior.AllowSelectByRowClick = true;

            settings.CommandColumn.SelectAllCheckboxMode = GridViewSelectAllCheckBoxMode.Page;

            settings.Settings.ShowFilterRowMenu = true;
            settings.CommandColumn.ShowClearFilterButton = true;
            settings.SettingsPager.EnableAdaptivity = true;

            //settings.SettingsPager.Visible = true;
            //settings.SettingsPager.Position = System.Web.UI.WebControls.PagerPosition.TopAndBottom;
            //settings.SettingsPager.PageSizeItemSettings.ShowAllItem = true;
            //settings.SettingsPager.PageSizeItemSettings.Visible = true;
            //settings.SettingsPager.FirstPageButton.Visible = true;
            //settings.SettingsPager.LastPageButton.Visible = true;
            //settings.SettingsPager.AllButton.Visible = true;
            //settings.SettingsPager.AlwaysShowPager = true;

            //settings.Settings.ShowGroupPanel = true;

            //settings.CommandColumn.ButtonRenderMode = GridCommandButtonRenderMode.Image;

            settings.CommandColumn.ShowNewButton = false;
            settings.CommandColumn.ShowDeleteButton = true;
            settings.CommandColumn.ShowEditButton = false;

            //settings.SettingsCommandButton.DeleteButton.Image.Url = "~/Content/img/delete.png";
            //settings.SettingsCommandButton.DeleteButton.RenderMode = GridCommandButtonRenderMode.Image;

            //settings.SettingsCommandButton.EditButton.Image.Url = "~/Content/img/edit.png";
            //settings.SettingsCommandButton.EditButton.RenderMode = GridCommandButtonRenderMode.Image;

            //settings.SettingsEditing.AddNewRowRouteValues = new { Controller = "Hidrants", Action = "HidrantAddNew" };
            //settings.SettingsEditing.UpdateRowRouteValues = new { Controller = "Hidrants", Action = "HidrantUpdate" };
            //settings.SettingsEditing.DeleteRowRouteValues = new { Controller = "Hidrants", Action = "HidrantDelete" };
            //settings.SettingsEditing.Mode = GridViewEditingMode.EditFormAndDisplayRow;

            //ViewContext viewContext = new ViewContext();

            //settings.Columns.Add(column => {
            //    column.Caption = "C";
            //    column.SetDataItemTemplateContent(c=> 
            //    {
            //        //viewContext.Writer.Write(Html.ActionLink("Edit", "ExternalEditFormEdit", new { ProductID = DataBinder.Eval(c.DataItem, "ProductID") }));

            //    });

            //});

            //settings.Columns.Add(column => {
            //    column.Caption = "C";
            //    column.SetDataItemTemplateContent(c =>
            //    {
            //        ViewContext.Writer.Write(
            //            Html.ActionLink("Edit", "ExternalEditFormEdit", new { ProductID = DataBinder.Eval(c.DataItem, "ProductID") }) + "&nbsp;"+
            //            Html.ActionLink("Delete", "ExternalEditFormDelete", new { ProductID = DataBinder.Eval(c.DataItem, "ProductID") },
            //                new { onclick = "return confirm('Do you really want to delete this record?')" })
            //        );
            //    });
            //    column.SetHeaderTemplateContent(c =>
            //    {
            //        ViewContext.Writer.Write(
            //            Html.ActionLink("New", "ExternalEditFormEdit", new { ProductID = -1 }).ToHtmlString()
            //        );
            //    });
            //    column.Settings.AllowDragDrop = DevExpress.Utils.DefaultBoolean.False;
            //    column.Settings.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            //    column.Width = 70;
            //});;

            //settings.Columns.Add(column => {
            //    column.FieldName = "Unbound";
            //    column.Caption = "Button";
            //    column.UnboundType = DevExpress.Data.UnboundColumnType.String;
            //    column.EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.False;
            //    column.Width = System.Web.UI.WebControls.Unit.Percentage(10);
            //    column.SetDataItemTemplateContent(c =>
            //    {

            //        htmlHelper.DevExpress().Button(b =>
            //        {
            //            b.Name = "Button" + c.KeyValue;
            //            b.Text = "Test";
            //            b.ClientSideEvents.Click = string.Format(@"function(s, e) {{ alert({0}); }}", c.KeyValue);
            //        }).GetHtml();
            //    });

            //});

            return settings;
        }

        // GET: Hidrants/Create
        public ActionResult Create()
        {
            //ViewBag.HidrantTipId = new SelectList(db.HidrantTips, "HidrantTipId", "Naziv");
            //ViewBag.InstalacijaId = new SelectList(db.Instalacijas, "InstalacijaId", "Naziv");
            //ViewBag.KompletnostId = new SelectList(db.Kompletnosts, "KompletnostId", "Naziv");
            //ViewBag.LokacijaId = new SelectList(db.Lokacijas, "LokacijaId", "Naziv");
            //ViewBag.PromjerMlazniceId = new SelectList(db.PromjerMlaznices, "PromjerMlazniceId", "PromjerMlazniceId");
            return View();
        }

        // POST: Hidrants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HidrantId,Oznaka,InstalacijaId,HidrantTipId,HidrostatickiPritisak,HidrodinamickiPritisak,Protok,KompletnostId,LokacijaId,PromjerMlazniceId")] Hidrant hidrant)
        {
            //if (ModelState.IsValid)
            //{
            //    DataProvider.DB.Hidrants.Add(hidrant);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //ViewBag.HidrantTipId = new SelectList(db.HidrantTips, "HidrantTipId", "Naziv", hidrant.HidrantTipId);
            //ViewBag.InstalacijaId = new SelectList(db.Instalacijas, "InstalacijaId", "Naziv", hidrant.InstalacijaId);
            //ViewBag.KompletnostId = new SelectList(db.Kompletnosts, "KompletnostId", "Naziv", hidrant.KompletnostId);
            //ViewBag.LokacijaId = new SelectList(db.Lokacijas, "LokacijaId", "Naziv", hidrant.LokacijaId);
            //ViewBag.PromjerMlazniceId = new SelectList(db.PromjerMlaznices, "PromjerMlazniceId", "PromjerMlazniceId", hidrant.PromjerMlazniceId);
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
