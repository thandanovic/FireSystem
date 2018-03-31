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
using FireSys.Manager;
using Microsoft.AspNet.Identity;
using System.Collections;
using DevExpress.Web.Mvc;
using DevExpress.Web;
using FireSys.Helpers;
using FireSys.Attributes;

namespace FireSys.Controllers
{
    [AuthorizeRoles("user")]
    public class KlijentiController : BaseController
    {
        private FireSysModel db = new FireSysModel();
        private KlijentManager klijentManager = new KlijentManager();

        public ActionResult Index()
        {
            Session["KlijentModel"] = GetClients();
            return View(Session["KlijentModel"]);
        }

        public ActionResult ClientGridView()
        {
            Session["KlijentModel"] = GetClients();
            return PartialView("ClientGridView", Session["KlijentModel"]);
        }

        public IEnumerable GetClients()
        {
            FireSysModel DB = new FireSysModel();

            List<Klijent> klijenti = DB.Klijents.ToList();
            
            foreach(var klijent in klijenti)
            {
                if (ContextElement.Instance.Korisnici.ContainsKey(klijent.KorisnikKreiraoId))
                {
                    klijent.KorisnikKreiraoId = ContextElement.Instance.Korisnici[klijent.KorisnikKreiraoId].Prezime + " " + ContextElement.Instance.Korisnici[klijent.KorisnikKreiraoId].Ime;
                }
            }
            
            return klijenti;
        }

        public ActionResult ExportTo(string OutputFormat)
        {
            var model = Session["KlijentModel"];

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

            settings.Name = "klijentGrid";
            settings.CallbackRouteValues = new { Controller = "Klijenti", Action = "ClientGridView" };
            settings.Width = System.Web.UI.WebControls.Unit.Percentage(100);

            settings.KeyFieldName = "KlijentId";

            settings.Settings.ShowFilterRow = true;

            settings.CommandColumn.Visible = true;
            settings.CommandColumn.ShowSelectCheckbox = true;

            settings.SettingsExport.ExportSelectedRowsOnly = true;
            settings.SettingsPager.Mode = GridViewPagerMode.ShowPager;
            settings.SettingsPager.PageSize = 100;

            settings.Columns.Add(column =>
            {
                column.FieldName = "KlijentId";
                column.Caption = "ID";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Naziv";
                column.Caption = "Naziv";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Kontakt";
                column.Caption = "Kontakt";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "DatumKreiranja";
                column.Caption = "Datum kreiranja";
                column.PropertiesEdit.DisplayFormatString = "dd.MM.yyyy";

            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Obrisano";
                column.Caption = "Obrisano";
                column.ColumnType = MVCxGridViewColumnType.CheckBox;

            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "DatumBrisanja";
                column.Caption = "Datum brisanja";
                column.PropertiesEdit.DisplayFormatString = "dd.MM.yyyy";

            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "KorisnikKreiraoId";
                column.Caption = "Kreirao";
            });

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.Landscape = true;

            settings.SettingsExport.PageHeader.Center = "Powered by dostah";
            settings.SettingsExport.ReportHeader = "Vatrositemi - Klijenti";

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

        // GET: Klijenti/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klijent klijent = db.Klijents.Find(id);
            if (klijent == null)
            {
                return HttpNotFound();
            }
            return View(klijent);
        }

        // GET: Klijenti/Create
        public ActionResult Create()
        {
            Klijent k = new Klijent();
            k.KorisnikKreiraoId = User.Identity.GetUserId();
            return View(k);
        }

        // POST: Klijenti/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Naziv,Kontakt,KorisnikKreiraoId")] Klijent klijent)
        {
            if (ModelState.IsValid)
            {
                klijentManager.Add(klijent);
                return RedirectToAction("Index");
            }

            return View(klijent);
        }

        // GET: Klijenti/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klijent klijent = db.Klijents.Find(id);
            if (klijent == null)
            {
                return HttpNotFound();
            }
            return View(klijent);
        }

        // POST: Klijenti/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KlijentId,Naziv,Kontakt,DatumKreiranja,Obrisano,DatumBrisanja,KorisnikKreiraoId")] Klijent klijent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(klijent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(klijent);
        }

        public ActionResult Delete(int id)
        {
            try
            {
                Klijent klijent = DataProvider.DB.Klijents.Find(id);
                DataProvider.DB.Klijents.Remove(klijent);
                int i = DataProvider.DB.SaveChanges();
                return Json("Success");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return RedirectToAction("Index");
            }
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
