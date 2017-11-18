namespace FireSys.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("VatrogasniAparatTip")]
    public partial class VatrogasniAparatTip
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VatrogasniAparatTip()
        {
            VatrogasniAparats = new HashSet<VatrogasniAparat>();
        }

        public int VatrogasniAparatTipId { get; set; }

        [Required]
        [StringLength(50)]
        public string Naziv { get; set; }

        public bool Obrisan { get; set; }

        public bool Validan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VatrogasniAparat> VatrogasniAparats { get; set; }
    }
}
