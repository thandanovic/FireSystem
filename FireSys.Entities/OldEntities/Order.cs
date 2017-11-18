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
    public class Order : BaseEntity
    {

        public Int64? UserInfoId { get; set; }

   
        public Int64? LocationId { get; set; }

        public Int64? OrderId { get; set; }

        [Required]
        [Display(Name = "Payment status")]
        public string PaymentStatus { get; set; }

        [Required]
        [Display(Name = "Order Status")]
        public string OrderStatus { get; set; }

        [Required]
        [Display(Name = "Container ID")]
        public string ContainerId { get; set; }

        [Required]
        public DateTime? Deadline { get; set; }

        [Required]
        [Display(Name = "Price")]
        public decimal TotalPrice { get; set; }

        [Required]
        public decimal? Weight { get; set; }

        public UserInfo UserInfo { get; set; }
        public Location Location { get; set; }

        [NotMapped]
        public List<Location> LocationsList { get; set; }

    }
}
