using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireSys.Entities
{
    public class BaseEntity
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }

        public BaseEntity()
        {
            CreatedOn = DateTime.Now;
            UpdatedOn = DateTime.Now;
        }
    }
}
