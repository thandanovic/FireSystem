namespace FireSys.Entities
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Lokacija")]
    public partial class Lokacija
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Lokacija()
        {
            Hidrants = new HashSet<Hidrant>();
            RadniNalogs = new HashSet<RadniNalog>();
            VatrogasniAparats = new HashSet<VatrogasniAparat>();
            Zapisniks = new HashSet<Zapisnik>();
        }

        public int LokacijaId { get; set; }

        [Required]
        [StringLength(500)]
        public string Naziv { get; set; }

        [StringLength(500)]
        public string Adresa { get; set; }

        public int? MjestoId { get; set; }

        public int? RegijaId { get; set; }

        [StringLength(2000)]
        public string Kontakt { get; set; }

        [StringLength(2000)]
        public string Komentar { get; set; }

        public DateTime DatumKreiranja { get; set; }

        [Required]
        [StringLength(50)]
        public string KorisnikKreiraoId { get; set; }

        public DateTime? DatumBrisanja { get; set; }

        public bool? Obrisano { get; set; }

        public int KlijentId { get; set; }

        public int? LokacijaVrstaId { get; set; }

        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Hidrant> Hidrants { get; set; }

        [JsonIgnore]
        public virtual Klijent Klijent { get; set; }

        [JsonIgnore]
        public virtual LokacijaVrsta LokacijaVrsta { get; set; }

        [JsonIgnore]
        public virtual Mjesto Mjesto { get; set; }

        [JsonIgnore]
        public virtual Regija Regija { get; set; }

        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RadniNalog> RadniNalogs { get; set; }

        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VatrogasniAparat> VatrogasniAparats { get; set; }

        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Zapisnik> Zapisniks { get; set; }
    }
}
