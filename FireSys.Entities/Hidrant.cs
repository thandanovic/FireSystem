namespace FireSys.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Hidrant")]
    public partial class Hidrant
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Hidrant()
        {
            RadniNalogHidrants = new HashSet<RadniNalogHidrant>();
            ZapisnikHidrants = new HashSet<ZapisnikHidrant>();
        }

        public int HidrantId { get; set; }

        [Required]
        [StringLength(50)]
        public string Oznaka { get; set; }

        public int InstalacijaId { get; set; }

        public int? HidrantTipId { get; set; }

        public decimal? HidrostatickiPritisak { get; set; }

        public decimal? HidrodinamickiPritisak { get; set; }

        public int? Protok { get; set; }

        public int? KompletnostId { get; set; }

        public int LokacijaId { get; set; }

        public int? PromjerMlazniceId { get; set; }

        public virtual HidrantTip HidrantTip { get; set; }

        public virtual Instalacija Instalacija { get; set; }

        public virtual Kompletnost Kompletnost { get; set; }

        public virtual Lokacija Lokacija { get; set; }

        public virtual PromjerMlaznice PromjerMlaznice { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RadniNalogHidrant> RadniNalogHidrants { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ZapisnikHidrant> ZapisnikHidrants { get; set; }
    }
}
