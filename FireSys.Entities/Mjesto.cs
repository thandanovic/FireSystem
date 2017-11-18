namespace FireSys.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Mjesto")]
    public partial class Mjesto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Mjesto()
        {
            Lokacijas = new HashSet<Lokacija>();
        }

        public int MjestoId { get; set; }

        [Required]
        [StringLength(500)]
        public string Naziv { get; set; }

        public DateTime DatumKreiranja { get; set; }

        public bool? Obrisano { get; set; }

        [Required]
        [StringLength(50)]
        public string KorisnikKreiraoId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Lokacija> Lokacijas { get; set; }
    }
}
