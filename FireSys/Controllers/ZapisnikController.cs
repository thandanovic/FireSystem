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
using FireSys.Attributes;

namespace FireSys.Controllers
{
    [AuthorizeRoles("user")]
    public class ZapisnikController : BaseController
    {
        #region Fields

        private FireSysModel db = new FireSysModel();

        #endregion Fields

        #region Record CRUD

        public ActionResult Index()
        {
            Session["ZapisnikModel"] = GetZapisniks();
            return View(Session["ZapisnikModel"]);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ZapisnikId,BrojZapisnika,BrojZapisnikaGodina,DatumKreiranja,RadniNalogId,Validan,DatumBrisanja,BrojZapisnikaMjesec,DatumZapisnika,ZapisnikTipId,LokacijaId,PregledIzvrsioId,Napomena,KorisnikKreiraoId,Fakturisano,KorisnikKontrolisaoId,BrojFakture,DokumentacijaPrisutna,LokacijaHidranata,HidrantPrikljucenNa,IzRadnogNalogaId,KreiraniRadniNalogId")] Zapisnik zapisnik)
        {
            if (ModelState.IsValid)
            {
                zapisnik.DatumKreiranja = DateTime.Now;
                zapisnik.StatusId = 4;
                db.Zapisniks.Add(zapisnik);
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = zapisnik.ZapisnikId });
            }

            return View(zapisnik);
        }

        [HttpGet]
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

            if(zapisnik.ZapisnikTipId == 1)
            {
                var jsonLokacije = JsonConvert.SerializeObject(db.Lokacijas.Where(x => x.KlijentId == zapisnik.Lokacija.KlijentId).ToList());
                var jsonTip = JsonConvert.SerializeObject(DataProvider.GetTipAparata());
                var jsonIspravnost = JsonConvert.SerializeObject(DataProvider.GetIspravnost());
                var jsonVrsta = JsonConvert.SerializeObject(DataProvider.GetVrstaAparata());

                ViewBag.Lokacije = jsonLokacije;
                ViewBag.Tipovi = jsonTip;
                ViewBag.Ispravnosti = jsonIspravnost;
                ViewBag.Vrste = jsonVrsta;
            }
            else
            {
                var jsonInstalacije = JsonConvert.SerializeObject(DataProvider.GetInstallations());
                var jsonKompletnost = JsonConvert.SerializeObject(DataProvider.GetKompletnost());
                var jsonTip = JsonConvert.SerializeObject(DataProvider.GetHidrantTypes());
                var jsonPromjerMlaznice = JsonConvert.SerializeObject(DataProvider.GetPromjerMlaznice());

                ViewBag.Instalacije = jsonInstalacije;
                ViewBag.Kompletnost = jsonKompletnost;
                ViewBag.HidrantTip = jsonTip;
                ViewBag.PromjerMlaznice = jsonPromjerMlaznice;
            }

            ZapisnikViewModel model = new ZapisnikViewModel();
            model.Zapisnik = zapisnik;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ZapisnikId,BrojZapisnika,BrojZapisnikaGodina,DatumKreiranja,RadniNalogId,Validan,DatumBrisanja,BrojZapisnikaMjesec,DatumZapisnika,ZapisnikTipId,LokacijaId,PregledIzvrsioId,Napomena,KorisnikKreiraoId,Fakturisano,KorisnikKontrolisaoId,BrojFakture,DokumentacijaPrisutna,LokacijaHidranata,HidrantPrikljucenNa,IzRadnogNalogaId,KreiraniRadniNalogId")] Zapisnik zapisnik)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zapisnik).State = EntityState.Modified;
                db.SaveChanges();
            }

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

        [HttpGet]
        public ContentResult DeleteConfirmed(int id)
        {
            try
            {
                Zapisnik zapisnik = db.Zapisniks.Find(id);

                db.ZapisnikAparats.RemoveRange(zapisnik.ZapisnikAparats);
                db.ZapisnikHidrants.RemoveRange(zapisnik.ZapisnikHidrants);

                db.Zapisniks.Remove(zapisnik);
                db.SaveChanges();

                return new ContentResult() { Content = Url.Action("Index", "Zapisnik") };
            }
            catch (Exception ex)
            {
                log.Error("", ex);
                return new ContentResult() { Content = ex.Message };
            }
        }

        #endregion Record CRUD

        #region Business action methods

        [HttpPost]
        public JsonResult Report(int id)
        {
            ZapisnikAparatReport report = new ZapisnikAparatReport();

            Zapisnik zapisnik = db.Zapisniks.Find(id);

            bool ispravan = true;

            foreach (var aparat in zapisnik.ZapisnikAparats)
            {
                if (aparat.IspravnostId != 1)
                {
                    ispravan = false;
                    break;
                }
            }

            Korisnik pregledao = DataProvider.DB.Korisniks.Find(zapisnik.PregledIzvrsioId);
            Korisnik kreirao = DataProvider.DB.Korisniks.Find(zapisnik.KorisnikKreiraoId);

            report.Parameters["BrojZapisnika"].Value = zapisnik.FullBrojZapisnika;
            report.Parameters["Kreirao"].Value = kreirao != null ? kreirao.Prezime + " " + kreirao.Ime : string.Empty;
            report.Parameters["PregledIzvrsen"].Value = zapisnik.DatumZapisnika.ToString("dd.MM.yyyy");
            report.Parameters["PregledIzvrsio"].Value = pregledao != null ? pregledao.Prezime + " " + pregledao.Ime : string.Empty;
            report.Parameters["Status"].Value = ispravan ? "ISPRAVNI" : "NEISPRAVNI";
            report.Parameters["VrijediDo"].Value = zapisnik.DatumZapisnika.AddMonths(6).ToString("dd.MM.yyyy");
            report.Parameters["ID"].Value = id;

            report.RequestParameters = false;
            report.CreateDocument();

            return Json(new
            {
                content = GridViewHelper.RenderRazorViewToString(this, "~/Views/Zapisnik/Reports/ZapisnikReport.cshtml", report)
            });
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
            List<Zapisnik> zapisnici = db.Zapisniks.Where(x => ids.Contains(x.ZapisnikId)).ToList();

            foreach (var zapisnik in zapisnici)
            {
                zapisnik.StatusId = status;
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
                    message = "ERROR"
                });
            }

            return Json(new
            {
                message = "OK"
            });
        }

        [HttpPost]
        public JsonResult CreateWorkOrder(int? zapisnikID)
        {
            if (zapisnikID == null)
            {
                throw new ClientException("Zapisnik ID nedostaje.");
            }

            Zapisnik zapisnik = db.Zapisniks.Find(zapisnikID);

            RadniNalog rn = new RadniNalog();

            rn.BrojNalogaGodina = zapisnik.BrojZapisnikaGodina;
            rn.BrojNalogaMjesec = zapisnik.BrojZapisnikaMjesec;
            rn.BrojNaloga = db.RadniNalogs.Max(x => x.BrojNaloga) + 1;
            rn.DatumKreiranja = DateTime.Now;
            rn.DatumNaloga = zapisnik.DatumZapisnika;
            rn.BrojAparata = zapisnik.ZapisnikAparats.Count;
            rn.BrojHidranata = zapisnik.ZapisnikHidrants.Count;
            rn.LokacijaId = zapisnik.LokacijaId;
            rn.StatusId = 4;

            db.RadniNalogs.Add(rn);
            db.SaveChanges();
            zapisnik.IzRadnogNalogaId = rn.RadniNalogId;
            db.SaveChanges();

            return Json(new
            {
                message = "Radni nalog uspjesno je kreiran"
            });
        }

        #endregion Business action methods

        #region Record Extinguishers

        /// <summary>
        /// Action method for the fetching all extinguishers related to the record
        /// </summary>
        /// <param name="zapisnikId"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetZapisnikAparati(int zapisnikId)
        {
            Zapisnik zapisnik = db.Zapisniks.Find(zapisnikId);
            List<ZapisnikAparatParticle> aparati = new List<ZapisnikAparatParticle>();

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

        [HttpGet]
        public JsonResult GetExtinguishers(int locationID, int zapisnikID, string vrsta, string ispravnost, string tip, string brojAparata, string brojKartice, string napomena, int godinaProizvodnje, int vrijediDo)
        {
            Zapisnik zapisnik = db.Zapisniks.Where(x => (x.LokacijaId == locationID && x.ZapisnikId != zapisnikID)).OrderByDescending(x => x.DatumKreiranja).FirstOrDefault();

            Zapisnik tmpZapisnik = db.Zapisniks.Find(zapisnikID);

            if (zapisnik == null)
            {
                return Json(new { error = "Zapisnik sa datom lokacijom nikada nije generisan." }, JsonRequestBehavior.AllowGet);
            }

            List<string> extinguisherIDs = new List<string>();

            if (tmpZapisnik != null && tmpZapisnik.ZapisnikAparats.Count() > 0)
            {

                foreach (var extinguisher in tmpZapisnik.ZapisnikAparats)
                {
                    extinguisherIDs.Add(extinguisher.VatrogasniAparat.BrojaAparata);
                }
            }

            List<ZapisnikAparatParticle> aparati = new List<ZapisnikAparatParticle>();


            foreach (var aparat in zapisnik.ZapisnikAparats)
            {
                if (!string.IsNullOrEmpty(vrsta) && !aparat.VatrogasniAparat.VatrogasniAparatVrsta.Naziv.Contains(vrsta))
                    continue;

                if (!string.IsNullOrEmpty(ispravnost) && !aparat.VatrogasniAparat.Ispravnost.Naziv.Contains(ispravnost))
                    continue;

                if (!string.IsNullOrEmpty(tip) && !aparat.VatrogasniAparat.VatrogasniAparatTip.Naziv.Contains(tip))
                    continue;

                if (!string.IsNullOrEmpty(brojAparata) && !aparat.VatrogasniAparat.BrojaAparata.Contains(brojAparata.ToString()))
                    continue;

                if (!string.IsNullOrEmpty(brojKartice) && !aparat.VatrogasniAparat.EvidencijskaKartica.BrojEvidencijskeKartice.Contains(brojKartice))
                    continue;

                if (!string.IsNullOrEmpty(napomena) && !aparat.VatrogasniAparat.Napomena.Contains(napomena))
                    continue;

                if (godinaProizvodnje > 0 && aparat.VatrogasniAparat.GodinaProizvodnje != godinaProizvodnje)
                    continue;

                if (vrijediDo > 0 && aparat.VatrogasniAparat.IspitivanjeVrijediDo != vrijediDo)
                    continue;

                if (!extinguisherIDs.Contains(aparat.VatrogasniAparat.BrojaAparata))
                {
                    aparati.Add(Helper.RenderExtendedZapisnikAparat(aparat));
                }
            }

            var jsonAparati = JsonConvert.SerializeObject(aparati);

            return Json(new
            {
                streamAparati = jsonAparati
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion Record Extinguishers

        #region Record Standpipe

        [HttpGet]
        public JsonResult GetZapisnikHidranti(int zapisnikId)
        {
            Zapisnik zapisnik = db.Zapisniks.Find(zapisnikId);
            List<ZapisnikHidrantParticle> hidranti = new List<ZapisnikHidrantParticle>();

            foreach (var hidrant in zapisnik.ZapisnikHidrants)
            {
                hidranti.Add(Helper.RenderZapisnikHidrant(hidrant));
            }

            var jsonHidranti = JsonConvert.SerializeObject(hidranti);

            return Json(new
            {
                hidranti = jsonHidranti
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult InsertZapisnikHidrant(ZapisnikHidrantParticle hidrant)
        {
            if (!hidrant.Validate())
            {
                throw new ClientException(hidrant.ErrorMessage);
            }

            ZapisnikHidrant zapisnikHidrant = new ZapisnikHidrant();

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    //At this point we need to programmatically insert new standpipe
                    //due to later use
                    Hidrant newHidrant = new Hidrant()
                    {
                        Oznaka = hidrant.Oznaka,
                        InstalacijaId = hidrant.InstalacijaId,
                        HidrantTipId = hidrant.HidrantTipId,
                        HidrostatickiPritisak = Helper.Decimal2String(hidrant.HidrostatickiPritisak),
                        HidrodinamickiPritisak = Helper.Decimal2String(hidrant.HidrodinamickiPritisak),
                        Protok = hidrant.Protok,
                        KompletnostId = hidrant.KompletnostId,
                        LokacijaId = hidrant.LokacijaId,
                        PromjerMlazniceId = hidrant.PromjerMlazniceId
                    };

                    db.Entry(newHidrant).State = EntityState.Added;
                    db.SaveChanges();

                    zapisnikHidrant.HidrantId = newHidrant.HidrantId;
                    zapisnikHidrant.ZapisnikId = hidrant.ZapisnikId;
                    zapisnikHidrant.KompletnostId = hidrant.KompletnostId;
                    zapisnikHidrant.Valid = false;
                    zapisnikHidrant.DatumKreiranja = DateTime.Now;
                    zapisnikHidrant.Komentar = hidrant.Komentar;

                    db.Entry(zapisnikHidrant).State = EntityState.Added;
                    db.SaveChanges();

                    transaction.Commit();

                    hidrant = Helper.RenderZapisnikHidrant(zapisnikHidrant);

                    return Json(new
                    {
                        item = JsonConvert.SerializeObject(hidrant)
                    }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message);
                    return Json(new { error = Constants.GenericErrorMessage }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpGet]
        public JsonResult UpdateZapisnikHidrant(ZapisnikHidrantParticle hidrant)
        {
            ZapisnikHidrant zapisnikHidrant = db.ZapisnikHidrants.Find(hidrant.ZapisnikHidrantId);

            if (zapisnikHidrant != null)
            {
                try
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        zapisnikHidrant.KompletnostId = hidrant.KompletnostId;
                        zapisnikHidrant.Komentar = hidrant.Komentar;
                        zapisnikHidrant.Mikrolokacija = hidrant.Mikrolokacija;
                        db.Entry(zapisnikHidrant).State = EntityState.Modified;
                        db.SaveChanges();

                        Hidrant trenutniHidrant = db.Hidrants.Find(zapisnikHidrant.HidrantId);

                        if (trenutniHidrant != null)
                        {
                            trenutniHidrant.Oznaka = hidrant.Oznaka;
                            trenutniHidrant.InstalacijaId = hidrant.InstalacijaId;
                            trenutniHidrant.HidrantTipId = hidrant.HidrantTipId;
                            trenutniHidrant.HidrodinamickiPritisak = Helper.Decimal2String(hidrant.HidrodinamickiPritisak);
                            trenutniHidrant.HidrostatickiPritisak = Helper.Decimal2String(hidrant.HidrostatickiPritisak);
                            trenutniHidrant.Protok = hidrant.Protok;
                            trenutniHidrant.KompletnostId = hidrant.KompletnostId;
                            trenutniHidrant.LokacijaId = hidrant.LokacijaId;
                            trenutniHidrant.PromjerMlazniceId = hidrant.PromjerMlazniceId;

                            db.Entry(trenutniHidrant).State = EntityState.Modified;
                            db.SaveChanges();
                        }

                        transaction.Commit();

                        hidrant = Helper.RenderZapisnikHidrant(zapisnikHidrant);

                        return Json(new
                        {
                            item = JsonConvert.SerializeObject(hidrant)
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message);
                    return Json(new { error = Constants.GenericErrorMessage }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { error = Constants.GenericErrorMessage }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteZapisnikHidrant(ZapisnikHidrantParticle hidrant)
        {
            ZapisnikHidrant zapisnikHidrant = db.ZapisnikHidrants.Find(hidrant.ZapisnikHidrantId);
            var message = string.Empty;

            if (zapisnikHidrant != null)
            {
                db.Entry(zapisnikHidrant.Hidrant).State = EntityState.Deleted;

                db.ZapisnikHidrants.Remove(zapisnikHidrant);
                db.Entry(zapisnikHidrant).State = EntityState.Deleted;
                db.SaveChanges();
            }

            return Json(new
            {
                item = message
            });
        }

        #endregion Record Standpipe

        #region Helpers

        public IEnumerable GetZapisniks()
        {
            FireSysModel DB = new FireSysModel();
            return DB.Zapisniks.Include(z => z.Lokacija).Include(x => x.Status).ToList();
        }

        public ActionResult ZapisnikGridView()
        {
            Session["ZapisnikModel"] = GetZapisniks();
            return PartialView("ZapisnikGridView", Session["ZapisnikModel"]);
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion Helpers
    }
}
