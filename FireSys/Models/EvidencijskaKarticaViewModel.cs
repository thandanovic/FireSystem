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
    public class EvidencijskaKarticaViewModel
    {
        public EvidencijskaKarticaViewModel()
        {                        
            this.DatumZaduzenja = DateTime.Now;
            int lastRadniNalogId = DataProvider.DB.EvidencijskaKarticas.Max(r => r.EvidencijskaKarticaId);
            EvidencijskaKartica radniNalog = DataProvider.DB.EvidencijskaKarticas.Find(lastRadniNalogId);
            this.RasponOd = Convert.ToInt32(radniNalog.BrojEvidencijskeKartice)+1;
            this.RasponDo = this.RasponOd + 100;
        }

        public EvidencijskaKarticaViewModel(EvidencijskaKartica evidencijska)
        {
            FillViewModel(evidencijska);
        }

        public void FillViewModel(EvidencijskaKartica  evidencijskaKartica)
        {
            this.DatumZaduzenja = evidencijskaKartica.DatumZaduzenja;
            this.EvidencijskaKarticaId = evidencijskaKartica.EvidencijskaKarticaId;
            this.EvidencijskaKarticaTipId = evidencijskaKartica.EvidencijskaKarticaTipId;
            this.UserId = evidencijskaKartica.UserId;
        }

        public int EvidencijskaKarticaId { get; set; }

        
        [StringLength(10)]
        [Display(Name = "Raspon kartica")]
        public string BrojEvidencijskeKartice { get; set; }

        [Required]
        [Display(Name = "Korisnik")]
        public Guid UserId { get; set; }

        
        [Required]
        public int RasponOd { get; set; }

        [Required]
        public int RasponDo { get; set; }

        [Required]
        [Display(Name = "Tip Kartice")]
        public int EvidencijskaKarticaTipId { get; set; }

        public bool RemoveKartice { get; set; }

        [Required]
        public DateTime DatumZaduzenja { get; set; }

        private SelectList _tipoviKartice;
        private SelectList _korisnici;

        
    public SelectList Korisnici
        {
            get
            {
                if (_korisnici == null) _korisnici = new SelectList(DataProvider.DB.Korisniks.Select(x => new SelectListItem
                {
                    Value = x.KorisnikId.ToString(),
                    Text = x.Ime + " " + x.Prezime
                }), "Value", "Text");

                return _korisnici;
            }
            set { }
        }
        public SelectList TippviKartice {
            get
            {
                if(_tipoviKartice==null) _tipoviKartice = new SelectList(DataProvider.DB.EvidencijskaKarticaTips, "EvidencijskaKarticaTipId", "EvidencijskaKarticaTipNaziv");

                return _tipoviKartice;
            }
            set { }
        }
        

        public EvidencijskaKartica GetEvidencijskaKartica()
        {
            EvidencijskaKartica evidencijskaKartica = new EvidencijskaKartica();
            evidencijskaKartica.DatumZaduzenja = this.DatumZaduzenja;
            evidencijskaKartica.EvidencijskaKarticaId = this.EvidencijskaKarticaId;
            evidencijskaKartica.EvidencijskaKarticaTipId = this.EvidencijskaKarticaTipId;
            evidencijskaKartica.UserId = this.UserId;
            return evidencijskaKartica;
        }


    }
}