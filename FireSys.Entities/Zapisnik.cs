namespace FireSys.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Zapisnik")]
    public partial class Zapisnik
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Zapisnik()
        {
            ZapisnikAparats = new HashSet<ZapisnikAparat>();
            ZapisnikHidrants = new HashSet<ZapisnikHidrant>();
            DatumZapisnika = DateTime.Now;
            BrojZapisnikaGodina = DateTime.Now.Year;
            BrojZapisnikaMjesec = DateTime.Now.Month;
        }

        public int ZapisnikId { get; set; }

        public int BrojZapisnika { get; set; }

        public int BrojZapisnikaGodina { get; set; }

        public DateTime DatumKreiranja { get; set; }

        public int? RadniNalogId { get; set; }

        public bool Validan { get; set; }

        public DateTime? DatumBrisanja { get; set; }

        public int BrojZapisnikaMjesec { get; set; }

        public DateTime DatumZapisnika { get; set; }

        public int ZapisnikTipId { get; set; }

        public int LokacijaId { get; set; }

        public Guid PregledIzvrsioId { get; set; }

        [StringLength(4000)]
        public string Napomena { get; set; }

        public Guid KorisnikKreiraoId { get; set; }

        public bool Fakturisano { get; set; }

        public Guid? KorisnikKontrolisaoId { get; set; }

        [StringLength(50)]
        public string BrojFakture { get; set; }

        public bool? DokumentacijaPrisutna { get; set; }

        [StringLength(100)]
        public string LokacijaHidranata { get; set; }

        [StringLength(500)]
        public string HidrantPrikljucenNa { get; set; }

        public int? IzRadnogNalogaId { get; set; }

        public int? KreiraniRadniNalogId { get; set; }

        [NotMapped]
        public string FullBrojZapisnika
        {
            get { return string.Format("{0}-{1}/{2}", BrojZapisnika, BrojZapisnikaMjesec, BrojZapisnikaGodina); }
        }

        public virtual Lokacija Lokacija { get; set; }

        public virtual RadniNalog RadniNalog { get; set; }

        public virtual RadniNalog RadniNalog1 { get; set; }

        public virtual RadniNalog RadniNalog2 { get; set; }

        public virtual ZapisnikTip ZapisnikTip { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ZapisnikAparat> ZapisnikAparats { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ZapisnikHidrant> ZapisnikHidrants { get; set; }


       
    }
}
