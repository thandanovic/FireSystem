namespace FireSys.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("VatrogasniAparat")]
    public partial class VatrogasniAparat
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VatrogasniAparat()
        {
            RadniNalogAparats = new HashSet<RadniNalogAparat>();
            ZapisnikAparats = new HashSet<ZapisnikAparat>();
        }

        public int VatrogasniAparatId { get; set; }

        public int VatrogasniAparatTipId { get; set; }

        [Required]
        [StringLength(50)]
        public string BrojaAparata { get; set; }

        public int? GodinaProizvodnje { get; set; }

        public int? EvidencijskaKarticaId { get; set; }

        [StringLength(500)]
        public string Napomena { get; set; }

        public int LokacijaId { get; set; }

        public int? IspravnostId { get; set; }

        public int? IspitivanjeVrijediDo { get; set; }

        public DateTime DatumKreiranja { get; set; }

        public bool Obrisan { get; set; }

        public Guid KorisnikKreiraoId { get; set; }

        public int VatrogasniAparatVrstaId { get; set; }

        public virtual EvidencijskaKartica EvidencijskaKartica { get; set; }

        public virtual Ispravnost Ispravnost { get; set; }

        public virtual Lokacija Lokacija { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RadniNalogAparat> RadniNalogAparats { get; set; }

        public virtual VatrogasniAparatTip VatrogasniAparatTip { get; set; }

        public virtual VatrogasniAparatVrsta VatrogasniAparatVrsta { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ZapisnikAparat> ZapisnikAparats { get; set; }
    }
}
