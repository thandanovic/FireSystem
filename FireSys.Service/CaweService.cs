using FireSys.Entities;
using FireSys.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireSys.Service
{
    
    public class CaWeService : ICaweService
    {
        private static CaWeService caweService;

        public static CaWeService Instance
        {
           get { return caweService ?? (caweService = new CaWeService()); }
        }

        private RoleManager roleManager;

        private RoleManager RoleManager
        {
            get { return roleManager ?? (roleManager = new RoleManager()); }
        }

        public List<Role> GetAllRoles()
        {
            return RoleManager.GetAll().ToList();
        }

    }
}
