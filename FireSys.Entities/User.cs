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
    public class User : BaseEntity, IUser
    {

        public Int64 UserId { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        public string Pass { get; set; }

        [StringLength(50)]
        [Display(Name="First Name")]
        public string FirstName { get; set; }
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Name
        {
            get
            {
                return String.IsNullOrEmpty(LastName+FirstName) ? Email : LastName + ", " + FirstName;
            }
        }

        public string CurrentRoles
        {
            get
            {
                return String.Join(", ", Roles);
            }
        }

        public List<Role> Roles { get; set; }

    }
}
