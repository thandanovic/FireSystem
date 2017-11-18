namespace FireSys.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RadniNalog")]
    public partial class RadniNalog
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RadniNalog()
        {
            RadniNalogAparats = new HashSet<RadniNalogAparat>();
            RadniNalogHidrants = new HashSet<RadniNalogHidrant>();
            Zapisniks = new HashSet<Zapisnik>();
            Zapisniks1 = new HashSet<Zapisnik>();
            Zapisniks2 = new HashSet<Zapisnik>();
        }

        public int RadniNalogId { get; set; }

        public int LokacijaId { get; set; }

        public DateTime DatumNaloga { get; set; }

        public Guid KorisnikKreiraiId { get; set; }

        public int BrojNaloga { get; set; }

        public int BrojNalogaMjesec { get; set; }

        public int BrojNalogaGodina { get; set; }

        public DateTime DatumKreiranja { get; set; }

        public bool Aparati { get; set; }

        public bool Hidranti { get; set; }

        [StringLength(4000)]
        public string Komentar { get; set; }

        public int? BrojHidranata { get; set; }

        public int? BrojAparata { get; set; }

        [StringLength(250)]
        public string Narucilac { get; set; }

        public virtual Lokacija Lokacija { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RadniNalogAparat> RadniNalogAparats { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RadniNalogHidrant> RadniNalogHidrants { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Zapisnik> Zapisniks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Zapisnik> Zapisniks1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Zapisnik> Zapisniks2 { get; set; }
    }
}
