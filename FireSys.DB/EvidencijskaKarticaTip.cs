namespace FireSys.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EvidencijskaKarticaTip")]
    public partial class EvidencijskaKarticaTip
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EvidencijskaKarticaTip()
        {
            EvidencijskaKarticas = new HashSet<EvidencijskaKartica>();
        }

        public int EvidencijskaKarticaTipId { get; set; }

        [Required]
        [StringLength(50)]
        public string EvidencijskaKarticaTipNaziv { get; set; }

        public bool Valid { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EvidencijskaKartica> EvidencijskaKarticas { get; set; }
    }
}
