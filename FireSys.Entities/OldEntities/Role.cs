using FireSys.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireSys.Entities
{
    public class Role : BaseEntity, IRole
    {
        public Int64 RoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
