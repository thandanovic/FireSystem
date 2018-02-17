using FireSys.Entities;
using FireSys.Helpers;
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
        public RadniNalogViewModel()
        {
            this.BrojAparata = 0;
            this.BrojHidranata = 0;            
            this.DatumNaloga = DateTime.Now;
        }

        public RadniNalogViewModel(RadniNalog radniNalog)
        {
            FillViewModel(radniNalog);
        }

        public void FillViewModel(RadniNalog radniNalog)
        {
            this.RadniNalogId = radniNalog.RadniNalogId;
            this.DatumKreiranja = radniNalog.DatumKreiranja;
            this.LokacijaId = radniNalog.LokacijaId;
            this.DatumNaloga = radniNalog.DatumNaloga;
            this.BrojNaloga = radniNalog.BrojNaloga;
            this.BrojNalogaMjesec = radniNalog.BrojNalogaMjesec;
            this.BrojNalogaGodina = radniNalog.BrojNalogaGodina;
            this.DatumKreiranja = radniNalog.DatumKreiranja;
            this.BrojAparata = radniNalog.BrojAparata;
            this.BrojHidranata = radniNalog.BrojHidranata;
            this.Aparati = radniNalog.Aparati;
            this.Hidranti = radniNalog.Hidranti;
            this.Komentar = radniNalog.Komentar;
            this.Narucilac = radniNalog.Narucilac;
            this.SelectedKlijentId = radniNalog.Lokacija.KlijentId;
        }

        public int RadniNalogId { get; set; }

        [Display(Name = "Lokacija")]
        public int? LokacijaId { get; set; }

        [Display(Name = "Datum naloga")]
        public DateTime DatumNaloga { get; set; }   

        public bool IsNewLokacija { get; set; }

        public string NoviKlijentNaziv { get; set; }

        public string NovaLokacijaNaziv { get; set; }

        public string NovaLokacijaKomentar { get; set; }

        public Guid KorisnikKreiraiId { get; set; }

        [Display(Name = "Broj naloga")]
        public int BrojNaloga { get; set; }

        public int BrojNalogaMjesec
        {

            get
            {
                return DatumNaloga.Month;
            }
            set { }
        }

        public int BrojNalogaGodina
        {

            get
            {
                return DatumNaloga.Year;
            }
            set { }

        }

        public DateTime DatumKreiranja { get; set; }

        public bool Aparati
        {
            get
            {
                return BrojAparata>0;
            }
            set { }
        }

        public bool Hidranti {
            get
            {
                return BrojHidranata > 0;
            }
            set { }
        }

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
        public int? SelectedKlijentId { get; set; }

        [Display(Name = "Regija")]
        public int RegijaId { get; set; }

        private SelectList _klijenti;
        private SelectList _regije;


        public SelectList Klijenti
        {
            get
            {
                return _klijenti ?? new SelectList(DataProvider.DB.Klijents, "KlijentId", "Naziv");
            }
            set { }
        }
        public SelectList Regije {
            get
            {
                return _regije ?? new SelectList(DataProvider.DB.Regijas, "RegijaId", "Naziv");
            }
            set { }
        }

        public RadniNalog GetRadniNalog()
        {
            RadniNalog radni = new RadniNalog();
            radni.LokacijaId = this.LokacijaId.Value;
            radni.DatumNaloga = this.DatumNaloga;
            radni.BrojNaloga = this.BrojNaloga;
            radni.BrojNalogaMjesec = this.BrojNalogaMjesec;
            radni.BrojNalogaGodina = this.BrojNalogaGodina;
            radni.DatumKreiranja = this.DatumKreiranja;
            radni.BrojAparata = this.BrojAparata;
            radni.BrojHidranata = this.BrojHidranata;
            radni.Aparati = this.Aparati;
            radni.Hidranti = this.Hidranti;
            radni.Komentar = this.Komentar;
            radni.Narucilac = this.Narucilac;
            radni.RadniNalogId = this.RadniNalogId;
            return radni;
        }


    }
}