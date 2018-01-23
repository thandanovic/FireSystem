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
using FireSys.Models;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using FireSys.Common;

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
            //return DB.Zapisniks.Include(z => z.Lokacija).Include(z => z.RadniNalog).Include(z => z.RadniNalog1).Include(z => z.RadniNalog2).Include(z => z.ZapisnikTip).ToList();
            return DB.Zapisniks.Include(z => z.Lokacija).ToList();
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

        [HttpGet]
        public JsonResult Create()
        {
            ZapisnikViewModel model = new ZapisnikViewModel();
            model.Zapisnik = new Zapisnik();
            
            return Json(new
            {
                content = GridViewHelper.RenderRazorViewToString(this, "~/Views/Zapisnik/Create.cshtml", model)
            });
        }

        [HttpGet]
        public JsonResult GetZapisnikAparati(int zapisnikId)
        {
            Zapisnik zapisnik = db.Zapisniks.Find(zapisnikId);

            List<ZapisnikAparatParticle> aparati = new List<ZapisnikAparatParticle>();
            ZapisnikAparatParticle ap = null;

            foreach (var aparat in zapisnik.ZapisnikAparats)
            {
                aparati.Add(Helper.RenderZapisnikAparat(aparat));
            }

            var jsonAparati = JsonConvert.SerializeObject(aparati);

            return Json(new
            {
                streamAparati = jsonAparati
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult InsertZapisnikAparat(ZapisnikAparatParticle aparat)
        {
            if (!aparat.Validate())
            {
                throw new ClientException(aparat.ErrorMessage);
            }

            ZapisnikAparat zapisnikAparat = new ZapisnikAparat();

            if (!string.IsNullOrEmpty(aparat.BrojKartice))
            {
                var kartice = from s in db.EvidencijskaKarticas select s;
                EvidencijskaKartica kartica = kartice.Where(x => x.BrojEvidencijskeKartice == aparat.BrojKartice).FirstOrDefault();

                if (kartica == null)
                {
                    throw new ClientException("Kartica nije zaduzena");
                }

                zapisnikAparat.EvidencijskaKarticaId = kartica.EvidencijskaKarticaId;
            }

            Ispravnost ispravnost = db.Ispravnosts.Find(aparat.IspravnostId);

            if (ispravnost != null)
            {
                zapisnikAparat.IspravnostId = ispravnost.IspravnostId;
            }
            else
            {
                throw new ClientException("Ispravnost nije definisana u bazi, morate je definisati.");
            }


            VatrogasniAparat vatroAparat = new VatrogasniAparat();

            vatroAparat.VatrogasniAparatTipId = aparat.TipId;
            vatroAparat.GodinaProizvodnje = aparat.GodinaProizvodnje;
            vatroAparat.Napomena = aparat.Napomena;
            vatroAparat.IspitivanjeVrijediDo = aparat.VrijediDo;
            vatroAparat.BrojaAparata = aparat.BrojAparata;
            vatroAparat.VatrogasniAparatVrstaId = aparat.VrstaId;
            vatroAparat.DatumKreiranja = DateTime.Now;
            vatroAparat.LokacijaId = aparat.LokacijaId;
            vatroAparat.IspravnostId = ispravnost.IspravnostId;


            db.Entry(vatroAparat).State = EntityState.Added;
            db.SaveChanges();

            zapisnikAparat.VatrogasniAparatId = vatroAparat.VatrogasniAparatId;
            zapisnikAparat.VatrogasniAparat = vatroAparat;
            zapisnikAparat.ZapisnikId = aparat.ZapisnikId;
            zapisnikAparat.Valid = true;
            zapisnikAparat.DatumKreiranja = DateTime.Now;

            db.Entry(zapisnikAparat).State = EntityState.Added;
            db.SaveChanges();

            aparat = Helper.RenderZapisnikAparat(zapisnikAparat);

            return Json(new
            {
                item = JsonConvert.SerializeObject(aparat)
            }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult UpdateZapisnikAparat(ZapisnikAparatParticle aparat)
        {
            ZapisnikAparat zapisnikAparat = db.ZapisnikAparats.Find(aparat.ZapisnikAparatId);

            if (zapisnikAparat != null)
            {
                if (zapisnikAparat.IspravnostId != aparat.IspravnostId)
                {
                    zapisnikAparat.IspravnostId = aparat.IspravnostId;
                    db.Entry(zapisnikAparat).State = EntityState.Modified;
                    db.SaveChanges();
                }

                EvidencijskaKartica kartica = db.EvidencijskaKarticas.Find(zapisnikAparat.VatrogasniAparat.EvidencijskaKarticaId);
                if (kartica != null && kartica.BrojEvidencijskeKartice != aparat.BrojKartice)
                {
                    kartica.BrojEvidencijskeKartice = aparat.BrojKartice;
                    db.Entry(kartica).State = EntityState.Modified;
                    db.SaveChanges();
                }

                VatrogasniAparat vatroAparat = db.VatrogasniAparats.Find(zapisnikAparat.VatrogasniAparatId);

                if (vatroAparat != null)
                {
                    vatroAparat.VatrogasniAparatTipId = aparat.TipId;
                    vatroAparat.GodinaProizvodnje = aparat.GodinaProizvodnje;
                    vatroAparat.Napomena = aparat.Napomena;
                    vatroAparat.IspitivanjeVrijediDo = aparat.VrijediDo;
                    vatroAparat.BrojaAparata = aparat.BrojAparata;
                    vatroAparat.VatrogasniAparatVrstaId = aparat.VrstaId;

                    db.Entry(vatroAparat).State = EntityState.Modified;
                    db.SaveChanges();
                }

                aparat = Helper.RenderZapisnikAparat(zapisnikAparat);
            }

            return Json(new
            {
                item = JsonConvert.SerializeObject(aparat)
            }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult DeleteZapisnikAparat(ZapisnikAparatParticle aparat)
        {
            ZapisnikAparat zapisnikAparat = db.ZapisnikAparats.Find(aparat.ZapisnikAparatId);
            var message = string.Empty;

            if (zapisnikAparat != null)
            {
                //VatrogasniAparat vatroAparat = db.VatrogasniAparats.Find(zapisnikAparat.VatrogasniAparatId);
                db.Entry(zapisnikAparat.VatrogasniAparat).State = EntityState.Deleted;

                db.ZapisnikAparats.Remove(zapisnikAparat);
                db.Entry(zapisnikAparat).State = EntityState.Deleted;
                db.SaveChanges();
            }

            return Json(new
            {
                item = message
            });
        }



        // POST: Zapisnik/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ZapisnikId,BrojZapisnika,BrojZapisnikaGodina,DatumKreiranja,RadniNalogId,Validan,DatumBrisanja,BrojZapisnikaMjesec,DatumZapisnika,ZapisnikTipId,LokacijaId,PregledIzvrsioId,Napomena,KorisnikKreiraoId,Fakturisano,KorisnikKontrolisaoId,BrojFakture,DokumentacijaPrisutna,LokacijaHidranata,HidrantPrikljucenNa,IzRadnogNalogaId,KreiraniRadniNalogId")] Zapisnik zapisnik)
        {
            if (ModelState.IsValid)
            {
                zapisnik.DatumKreiranja = DateTime.Now;
                db.Zapisniks.Add(zapisnik);
                db.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction("Edit", new { id = zapisnik.ZapisnikId });
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

            var jsonLokacije = JsonConvert.SerializeObject(db.Lokacijas.Where(x => x.KlijentId == zapisnik.Lokacija.KlijentId).ToList());
            var jsonTip = JsonConvert.SerializeObject(DataProvider.GetTipAparata());
            var jsonIspravnost = JsonConvert.SerializeObject(DataProvider.GetIspravnost());
            var jsonVrsta = JsonConvert.SerializeObject(DataProvider.GetVrstaAparata());

            ViewBag.Lokacije = jsonLokacije;
            ViewBag.Tipovi = jsonTip;
            ViewBag.Ispravnosti = jsonIspravnost;
            ViewBag.Vrste = jsonVrsta;

            ZapisnikViewModel model = new ZapisnikViewModel();
            model.Zapisnik = zapisnik;
            return View(model);
        }

        // POST: Zapisnik/Edit/5
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

        [HttpPost]
        public JsonResult Report(int id)
        {
            ZapisnikReport report = new ZapisnikReport();

            report.Parameters["BrojZapisnika"].Value = "test";
            report.Parameters["Narucilac"].Value = "test";
            report.Parameters["Lokacija"].Value = "test";
            report.Parameters["Adresa"].Value = "test";
            report.Parameters["Mjesto"].Value = "test";
            report.Parameters["Grad"].Value = "test";

            report.RequestParameters = false;
            report.CreateDocument();
            //report.PrintingSystem.Document.AutoFitToPagesWidth = 1;

            return Json(new
            {
                content = GridViewHelper.RenderRazorViewToString(this, "~/Views/Zapisnik/Reports/ZapisnikReport.cshtml", report)
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
