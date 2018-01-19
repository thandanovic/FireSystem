namespace FireSys.Entities
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("ZapisnikAparat")]
    public partial class ZapisnikAparat
    {
        public int ZapisnikAparatId { get; set; }

        public int VatrogasniAparatId { get; set; }

        public int ZapisnikId { get; set; }

        public int IspravnostId { get; set; }

        public bool Valid { get; set; }

        public DateTime DatumKreiranja { get; set; }

        public int? EvidencijskaKarticaId { get; set; }

        [StringLength(4000)]
        public string Komentar { get; set; }

        public virtual EvidencijskaKartica EvidencijskaKartica { get; set; }

        public virtual Ispravnost Ispravnost { get; set; }

        public virtual VatrogasniAparat VatrogasniAparat { get; set; }

        public virtual Zapisnik Zapisnik { get; set; }
    }
}
