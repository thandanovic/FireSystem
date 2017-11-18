
using FireSys.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FireSys.Models
{
    public class UserViewModel: BaseViewModel
    {
        public AspNetUser  UserInfo {get;set;} 
    }
}