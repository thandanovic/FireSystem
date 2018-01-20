using FireSys.DB;
using FireSys.Entities;
using FireSys.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FireSys.Helpers
{
    public static class DataProvider
    {
        const string DataContextKey = "FireSysModel";
        public static FireSysModel DB
        {
            get
            {
                if (HttpContext.Current.Items[DataContextKey] == null)
                    HttpContext.Current.Items[DataContextKey] = new FireSysModel();
                return (FireSysModel)HttpContext.Current.Items[DataContextKey];
            }
        }

        public static IEnumerable GetHidrantTypes()
        {
            var query = from tip in DB.HidrantTips
                        select new
                        {
                            HidrantTipID = tip.HidrantTipId,
                            Naziv = tip.Naziv
                        };
            return query.ToList();
        }

        public static IEnumerable GetInstallations()
        {
            var query = from instalacija in DB.Instalacijas
                        select new
                        {
                            InstalacijaID = instalacija.InstalacijaId,
                            Naziv = instalacija.Naziv
                        };
            return query.ToList();
        }

        public static IEnumerable GetKompletnost()
        {
            var query = from komp in DB.Kompletnosts
                        select new
                        {
                            KompletnostId = komp.KompletnostId,
                            Naziv = komp.Naziv
                        };
            return query.ToList();
        }

        public static IEnumerable GetLocations()
        {
            var query = (from lok in DB.Lokacijas
                         select new
                         {
                             LokacijaID = lok.LokacijaId,
                             Naziv = lok.Naziv
                         }).OrderBy(x => x.Naziv);
            return query.ToList();
        }

        public static IEnumerable GetEvidencijskeKarticeTip()
        {
            var query = from lok in DB.EvidencijskaKarticaTips
                        select new
                        {
                            EvidencijskaKarticaID = lok.EvidencijskaKarticaTipId,
                            Naziv = lok.EvidencijskaKarticaTipNaziv
                        };
            return query.ToList();
        }

        public static IEnumerable GetMjesta()
        {
            var query = from komp in DB.Mjestoes
                        select new
                        {
                            MjestoId = komp.MjestoId,
                            Naziv = komp.Naziv
                        };
            return query.ToList();
        }

        public static IEnumerable GetRegions()
        {
            var query = (from komp in DB.Regijas
                         select new
                         {
                             RegijaId = komp.RegijaId,
                             Naziv = komp.Naziv
                         }).OrderBy(x => x.Naziv);
            return query.ToList();
        }

        public static IEnumerable GetClients()
        {
            var query = from komp in DB.Klijents
                        orderby komp.Naziv
                        select new
                        {
                            KlijentId = komp.KlijentId,
                            Naziv = komp.Naziv
                        };
            return query.ToList();
        }

        public static IEnumerable GetLokacijaVrsta()
        {
            var query = from komp in DB.LokacijaVrstas
                        select new
                        {
                            LokacijaVrstaId = komp.LokacijaVrstaId,
                            Naziv = komp.Naziv
                        };
            return query.ToList();
        }

        public static IEnumerable GetTipAparata()
        {
            var query = from komp in DB.VatrogasniAparatTips
                        select new
                        {
                            VatrogasniAparatTipId = komp.VatrogasniAparatTipId,
                            Naziv = komp.Naziv
                        };
            return query.ToList();
        }

        public static IEnumerable GetEvidencijskeKartice()
        {
            var query = from komp in DB.EvidencijskaKarticas
                        select new
                        {
                            EvidencijskaKarticaId = komp.EvidencijskaKarticaId,
                            Broj = komp.BrojEvidencijskeKartice
                        };
            return query.ToList();
        }

        public static IEnumerable GetIspravnost()
        {
            var query = from komp in DB.Ispravnosts
                        select new
                        {
                            IspravnostId = komp.IspravnostId,
                            Naziv = komp.Naziv
                        };
            return query.ToList();
        }

        public static IEnumerable GetVrstaAparata()
        {
            var query = from komp in DB.VatrogasniAparatVrstas
                        select new
                        {
                            VatrogasniAparatVrstaId = komp.VatrogasniAparatVrstaId,
                            Naziv = komp.Naziv
                        };
            return query.ToList();
        }

        public static IEnumerable GetZapisnikTip()
        {
            var query = from komp in DB.ZapisnikTips
                        select new
                        {
                            ZapisnikTipId = komp.ZapisnikTipId,
                            Naziv = komp.Naziv
                        };
            return query.ToList();
        }

        public static IEnumerable GetKlijenti()
        {
            var query = from komp in DB.Klijents
                        orderby komp.Naziv
                        select new
                        {
                            KlijentId = komp.KlijentId,
                            Naziv = komp.Naziv
                        };
            return query.ToList();
        }

        public static IEnumerable GetZaposlenici()
        {
            var query = from komp in DB.Korisniks
                        select new
                        {
                            KorisnikId = komp.KorisnikId,
                            Naziv = komp.Prezime + " " + komp.Ime
                        };
            return query.ToList();
        }
    }


    public static class Helper
    {
        public static ZapisnikAparatParticle RenderZapisnikAparat(ZapisnikAparat aparat)
        {
            ZapisnikAparatParticle ap = new ZapisnikAparatParticle();
            ap.ZapisnikAparatId = aparat.ZapisnikAparatId;
            ap.TipId = aparat.VatrogasniAparat.VatrogasniAparatTipId;
            ap.GodinaProizvodnje = aparat.VatrogasniAparat.GodinaProizvodnje.HasValue ? aparat.VatrogasniAparat.GodinaProizvodnje.Value : 0;
            ap.IspravnostId = aparat.IspravnostId;
            ap.Napomena = aparat.VatrogasniAparat.Napomena;
            ap.VrijediDo = aparat.VatrogasniAparat.IspitivanjeVrijediDo.HasValue ? aparat.VatrogasniAparat.IspitivanjeVrijediDo.Value : 0;
            ap.BrojAparata = aparat.VatrogasniAparat.BrojaAparata;
            ap.VrstaId = aparat.VatrogasniAparat.VatrogasniAparatVrstaId;
            ap.BrojKartice = aparat.VatrogasniAparat.EvidencijskaKartica != null ? aparat.VatrogasniAparat.EvidencijskaKartica.BrojEvidencijskeKartice : string.Empty;
            return ap;
        }
    }



    public class ContextElement
    {
        private static ContextElement instance;

        private static FireSysModel model;

        public Dictionary<string, Korisnik> Korisnici;

        private ContextElement()
        {

            Dictionary<string, Korisnik> korisnici = new Dictionary<string, Korisnik>();

            foreach (var element in model.Korisniks)
            {
                if (!korisnici.ContainsKey(element.KorisnikId.ToString()))
                {
                    korisnici.Add(element.KorisnikId.ToString(), element);
                }
            }

            Korisnici = korisnici;
        }

        public static ContextElement Instance
        {
            get
            {
                if (instance == null)
                {
                    model = new FireSysModel();
                    instance = new ContextElement();
                }
                return instance;
            }
        }




    }
}