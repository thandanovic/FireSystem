namespace FireSys.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RadniNalogHidrant")]
    public partial class RadniNalogHidrant
    {
        public int RadniNalogHidrantId { get; set; }

        public int HidrantId { get; set; }

        public int RadniNalogId { get; set; }

        public virtual Hidrant Hidrant { get; set; }

        public virtual RadniNalog RadniNalog { get; set; }
    }
}
