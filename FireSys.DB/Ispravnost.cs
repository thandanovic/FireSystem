namespace FireSys.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ispravnost")]
    public partial class Ispravnost
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ispravnost()
        {
            VatrogasniAparats = new HashSet<VatrogasniAparat>();
            ZapisnikAparats = new HashSet<ZapisnikAparat>();
        }

        public int IspravnostId { get; set; }

        [StringLength(500)]
        public string Naziv { get; set; }

        public bool Obrisano { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VatrogasniAparat> VatrogasniAparats { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ZapisnikAparat> ZapisnikAparats { get; set; }
    }
}
