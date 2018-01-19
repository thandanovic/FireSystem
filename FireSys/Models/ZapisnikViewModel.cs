using FireSys.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FireSys.Models
{
    public class ZapisnikViewModel
    {
        readonly IEnumerable _lokacije;
        readonly IEnumerable _klijenti;
        readonly IEnumerable _tipoviZapisnika;
        readonly IEnumerable _zaposlenici;

        public Zapisnik Zapisnik { get; set; }

        public int KlijentId { get; set; }


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

        public ZapisnikViewModel()
        {
            _lokacije = FireSys.Helpers.DataProvider.GetLocations();
            _klijenti = FireSys.Helpers.DataProvider.GetKlijenti();
            _tipoviZapisnika = FireSys.Helpers.DataProvider.GetZapisnikTip();
            _zaposlenici = FireSys.Helpers.DataProvider.GetZaposlenici();
        }

        public ZapisnikAparatViewModel Aparat { get; set; }

    }
}