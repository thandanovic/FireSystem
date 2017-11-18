namespace FireSys.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EvidencijskaKartica")]
    public partial class EvidencijskaKartica
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EvidencijskaKartica()
        {
            VatrogasniAparats = new HashSet<VatrogasniAparat>();
            ZapisnikAparats = new HashSet<ZapisnikAparat>();
        }

        public int EvidencijskaKarticaId { get; set; }

        [Required]
        [StringLength(10)]
        public string BrojEvidencijskeKartice { get; set; }

        public Guid UserId { get; set; }

        public bool Validna { get; set; }

        public bool Obrisana { get; set; }

        public int EvidencijskaKarticaTipId { get; set; }

        public DateTime DatumZaduzenja { get; set; }

        public virtual EvidencijskaKarticaTip EvidencijskaKarticaTip { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VatrogasniAparat> VatrogasniAparats { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ZapisnikAparat> ZapisnikAparats { get; set; }
    }
}
