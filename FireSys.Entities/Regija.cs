namespace FireSys.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("Regija")]
    public partial class Regija
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Regija()
        {
            Lokacijas = new HashSet<Lokacija>();
        }

        public int RegijaId { get; set; }

        [Required]
        [StringLength(500)]
        public string Naziv { get; set; }

        public DateTime DatumKreiranja { get; set; }

        public bool? Obrisano { get; set; }

        public int KorisnikKreiraoId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Lokacija> Lokacijas { get; set; }
    }
}
