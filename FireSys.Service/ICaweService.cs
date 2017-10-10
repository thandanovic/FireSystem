using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireSys.Entities;

namespace FireSys.Service 
{
    interface ICaweService
    {
        List<Role> GetAllRoles();
    }
}
