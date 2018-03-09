using FireSys.Entities;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FireSys.Models
{
    public class ZapisnikAparatParticle
    {
        public int ZapisnikAparatId { get; set; }
        public int TipId { get; set; }
        public string BrojAparata { get; set; }
        public int GodinaProizvodnje { get; set; }
        public int VrijediDo { get; set; }
        public string BrojKartice { get; set; }
        public string Napomena { get; set; }
        public int IspravnostId { get; set; }
        public int VrstaId { get; set; }
        [JsonIgnore]
        public int LokacijaId { get; set; }
        [JsonIgnore]
        public int ZapisnikId { get; set; }

        public string ErrorMessage { get; set; }

        public bool Validate()
        {
            if (TipId == 0)
            {
                ErrorMessage = "Tip aparata ne smije biti nedefinisan";
                return false;
            }

            if (string.IsNullOrEmpty(BrojAparata))
            {
                ErrorMessage = "Broj aparata ne smije biti nedefinisan";
                return false;
            }

            if (GodinaProizvodnje == 0)
            {
                ErrorMessage = "Godina proizvodnje ne smije biti nedefinisana";
                return false;
            }

            if (VrijediDo == 0)
            {
                ErrorMessage = "Vrijedi do ne smije biti nedefinisan";
                return false;
            }

            if (IspravnostId == 0)
            {
                ErrorMessage = "Ispravnost ne smije biti nedefinisana";
                return false;
            }

            if (VrstaId == 0)
            {
                ErrorMessage = "Vrsta ne smije biti nedefinisana";
                return false;
            }

            return true;
        }
    }

    public class ZapisnikAparatExtended : ZapisnikAparatParticle
    {
        public string Tip { get; set; }
        public string Ispravnost { get; set; }
        public string Vrsta { get; set; }
    }

    public class ZapisnikHidrantParticle
    {
        public int ZapisnikHidrantId { get; set; }
        public int TipId { get; set; }
        public string BrojAparata { get; set; }
        public int GodinaProizvodnje { get; set; }
        public int VrijediDo { get; set; }
        public string BrojKartice { get; set; }
        public string Napomena { get; set; }
        public int IspravnostId { get; set; }
        public int VrstaId { get; set; }
        [JsonIgnore]
        public int LokacijaId { get; set; }
        [JsonIgnore]
        public int ZapisnikId { get; set; }

        public string ErrorMessage { get; set; }

        public bool Validate()
        {
            if (TipId == 0)
            {
                ErrorMessage = "Tip aparata ne smije biti nedefinisan";
                return false;
            }

            if (string.IsNullOrEmpty(BrojAparata))
            {
                ErrorMessage = "Broj aparata ne smije biti nedefinisan";
                return false;
            }

            if (GodinaProizvodnje == 0)
            {
                ErrorMessage = "Godina proizvodnje ne smije biti nedefinisana";
                return false;
            }

            if (VrijediDo == 0)
            {
                ErrorMessage = "Vrijedi do ne smije biti nedefinisan";
                return false;
            }

            if (IspravnostId == 0)
            {
                ErrorMessage = "Ispravnost ne smije biti nedefinisana";
                return false;
            }

            if (VrstaId == 0)
            {
                ErrorMessage = "Vrsta ne smije biti nedefinisana";
                return false;
            }

            return true;
        }
    }

    public class ZapisnikAparatViewModel
    {
        public List<ZapisnikAparatParticle> Aparati { get; set; }
        public List<ZapisnikHidrantParticle> Hidranti { get; set; }

        readonly IEnumerable _lokacije;
        readonly IEnumerable _klijenti;
        readonly IEnumerable _tipoviZapisnika;
        readonly IEnumerable _zaposlenici;
        readonly IEnumerable _tipoviAparata;
        readonly IEnumerable _ispravnost;
        readonly IEnumerable _aparatVrsta;

        public IEnumerable<SelectListItem> Lokacije
        {
            get { return new SelectList(_lokacije, "LokacijaId", "Naziv"); }
        }

        public IEnumerable<SelectListItem> Klijenti
        {
            get { return new SelectList(_klijenti, "KlijentId", "Naziv"); }
        }

        public IEnumerable<SelectListItem> TipoviZapisnika
        {
            get { return new SelectList(_tipoviZapisnika, "ZapisnikTipId", "Naziv"); }
        }

        public IEnumerable<SelectListItem> Zaposlenici
        {
            get { return new SelectList(_zaposlenici, "KorisnikId", "Naziv"); }
        }

        public IEnumerable<SelectListItem> TipoviAparata
        {
            get { return new SelectList(_tipoviAparata, "VatrogasniAparatTipId", "Naziv"); }
        }

        public IEnumerable<SelectListItem> Ispravnost
        {
            get { return new SelectList(_ispravnost, "IspravnostId", "Naziv"); }
        }

        public IEnumerable<SelectListItem> VrstaAparata
        {
            get { return new SelectList(_aparatVrsta, "VatrogasniAparatVrstaId", "Naziv"); }
        }

        public ZapisnikAparatViewModel()
        {
            _lokacije = FireSys.Helpers.DataProvider.GetLocations();
            _klijenti = FireSys.Helpers.DataProvider.GetKlijenti();
            _tipoviZapisnika = FireSys.Helpers.DataProvider.GetZapisnikTip();
            _zaposlenici = FireSys.Helpers.DataProvider.GetZaposlenici();
            _tipoviAparata = FireSys.Helpers.DataProvider.GetTipAparata();
            _ispravnost = FireSys.Helpers.DataProvider.GetIspravnost();
            _aparatVrsta = FireSys.Helpers.DataProvider.GetVrstaAparata();
        }
    }
}