using FireSys.DB;
using FireSys.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FireSys.Manager
{
    public static class ZapisnikManager
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

        public static bool Delete(int id)
        {
            try
            {
                Zapisnik zapisnik = DB.Zapisniks.Find(id);

                foreach(ZapisnikAparat aparat in zapisnik.ZapisnikAparats)
                {
                    DB.ZapisnikAparats.Remove(aparat);
                }

                foreach(ZapisnikHidrant hidrant in zapisnik.ZapisnikHidrants)
                {
                    DB.ZapisnikHidrants.Remove(hidrant);
                }

                DB.Zapisniks.Remove(zapisnik);
                DB.SaveChanges();
            }
            catch
            {
                
            }
            return true;
        }
    }
}
