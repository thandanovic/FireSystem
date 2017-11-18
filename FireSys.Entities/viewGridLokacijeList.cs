namespace FireSys.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("viewGridLokacijeList")]
    public partial class viewGridLokacijeList
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LokacijaId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(500)]
        public string Lokacija { get; set; }

        [StringLength(500)]
        public string Adresa { get; set; }

        [StringLength(500)]
        public string Mjesto { get; set; }

        [StringLength(500)]
        public string Klijent { get; set; }

        [StringLength(500)]
        public string Regija { get; set; }

        [StringLength(2000)]
        public string Kontakt { get; set; }
    }
}
