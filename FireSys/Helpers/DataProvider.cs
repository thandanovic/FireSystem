using FireSys.DB;
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
        public static FireSysModel  DB
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
            var query = from lok in DB.Lokacijas
                        select new
                        {
                            LokacijaID = lok.LokacijaId,
                            Naziv = lok.Naziv
                        };
            return query.ToList();
        }
    }
}