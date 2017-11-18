namespace FireSys.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
 

    [Table("Korisnik")]
    public partial class Korisnik
    {
        public Guid KorisnikId { get; set; }

        [Required]
        [StringLength(150)]
        public string Ime { get; set; }

        [Required]
        [StringLength(150)]
        public string Prezime { get; set; }
    }
}
