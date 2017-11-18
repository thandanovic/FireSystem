namespace FireSys.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ZapisnikTip")]
    public partial class ZapisnikTip
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ZapisnikTip()
        {
            Zapisniks = new HashSet<Zapisnik>();
        }

        public int ZapisnikTipId { get; set; }

        [Required]
        [StringLength(50)]
        public string Naziv { get; set; }

        public bool Valid { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Zapisnik> Zapisniks { get; set; }
    }
}
