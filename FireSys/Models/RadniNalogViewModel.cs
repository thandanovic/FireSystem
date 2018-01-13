using FireSys.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FireSys.Models
{
    public class RadniNalogViewModel
    {
       
        public int RadniNalogId { get; set; }

        [Display(Name = "Lokacija")]
        public int LokacijaId { get; set; }

        [Display(Name = "Datum naloga")]
        public DateTime DatumNaloga { get; set; }

        public Guid KorisnikKreiraiId { get; set; }

        [Display(Name = "Broj naloga")]
        public int BrojNaloga { get; set; }

        public int BrojNalogaMjesec { get; set; }

        public int BrojNalogaGodina { get; set; }

        public DateTime DatumKreiranja { get; set; }

        public bool Aparati { get; set; }

        public bool Hidranti { get; set; }

        [StringLength(4000)]
        public string Komentar { get; set; }

        [Display(Name = "Broj hidranata")]
        public int? BrojHidranata { get; set; }

        [Display(Name = "Broj aparata")]
        public int? BrojAparata { get; set; }

        [StringLength(250)]
        [Display(Name = "Naručilac")]
        public string Narucilac { get; set; }

        [Display(Name = "Lokacija")]
        public int SelectedKlijentId { get; set; }

        public SelectList Klijenti { get; set; }
        

    }
}