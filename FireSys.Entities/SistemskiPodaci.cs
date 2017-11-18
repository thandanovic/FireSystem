namespace FireSys.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SistemskiPodaci")]
    public partial class SistemskiPodaci
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string NazivFirme { get; set; }

        [Required]
        [StringLength(250)]
        public string Adresa { get; set; }

        [Required]
        [StringLength(20)]
        public string Telefon { get; set; }

        [Required]
        [StringLength(20)]
        public string Fax { get; set; }

        [Required]
        [StringLength(20)]
        public string FirmaID { get; set; }

        [Required]
        [StringLength(20)]
        public string FirmaPB { get; set; }

        [Required]
        [StringLength(20)]
        public string FirmaSudskiBroj { get; set; }
    }
}
