namespace FireSys.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

 
    public partial class EvidencijskaKarticaGrid
    {
      
        public EvidencijskaKarticaGrid()
        {
        }

        public int EvidencijskaKarticaId { get; set; }
        
        public string BrojEvidencijskeKartice { get; set; }
        
        public Guid UserId { get; set; }

        public bool Validna { get; set; }

        public bool Obrisana { get; set; }

        public int EvidencijskaKarticaTipId { get; set; }

        public DateTime DatumZaduzenja { get; set; }

        public String Klijent { get; set; }

        public String Lokacija { get; set; }

        public String Radnik { get; set; }



        
    }
}
