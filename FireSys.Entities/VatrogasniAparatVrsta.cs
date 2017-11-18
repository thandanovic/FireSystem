namespace FireSys.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("VatrogasniAparatVrsta")]
    public partial class VatrogasniAparatVrsta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VatrogasniAparatVrsta()
        {
            VatrogasniAparats = new HashSet<VatrogasniAparat>();
        }

        public int VatrogasniAparatVrstaId { get; set; }

        [Required]
        [StringLength(50)]
        public string Naziv { get; set; }

        [Required]
        [StringLength(5)]
        public string SkraceniNaziv { get; set; }

        public bool Valid { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VatrogasniAparat> VatrogasniAparats { get; set; }
    }
}
