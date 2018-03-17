using System;

namespace FireSys.Entities
{
    public class RadniNalogDolazeci
    {
        public string Klijent { get; set; }
        public Int32 ZapisnikId { get; set; }
        public string Lokacija { get; set; }
        public string BrojZapisnika { get; set; }
        public DateTime DatumZapisnika { get; set; }
        public string VrstaZapisnika { get; set; }

        public int StatusId { get; set; }
    }
   
}
