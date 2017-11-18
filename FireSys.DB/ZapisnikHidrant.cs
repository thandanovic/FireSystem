namespace FireSys.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ZapisnikHidrant")]
    public partial class ZapisnikHidrant
    {
        public int ZapisnikHidrantId { get; set; }

        public int HidrantId { get; set; }

        public int ZapisnikId { get; set; }

        public int KompletnostId { get; set; }

        public bool Valid { get; set; }

        public DateTime DatumKreiranja { get; set; }

        [StringLength(4000)]
        public string Komentar { get; set; }

        [StringLength(250)]
        public string Mikrolokacija { get; set; }

        public virtual Hidrant Hidrant { get; set; }

        public virtual Kompletnost Kompletnost { get; set; }

        public virtual Zapisnik Zapisnik { get; set; }
    }
}
