using FireSys.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FireSys.Models
{
    public class DashboardViewModel : BaseViewModel
    {
        public List<Hidrant> Hidranti { get; set; }
        public List<Hidrant> Orders { get; set; }
        public List<Hidrant> Locations { get; set; }

        public int RadniNaloziCount { get; set; }
        public int ZapisniciCount { get; set; }
        public int EvidencijskeCount { get; set; }
        public int FaktureCount { get; set; }
        public int LokacijeCount { get; set; }
        public int KlijentiCount { get; set; }
        public int HidrantiCount { get; set; }
        public int VatrigasniAparatiCount { get; set; }

    }
}