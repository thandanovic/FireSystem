namespace FireSys.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("viewUsersFull")]
    public partial class viewUsersFull
    {
        [Key]
        [Column(Order = 0)]
        public string Id { get; set; }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        [Key]
        [Column(Order = 1)]
        public string Discriminator { get; set; }

        [StringLength(150)]
        public string Prezime { get; set; }

        [StringLength(150)]
        public string Ime { get; set; }
    }
}
