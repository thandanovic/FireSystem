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
    }
}