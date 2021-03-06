namespace FireSys.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("RadniNalogAparat")]
    public partial class RadniNalogAparat
    {
        public int RadniNalogAparatId { get; set; }

        public int VatrogasniAparatId { get; set; }

        public int RadniNalogId { get; set; }

        public virtual RadniNalog RadniNalog { get; set; }

        public virtual VatrogasniAparat VatrogasniAparat { get; set; }
    }
}
