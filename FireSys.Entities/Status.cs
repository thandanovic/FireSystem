namespace FireSys.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Status ")]
    public partial class Status
    {
        public Status ()
        {
            RadniNalogs = new HashSet<RadniNalog>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Naziv { get; set; }

        public virtual ICollection<RadniNalog> RadniNalogs { get; set; }

    }
}
