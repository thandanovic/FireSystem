namespace FireSys.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("viewZapisniciRadniNalozi")]
    public partial class viewZapisniciRadniNalozi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ZapisnikId { get; set; }

        [StringLength(500)]
        public string Klijent { get; set; }

        [StringLength(500)]
        public string Lokacija { get; set; }

        [StringLength(14)]
        public string BrojZapisnika { get; set; }

        [StringLength(17)]
        public string BrojNaloga { get; set; }

        public int? LokacijaId { get; set; }

        public int? KlijentId { get; set; }

        public int? RadniNalogId { get; set; }
    }
}
