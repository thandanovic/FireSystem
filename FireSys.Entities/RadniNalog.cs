namespace FireSys.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

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


        public int StatusId { get; set; }


        [Display(Name = "Lokacija")]
        public int LokacijaId { get; set; }

        [Display(Name = "Datum naloga")]
        public DateTime DatumNaloga { get; set; }

        public Guid KorisnikKreiraiId { get; set; }

        [Display(Name = "Broj naloga")]
        public int BrojNaloga { get; set; }

        public int BrojNalogaMjesec { get; set; }

        public int BrojNalogaGodina { get; set; }

        public DateTime DatumKreiranja { get; set; }

        public bool Aparati { get; set; }

        public bool Hidranti { get; set; }

        [StringLength(4000)]
        public string Komentar { get; set; }

        [Display(Name = "Broj hidranata")]
        public int? BrojHidranata { get; set; }

        [Display(Name = "Broj aparata")]
        public int? BrojAparata { get; set; }

        [StringLength(250)]
        [Display(Name = "Naruèilac")]
        public string Narucilac { get; set; }

        [NotMapped]
        public string FullBrojNaloga
        {
            get { return string.Format("{0}-{1}/{2}", BrojNaloga, BrojNalogaMjesec, BrojNalogaGodina); }
        }

        public virtual Lokacija Lokacija { get; set; }

        public virtual Status Status { get; set; }

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
