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
        public List<UserInfo> Clients { get; set; }
        public List<Order> Orders { get; set; }
        public List<Location> Locations { get; set; }
    }
}