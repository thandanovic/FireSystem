using FireSys.Entities;
using FireSys.Helpers;
using FireSys.Manager;
using FireSys.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FireSys.UI.Manager
{
    public class EvidencijskeKarticeManager
    {

        public void DodajEvidencijskeKartice(EvidencijskaKarticaViewModel evidencijska)
        {
            string validanRaspon = ValidanRasponKartica(evidencijska.RasponOd, evidencijska.RasponDo);
            if(!String.IsNullOrEmpty(validanRaspon))
                throw new Exception("Postoje već kreirane evidencijske kartice u tom rasponu sa brojevima:" + validanRaspon);

            for (int i = evidencijska.RasponOd; i<= evidencijska.RasponDo; i++)
            {
                EvidencijskaKartica kartica = evidencijska.GetEvidencijskaKartica();
                kartica.BrojEvidencijskeKartice = i.ToString().PadLeft(10, '0');
                DataProvider.DB.EvidencijskaKarticas.Add(kartica);
                DataProvider.DB.Entry(kartica).State = System.Data.Entity.EntityState.Added;
            }
            DataProvider.DB.SaveChanges();                
        }


        public string ValidanRasponKartica(int RasponOd, int RasponDo)
        {
            string result = "";

            Func<string, bool> contains = delegate (string s)
            {
                if (string.IsNullOrWhiteSpace(s))
                {
                    return false;
                }
                long cardNumber = Convert.ToInt64(s);

                return cardNumber >= RasponOd && cardNumber <= RasponDo;
            };

            //List<EvidencijskaKartica> evidencijskeKarticeURasponu = DataProvider.DB.EvidencijskaKarticas.Where(x => contains(x.BrojEvidencijskeKartice)).ToList();
            var BrojOd = new SqlParameter("@BrojOd", RasponOd);
            var BrojDo = new SqlParameter("@BrojDo", RasponDo);
            var evidencijskeKarticeURasponu = DataProvider.DB.Database
                .SqlQuery<String>("ValidirajRasponEvidencijskihKartica @BrojOd, @BrojDo", BrojOd, BrojDo)
                .ToList();

            if (evidencijskeKarticeURasponu.Count > 0)
            {
                result = String.Join(",", evidencijskeKarticeURasponu);
            }
            return result;
        }
    }
}