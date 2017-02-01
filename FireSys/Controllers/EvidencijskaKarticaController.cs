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
using FireSys.Attributes;
using FireSys.UI.Manager;
using FireSys.Models;

namespace FireSys.Controllers
{
    [AuthorizeRoles("user")]
    public class EvidencijskaKarticaController : BaseController
    {
        private FireSysModel db = new FireSysModel();

        public EvidencijskeKarticeManager EvidendijskaManager = new EvidencijskeKarticeManager();

        public ActionResult Index()
        {
            Session["KarticeModel"] = GetKartice();
            return View(Session["KarticeModel"]);
        }

        public ActionResult EvidencijskaKarticaGridView()
        {
            Session["KarticeModel"] = GetKartice();
            return PartialView("EvidencijskaKarticaGridView", Session["KarticeModel"]);
        }

        public IEnumerable GetKartice()
        {
            FireSysModel DB = new FireSysModel();
            return DB.EvidencijskaKarticas.Include(z => z.EvidencijskaKarticaTip).ToList();
        }

        public ActionResult ExportTo(string OutputFormat)
        {
            var model = Session["KarticeModel"];

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

            settings.Name = "evidencijskaKarticaGrid";
            settings.CallbackRouteValues = new { Controller = "EvidencijskaKartica", Action = "EvidencijskaKarticaGridView" };
            settings.Width = System.Web.UI.WebControls.Unit.Percentage(100);

            settings.KeyFieldName = "EvidencijskaKarticaId";

            settings.Settings.ShowFilterRow = true;

            settings.CommandColumn.Visible = true;
            settings.CommandColumn.ShowSelectCheckbox = true;

            settings.SettingsExport.ExportSelectedRowsOnly = true;
            settings.SettingsPager.Mode = GridViewPagerMode.ShowPager;
            settings.SettingsPager.PageSize = 100;

            settings.Columns.Add(column =>
            {
                column.FieldName = "EvidencijskaKarticaId";
                column.Caption = "ID";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "BrojEvidencijskeKartice";
                column.Caption = "Broj kartice";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "EvidencijskaKarticaTip.EvidencijskaKarticaTipNaziv";
                column.Caption = "Tip";

                column.ColumnType = MVCxGridViewColumnType.ComboBox;
                var comboBoxProperties = column.PropertiesEdit as ComboBoxProperties;
                comboBoxProperties.DataSource = FireSys.Helpers.DataProvider.GetEvidencijskeKarticeTip();
                comboBoxProperties.TextField = "Naziv";
                comboBoxProperties.ValueField = "Naziv";
                comboBoxProperties.ValueType = typeof(string);
                comboBoxProperties.DropDownStyle = DropDownStyle.DropDown;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Validna";
                column.Caption = "Validna";
                column.ColumnType = MVCxGridViewColumnType.CheckBox;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Obrisana";
                column.Caption = "Obrisana";
                column.ColumnType = MVCxGridViewColumnType.CheckBox;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "DatumZaduzenja";
                column.Caption = "Datum zaduzenja";
                column.PropertiesEdit.DisplayFormatString = "dd.MM.yyyy";

            });

            return settings;
        }





        //// GET: EvidencijskaKartica
        //public ActionResult Index()
        //{
        //    var evidencijskaKarticas = db.EvidencijskaKarticas.Include(e => e.EvidencijskaKarticaTip);
        //    return View(evidencijskaKarticas.ToList());
        //}

        // GET: EvidencijskaKartica/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvidencijskaKartica evidencijskaKartica = db.EvidencijskaKarticas.Find(id);
            if (evidencijskaKartica == null)
            {
                return HttpNotFound();
            }
            return View(evidencijskaKartica);
        }

        // GET: EvidencijskaKartica/Create
        public ActionResult Create()
        {
            ViewBag.EvidencijskaKarticaTipId = new SelectList(db.EvidencijskaKarticaTips, "EvidencijskaKarticaTipId", "EvidencijskaKarticaTipNaziv");
            ViewBag.UserId = db.Korisniks
                   .Select(x => new SelectListItem
                   {
                       Value = x.KorisnikId.ToString(),
                       Text = x.Ime + " " + x.Prezime
                   })
                   .ToList();
            return View();
        }

        // POST: EvidencijskaKartica/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EvidencijskaKarticaId,UserId,Rasponod,RasponDo,RemoveKartica,EvidencijskaKarticaTipId,DatumZaduzenja")] EvidencijskaKarticaViewModel evidencijskaKartica)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    EvidendijskaManager.DodajEvidencijskeKartice(evidencijskaKartica);
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                }
            }

            ViewBag.EvidencijskaKarticaTipId = new SelectList(db.EvidencijskaKarticaTips, "EvidencijskaKarticaTipId", "EvidencijskaKarticaTipNaziv", evidencijskaKartica.EvidencijskaKarticaTipId);
            ViewBag.UserId = db.Korisniks
                   .Select(x => new SelectListItem
                   {
                       Value = x.KorisnikId.ToString(),
                       Text = x.Ime + " " + x.Prezime
                   })
                   .ToList();
            return View(evidencijskaKartica);
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateZaduzenje([Bind(Include = "EvidencijskaKarticaId,UserId,Rasponod,RasponDo,RemoveKartica,EvidencijskaKarticaTipId,DatumZaduzenja")] EvidencijskaKarticaViewModel evidencijskaKartica)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    EvidendijskaManager.DodajEvidencijskeKartice(evidencijskaKartica);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                }
            }

            ViewBag.EvidencijskaKarticaTipId = new SelectList(db.EvidencijskaKarticaTips, "EvidencijskaKarticaTipId", "EvidencijskaKarticaTipNaziv", evidencijskaKartica.EvidencijskaKarticaTipId);
            ViewBag.UserId = db.Korisniks
                   .Select(x => new SelectListItem
                   {
                       Value = x.KorisnikId.ToString(),
                       Text = x.Ime + " " + x.Prezime
                   })
                   .ToList();
            return View("Create", evidencijskaKartica);
        }

        // GET: EvidencijskaKartica/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvidencijskaKartica evidencijskaKartica = db.EvidencijskaKarticas.Find(id);
            if (evidencijskaKartica == null)
            {
                return HttpNotFound();
            }
            ViewBag.EvidencijskaKarticaTipId = new SelectList(db.EvidencijskaKarticaTips, "EvidencijskaKarticaTipId", "EvidencijskaKarticaTipNaziv", evidencijskaKartica.EvidencijskaKarticaTipId);
            return View(evidencijskaKartica);
        }

        // POST: EvidencijskaKartica/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EvidencijskaKarticaId,BrojEvidencijskeKartice,UserId,Validna,Obrisana,EvidencijskaKarticaTipId,DatumZaduzenja")] EvidencijskaKartica evidencijskaKartica)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evidencijskaKartica).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EvidencijskaKarticaTipId = new SelectList(db.EvidencijskaKarticaTips, "EvidencijskaKarticaTipId", "EvidencijskaKarticaTipNaziv", evidencijskaKartica.EvidencijskaKarticaTipId);
            return View(evidencijskaKartica);
        }

        // GET: EvidencijskaKartica/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvidencijskaKartica evidencijskaKartica = db.EvidencijskaKarticas.Find(id);
            if (evidencijskaKartica == null)
            {
                return HttpNotFound();
            }

            evidencijskaKartica.Obrisana = true;
            db.Entry(evidencijskaKartica).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: EvidencijskaKartica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EvidencijskaKartica evidencijskaKartica = db.EvidencijskaKarticas.Find(id);
            db.EvidencijskaKarticas.Remove(evidencijskaKartica);
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
