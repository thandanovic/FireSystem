using FireSys.Entities.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireSys.Entities
{
    public class Location : BaseEntity
    {

        public Int64 LocationId { get; set; }       

        [Required]

        [Display(Name = "Name")]
        public string LocationName { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string LocationAddress { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Start Time")]
        public string LocationStartingTime { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }
        
        [StringLength(50)]
        [Required]
        [Display(Name = "End Time")]
        public string LocationEndTime { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "Working")]
        public string LocationWorkingDays { get; set; }
        
        [Required]
        [Display(Name = "Price")]
        public Decimal LocationPrice { get; set; }
    }
}
