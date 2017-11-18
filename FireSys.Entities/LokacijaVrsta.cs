namespace FireSys.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("LokacijaVrsta")]
    public partial class LokacijaVrsta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LokacijaVrsta()
        {
            Lokacijas = new HashSet<Lokacija>();
        }

        public int LokacijaVrstaId { get; set; }

        [Required]
        [StringLength(250)]
        public string Naziv { get; set; }

        public bool? Obrisano { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Lokacija> Lokacijas { get; set; }
    }
}
