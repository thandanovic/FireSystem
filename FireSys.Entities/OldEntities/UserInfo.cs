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
    public class UserInfo : BaseEntity, IUser
    {

        public Int64? UserId { get; set; }
        public Int64 UserInfoId { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [Display(Name ="Password")]
        public string Pass { get; set; }

        [Required]
        [StringLength(50)]
        public string Company { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }
        
        [StringLength(50)]
        [Required]
        public string Street { get; set; }

        [Required]
        [Display(Name = "Contact Email's")]
        public string ContactEmail { get; set; }

        public User User { get; set; }


    }
}
