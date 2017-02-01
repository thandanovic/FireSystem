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
using FireSys.Manager;
using FireSys.Models;
using Microsoft.AspNet.Identity;
using System.Data.SqlClient;
using FireSys.Attributes;

namespace FireSys.Controllers
{
    [AuthorizeRoles("user")]
    public class RadniNalogController : BaseController
    {
        private FireSysModel db = new FireSysModel();

        private RadniNalogManager radniNalogManager = new RadniNalogManager();
        private KlijentManager klijentiManager = new KlijentManager();
        private LokacijaManager lokacijaManager = new LokacijaManager();

        // GET: RadniNalog
        public ActionResult Index()
        {            
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


        public ActionResult RadniNalogDolazeci()
        {
            Session["WorkingSheetModel"] = DolazeciGetSheets();
            return View(Session["WorkingSheetModel"]);
        }

        public ActionResult RadniNalogDolazeciGridView()
        {
            Session["WorkingSheetModel"] = DolazeciGetSheets();
            return PartialView("RadniNalogDolazeciGridView", Session["WorkingSheetModel"]);
        }

        public IEnumerable DolazeciGetSheets()
        {
            var datumOd = new SqlParameter("@DatumOd", DateTime.Now.AddMonths(-24));
            var datumDo = new SqlParameter("@DatumDo", DateTime.Now.AddMonths(24));
            var result = db.Database
                .SqlQuery<RadniNalogDolazeci>("spGetDolazeceNaloge @DatumOd, @DatumDo", datumOd, datumDo)
                .ToList();
            return result;
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
            RadniNalogViewModel model = new RadniNalogViewModel();
            ViewBag.LokacijaId = new SelectList(db.Lokacijas, "LokacijaId", "Naziv");            
            return View(model);
        }

        // POST: RadniNalog/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RadniNalogId,LokacijaId,DatumNaloga,KorisnikKreiraiId, RegijaId, BrojNaloga,SelectedKlijentId, NoviKlijentNaziv, NovaLokacijaNaziv,NovaLokacijaVrstaId, NovaLokacijaAdresa, NovaLokacijaKomentar, BrojNalogaMjesec,BrojNalogaGodina,DatumKreiranja,Aparati,Hidranti,Komentar,BrojHidranata,BrojAparata,Narucilac")] RadniNalogViewModel radniNalog)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    if (!String.IsNullOrEmpty(radniNalog.NoviKlijentNaziv)){
                        Klijent klijent = new Klijent();
                        klijent.Naziv = radniNalog.NoviKlijentNaziv;
                        klijent.KorisnikKreiraoId = User.Identity.GetUserId();
                        klijentiManager.Add(klijent);

                        Lokacija lokacija = new Lokacija();
                        lokacija.KlijentId = klijent.KlijentId;
                        lokacija.RegijaId = radniNalog.RegijaId;
                        lokacija.KorisnikKreiraoId = User.Identity.GetUserId();
                        lokacija.Naziv = radniNalog.NovaLokacijaNaziv;
                        lokacija.Komentar = radniNalog.NovaLokacijaKomentar;
                        lokacija.LokacijaVrstaId = radniNalog.NovaLokacijaVrstaId;
                        lokacija.Adresa = radniNalog.NovaLokacijaAdresa;
                        lokacijaManager.Add(lokacija);
                        radniNalog.LokacijaId = lokacija.LokacijaId;

                    }
                    else if (!String.IsNullOrEmpty(radniNalog.NovaLokacijaNaziv)){
                        Lokacija lokacija = new Lokacija();
                        lokacija.KlijentId = radniNalog.SelectedKlijentId.Value;
                        lokacija.RegijaId = radniNalog.RegijaId;
                        lokacija.KorisnikKreiraoId = User.Identity.GetUserId();
                        lokacija.Naziv = radniNalog.NovaLokacijaNaziv;
                        lokacija.Komentar = radniNalog.NovaLokacijaKomentar;
                        lokacija.LokacijaVrstaId = radniNalog.NovaLokacijaVrstaId;
                        lokacija.Adresa = radniNalog.NovaLokacijaAdresa;
                        int lokacijaId = lokacijaManager.Add(lokacija);
                        radniNalog.LokacijaId = lokacija.LokacijaId;
                    }
                    
                    radniNalogManager.Add(radniNalog.GetRadniNalog());
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = Common.Constants.GenericErrorMessage;
                //log error to some logger
                log.Error(Common.Constants.GenericErrorMessage, ex);
            }

            ViewBag.LokacijaId = new SelectList(db.Lokacijas, "LokacijaId", "Naziv", radniNalog.LokacijaId);
            radniNalog.Klijenti = new SelectList(db.Klijents, "KlijentId", "Naziv");
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
            RadniNalogViewModel radniNalogView = new RadniNalogViewModel(radniNalog);
            if (radniNalog == null)
            {
                return HttpNotFound();
            }
            return View(radniNalogView);
        }

        // POST: RadniNalog/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RadniNalogId,LokacijaId,DatumNaloga,KorisnikKreiraiId,BrojNaloga,BrojNalogaMjesec,BrojNalogaGodina,DatumKreiranja,Aparati,Hidranti,Komentar,BrojHidranata,BrojAparata,Narucilac")] RadniNalogViewModel radniNalog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(radniNalog.GetRadniNalog()).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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

        public ActionResult GetDetailsPartial(int? id)
        {
            RadniNalogViewModel model = new RadniNalogViewModel();
            if (id != null)
            {
                RadniNalog radniNalog = db.RadniNalogs.Find(id);
                model.FillViewModel(radniNalog);
            }          
            return PartialView("_Details", model);
        }

        [HttpPost]
        public JsonResult ChangeStatus(string IDs, int status)
        {

            if (string.IsNullOrEmpty(IDs))
            {
                return Json(new
                {
                    message = "ERROR"
                });
            }

            List<int> ids = IDs.Split(',').ToList().Select(int.Parse).ToList();
            List<RadniNalog> nalozi = db.RadniNalogs.Where(x => ids.Contains(x.RadniNalogId)).ToList();

            foreach (var nalog in nalozi)
            {
                nalog.StatusId = status;
                db.Entry(nalog).State = EntityState.Modified;
            }

            try
            {
                db.SaveChanges();
            }
            catch
            {
                return Json(new
                {
                    message = "ERROR"
                });
            }

            return Json(new
            {
                message = "OK"
            });
        }

        [HttpPost]
        public JsonResult PoveziRadniNalog(string IDs)
        {
            int? lokacija = 0;
            bool zapisnikHidrant = false ;
            bool zapisnikAparat = false ;

            int brojHidranta=0;
            int brojAparata=0;

            if (string.IsNullOrEmpty(IDs))
            {
                return Json(new
                {
                    message = "ERROR"
                });
            }

            List<int> ids = IDs.Split(',').ToList().Select(int.Parse).ToList();
            List<Zapisnik> zapisnici = db.Zapisniks.Where(x => ids.Contains(x.ZapisnikId)).ToList();

            foreach (var zapisnik in zapisnici)
            {
                if (lokacija == 0)
                {
                    lokacija = zapisnik.LokacijaId;
                }
                else if (lokacija != zapisnik.LokacijaId)
                {        
                    return Json(new
                    {
                        message = "ERROR",
                        errorMessage = "Nalog se može kreirati za samo jednu lokaciju."
                    });
                }

                if (zapisnik.ZapisnikHidrants.Count > 0)
                {
                    zapisnikHidrant = true;
                    brojHidranta += zapisnik.ZapisnikHidrants.Count;
                }

                if (zapisnik.ZapisnikAparats.Count > 0)
                {
                    zapisnikAparat = true;
                    brojAparata += zapisnik.ZapisnikAparats.Count;
                }
            }

            RadniNalog newRadniNalog = new RadniNalog
            {
                LokacijaId = lokacija.Value
            };


            int lastRadniNalogId = db.RadniNalogs.Max(r => r.RadniNalogId);

            RadniNalog radniNalog = db.RadniNalogs.Find(lastRadniNalogId);

            if(radniNalog.BrojNalogaGodina == DateTime.Now.Year)
            {
                newRadniNalog.BrojNaloga = radniNalog.BrojNaloga + 1;
            }
            else
            {
                newRadniNalog.BrojNaloga = 1;
            }

            newRadniNalog.BrojNalogaMjesec = DateTime.Now.Month;
            newRadniNalog.BrojNalogaGodina = DateTime.Now.Year;
            newRadniNalog.DatumKreiranja = DateTime.Now;
            newRadniNalog.DatumNaloga = DateTime.Now;
            newRadniNalog.KorisnikKreiraiId = Guid.Parse(base.User.Identity.GetUserId());
            newRadniNalog.Hidranti = zapisnikHidrant;
            newRadniNalog.Aparati = zapisnikAparat;
            newRadniNalog.BrojHidranata = new int?(brojHidranta);
            newRadniNalog.BrojAparata = new int?(brojAparata);
            newRadniNalog.StatusId = 4;
            db.RadniNalogs.Add(newRadniNalog);
            db.SaveChanges();

            foreach (var zapisnik in zapisnici)
            {
                zapisnik.KreiraniRadniNalogId = newRadniNalog.RadniNalogId;
                db.Entry(zapisnik).State = EntityState.Modified;
            }

            try
            {
                db.SaveChanges();
            }
            catch
            {
                return Json(new
                {
                    message = "ERROR",
                    errorMessage = "Desio se problem pri spasavanju u bazu."
                });
            }

            return Json(new
            {
                message = "OK",
                radniNalogId = newRadniNalog.RadniNalogId.ToString()
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
