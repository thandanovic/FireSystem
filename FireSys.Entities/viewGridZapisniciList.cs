namespace FireSys.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("viewGridZapisniciList")]
    public partial class viewGridZapisniciList
    {
        [StringLength(500)]
        public string Klijent { get; set; }

        [StringLength(500)]
        public string Lokacija { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ZapisnikId { get; set; }

        [StringLength(14)]
        public string BrojZapisnika { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime PregledIzvrsen { get; set; }

        [StringLength(50)]
        public string VrstaZapisnika { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(1)]
        public string PregledIzvrsio { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(1)]
        public string ZapisnikKreirao { get; set; }

        [Key]
        [Column(Order = 4)]
        public DateTime DatumKreiranja { get; set; }
    }
}
