using DevExpress.Web;
using DevExpress.Web.Mvc;
using DevExpress.Web.Mvc.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;

namespace FireSys.Helpers
{
    public static class GridViewHelper
    {
        public static GridViewSettings GetHidrantsView(this HtmlHelper htmlHelper)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "hidrantsGrid";
            settings.CallbackRouteValues = new { Controller = "Hidrants", Action = "HidrantsGridView" };
            settings.Width = System.Web.UI.WebControls.Unit.Percentage(100);

            settings.KeyFieldName = "HidrantId";

            settings.Settings.ShowFilterRow = true;

            settings.CommandColumn.Visible = true;
            settings.CommandColumn.ShowSelectCheckbox = true;

            settings.SettingsExport.ExportSelectedRowsOnly = true;

            settings.Columns.Add(column =>
            {
                column.FieldName = "HidrantId";
                column.Caption = "ID";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Oznaka";
                column.Caption = "Oznaka";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Protok";
                column.Caption = "Protok";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "HidrostatickiPritisak";
                column.Caption = "Hidrostatički pritisak";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "HidrodinamickiPritisak";
                column.Caption = "Hidrodinamički pritisak";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "HidrantTip.Naziv";
                column.Caption = "Tip";

                column.ColumnType = MVCxGridViewColumnType.ComboBox;
                var comboBoxProperties = column.PropertiesEdit as ComboBoxProperties;
                comboBoxProperties.DataSource = FireSys.Helpers.DataProvider.GetHidrantTypes();
                comboBoxProperties.TextField = "Naziv";
                comboBoxProperties.ValueField = "Naziv";
                comboBoxProperties.ValueType = typeof(string);
                comboBoxProperties.DropDownStyle = DropDownStyle.DropDown;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Instalacija.Naziv";
                column.Caption = "Instalacija";

                column.ColumnType = MVCxGridViewColumnType.ComboBox;
                var comboBoxProperties = column.PropertiesEdit as ComboBoxProperties;
                comboBoxProperties.DataSource = FireSys.Helpers.DataProvider.GetInstallations();
                comboBoxProperties.TextField = "Naziv";
                comboBoxProperties.ValueField = "Naziv";
                comboBoxProperties.ValueType = typeof(string);
                comboBoxProperties.DropDownStyle = DropDownStyle.DropDown;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Kompletnost.Naziv";
                column.Caption = "Kompletnost";

                column.ColumnType = MVCxGridViewColumnType.ComboBox;
                var comboBoxProperties = column.PropertiesEdit as ComboBoxProperties;
                comboBoxProperties.DataSource = FireSys.Helpers.DataProvider.GetKompletnost();
                comboBoxProperties.TextField = "Naziv";
                comboBoxProperties.ValueField = "Naziv";
                comboBoxProperties.ValueType = typeof(string);
                comboBoxProperties.DropDownStyle = DropDownStyle.DropDown;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Lokacija.Naziv";
                column.Caption = "Lokacija";
            });


            settings.Columns.Add(column =>
            {
                column.FieldName = "PromjerMlaznice.Promjer";
                column.Caption = "Promjer mlaznice";

            });

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.Landscape = true;

            settings.SettingsExport.PageHeader.Center = "Powered by dostah";
            settings.SettingsExport.ReportHeader = "Vatrositemi";

            //settings.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;

            settings.CommandColumn.SelectAllCheckboxMode = GridViewSelectAllCheckBoxMode.Page;

            settings.Settings.ShowFilterRowMenu = true;
            settings.CommandColumn.ShowClearFilterButton = true;
            settings.SettingsPager.EnableAdaptivity = true;

            settings.CommandColumn.ShowNewButton = false;
            settings.CommandColumn.ShowDeleteButton = false;
            settings.CommandColumn.ShowEditButton = false;

            //settings.Columns.Add(column => {
            //    column.FieldName = "Unbound";
            //    column.Caption = "";
            //    column.UnboundType = DevExpress.Data.UnboundColumnType.String;
            //    column.EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.False;
            //    column.Width = System.Web.UI.WebControls.Unit.Percentage(10);
            //    column.SetDataItemTemplateContent(c =>
            //    {
            //        htmlHelper.DevExpress().Button(b =>
            //        {
            //            b.Name = "Edit_" + c.KeyValue;
            //            b.Text = "Izmijeni";
            //            b.ClientSideEvents.Click = string.Format(@"function(s, e) {{ alert({0}); }}", c.KeyValue);

            //            b.EnableTheming = false;
            //            b.AllowFocus = false;
            //            b.ControlStyle.CssClass = "wine";
            //            b.Styles.Style.HoverStyle.CssClass = "wineHottracked";
            //            b.Styles.Style.PressedStyle.CssClass = "winePressed";
            //        }).GetHtml();
            //    });
            //});

            //settings.Columns.Add(column => {
            //    column.FieldName = "Unbound";
            //    column.Caption = "";

            //    column.ColumnType = MVCxGridViewColumnType.HyperLink;
            //    var hyperLinkProperties = column.PropertiesEdit as HyperLinkProperties;
            //    //Specify hyperLinkProperties here

            //    hyperLinkProperties.NavigateUrlFormatString = DataBinder.Eval(content.DataItem, "Website") == null ? "about:blank" : DataBinder.Eval(content.DataItem, "Website").ToString();
            //});

            //settings.Columns.Add(column =>
            //{
            //    column.Caption = "Delete";
            //    column.SetDataItemTemplateContent(container =>
            //    {
            //        htmlHelper.DevExpress().HyperLink(hyperlink =>
            //        {
            //            var keyValue = DataBinder.Eval(container.DataItem, "HidrantId");
            //            //var description = DataBinder.Eval(container.DataItem, "AlmasiGereken");
            //            var id = DataBinder.Eval(container.DataItem, "HidrantId");
            //            hyperlink.Name = "hl" + keyValue.ToString();
            //            hyperlink.Properties.Text = "Delete";
            //            //hyperlink.ClientSideEvents.CustomButtonClick = "function(s, e) { document.location='" + DevExpressHelper.GetUrl(new { Controller = "Support", Action = "ResendEvent" }) + "?key=' + s.GetRowKey(e.visibleIndex); }";
            //            hyperlink.NavigateUrl = DevExpressHelper.GetUrl(new { Controller = "Hidrants", Action = "Edit", id = id });
            //        }).Render();
            //    });
            //});

            settings.Settings.ShowFilterRow = true;

            settings.Columns.Add(s =>
            {
                s.FieldName = "edit";
                s.Caption = "Akcije";
                s.Width = 70;
                s.Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;

                s.SetDataItemTemplateContent(c =>
                {
                    var keyValue = DataBinder.Eval(c.DataItem, "HidrantId");

                    htmlHelper.ViewContext.Writer.Write(String.Format("<a href='{0}'><i class='fa fa-pencil-square-o edit-icon'></i></a>", DevExpressHelper.GetUrl(new { Controller = "Hidrants", Action = "Edit", id = keyValue })));
                    htmlHelper.ViewContext.Writer.Write(String.Format("<a style='margin-left: 10px;' class='delete-row' href='#' data-ref='{0}'><i class='fa fa-trash-o delete-icon'></i></a>", DevExpressHelper.GetUrl(new { Controller = "Hidrants", Action = "Delete", id = keyValue })));
                });
            });

            return settings;
        }

        public static GridViewSettings GetRadniNalogView(this HtmlHelper htmlHelper)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "radniNalogGrid";
            settings.CallbackRouteValues = new { Controller = "RadniNalog", Action = "RadniNalogGridView" };
            settings.Width = System.Web.UI.WebControls.Unit.Percentage(100);

            settings.KeyFieldName = "RadniNalogId";

            settings.Settings.ShowFilterRow = true;

            settings.CommandColumn.Visible = true;
            settings.CommandColumn.ShowSelectCheckbox = true;

            settings.SettingsExport.ExportSelectedRowsOnly = true;
            settings.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;

            settings.Columns.Add(column =>
            {
                column.FieldName = "RadniNalogId";
                column.Caption = "ID";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "DatumNaloga";
                column.Caption = "Datum naloga";
                column.PropertiesEdit.DisplayFormatString = "dd.MM.yyyy";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "BrojNaloga";
                column.Caption = "Broj naloga";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "BrojNalogaMjesec";
                column.Caption = "Mjesec";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "BrojNalogaGodina";
                column.Caption = "Godina";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "DatumKreiranja";
                column.Caption = "Datum kreiranja";
                column.PropertiesEdit.DisplayFormatString = "d";
                column.PropertiesEdit.DisplayFormatString = "dd.MM.yyyy";

            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Aparati";
                column.Caption = "Aparati";
                column.ColumnType = MVCxGridViewColumnType.CheckBox;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Hidranti";
                column.Caption = "Hidranti";
                column.ColumnType = MVCxGridViewColumnType.CheckBox;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Komentar";
                column.Caption = "Komentar";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "BrojHidranata";
                column.Caption = "Broj hidranata";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "BrojAparata";
                column.Caption = "Broj aparata";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Narucilac";
                column.Caption = "Naručilac";
            });

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.Landscape = true;

            settings.SettingsExport.PageHeader.Center = "Powered by dostah";
            settings.SettingsExport.ReportHeader = "Vatrositemi";

            //settings.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;

            settings.CommandColumn.SelectAllCheckboxMode = GridViewSelectAllCheckBoxMode.Page;

            settings.Settings.ShowFilterRowMenu = true;
            settings.CommandColumn.ShowClearFilterButton = true;
            settings.SettingsPager.EnableAdaptivity = true;

            settings.CommandColumn.ShowNewButton = false;
            settings.CommandColumn.ShowDeleteButton = false;
            settings.CommandColumn.ShowEditButton = false;

            settings.Settings.ShowFilterRow = true;

            settings.Columns.Add(s =>
            {
                s.FieldName = "edit";
                s.Caption = "Akcije";
                s.Width = 70;
                s.Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;

                s.SetDataItemTemplateContent(c =>
                {
                    var keyValue = DataBinder.Eval(c.DataItem, "RadniNalogId");

                    htmlHelper.ViewContext.Writer.Write(String.Format("<a href='{0}'><i class='fa fa-pencil-square-o edit-icon'></i></a>", DevExpressHelper.GetUrl(new { Controller = "RadniNalog", Action = "Edit", id = keyValue })));
                    htmlHelper.ViewContext.Writer.Write(String.Format("<a style='margin-left: 10px;' class='delete-row' href='#' data-ref='{0}'><i class='fa fa-trash-o delete-icon'></i></a>", DevExpressHelper.GetUrl(new { Controller = "RadniNalog", Action = "Delete", id = keyValue })));
                    //htmlHelper.ViewContext.Writer.Write(String.Format("<a style='margin-left: 10px;' href='{0}'><i class='fa fa-print edit-icon'></i></a>", DevExpressHelper.GetUrl(new { Controller = "RadniNalog", Action = "Report", id = keyValue })));
                    htmlHelper.ViewContext.Writer.Write(String.Format("<a href='#' class='print-report' style='margin-left: 10px;' data-id='{0}' data-url='{1}'><i class='fa fa-print edit-icon'></i></a>", keyValue, DevExpressHelper.GetUrl(new { Controller = "RadniNalog", Action = "Report" })));
                });
            });

            return settings;
        }

        public static GridViewSettings GetZapisnikView(this HtmlHelper htmlHelper)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "zapisnikGrid";
            settings.CallbackRouteValues = new { Controller = "Zapisnik", Action = "ZapisnikGridView" };
            settings.Width = System.Web.UI.WebControls.Unit.Percentage(100);

            settings.KeyFieldName = "ZapisnikId";

            settings.Settings.ShowFilterRow = true;

            settings.CommandColumn.Visible = true;
            settings.CommandColumn.ShowSelectCheckbox = true;

            settings.SettingsExport.ExportSelectedRowsOnly = true;
            settings.SettingsPager.Mode = GridViewPagerMode.ShowPager;
            settings.SettingsPager.PageSize = 100;

            settings.Columns.Add(column =>
            {
                column.FieldName = "ZapisnikId";
                column.Caption = "ID";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "BrojZapisnika";
                column.Caption = "Broj zapisnika";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "BrojZapisnikaMjesec";
                column.Caption = "Broj zapisnika mjesec";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "BrojZapisnikaGodina";
                column.Caption = "Broj zapisnika godina";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "DatumKreiranja";
                column.Caption = "Datum kreiranja";
                column.PropertiesEdit.DisplayFormatString = "dd.MM.yyyy";

            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "DatumBrisanja";
                column.Caption = "Datum brisanja";
                column.PropertiesEdit.DisplayFormatString = "dd.MM.yyyy";

            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "DatumZapisnika";
                column.Caption = "Datum zapisnika";
                column.PropertiesEdit.DisplayFormatString = "dd.MM.yyyy";

            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Validan";
                column.Caption = "Validan";
                column.ColumnType = MVCxGridViewColumnType.CheckBox;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Napomena";
                column.Caption = "Napomena";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Fakturisano";
                column.Caption = "Fakturisano";
                column.ColumnType = MVCxGridViewColumnType.CheckBox;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "BrojFakture";
                column.Caption = "Broj fakture";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "DokumentacijaPrisutna";
                column.Caption = "Dokumentacija";
                column.ColumnType = MVCxGridViewColumnType.CheckBox;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "LokacijaHidranata";
                column.Caption = "Lokacija hidranata";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "HidrantPrikljucenNa";
                column.Caption = "Hidrant priključen na";
            });



            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.Landscape = true;

            settings.SettingsExport.PageHeader.Center = "Powered by dostah";
            settings.SettingsExport.ReportHeader = "Vatrositemi";

            settings.CommandColumn.SelectAllCheckboxMode = GridViewSelectAllCheckBoxMode.Page;

            settings.Settings.ShowFilterRowMenu = true;
            settings.CommandColumn.ShowClearFilterButton = true;
            settings.SettingsPager.EnableAdaptivity = true;

            settings.CommandColumn.ShowNewButton = false;
            settings.CommandColumn.ShowDeleteButton = false;
            settings.CommandColumn.ShowEditButton = false;

            settings.Settings.ShowFilterRow = true;

            //settings.HtmlDataCellPrepared = (s, e) =>
            //{
            //    if (e.DataColumn.FieldName != "BrojAparata") return;

            //    if (Convert.ToInt32(e.CellValue) > 0)
            //    {
            //        e.Row.BackColor = System.Drawing.Color.Red;
            //        //e.Cell.ForeColor = System.Drawing.Color.White;
            //    }


            //};

            settings.HtmlRowPrepared = (s, e) =>
            {
                if (e.RowType != GridViewRowType.Data) return;

                bool fakturisano = Convert.ToBoolean(e.GetValue("Fakturisano").ToString());

                if (!fakturisano)
                {
                    e.Row.BackColor = System.Drawing.Color.Red;
                    //e.Cell.ForeColor = System.Drawing.Color.White;
                }


            };

            settings.Columns.Add(s =>
            {
                s.FieldName = "edit";
                s.Caption = "Akcije";
                s.Width = 70;
                s.Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;

                s.SetDataItemTemplateContent(c =>
                {
                    var keyValue = DataBinder.Eval(c.DataItem, "ZapisnikId");

                    htmlHelper.ViewContext.Writer.Write(String.Format("<a href='{0}'><i class='fa fa-pencil-square-o edit-icon'></i></a>", DevExpressHelper.GetUrl(new { Controller = "Zapisnik", Action = "Edit", id = keyValue })));
                    htmlHelper.ViewContext.Writer.Write(String.Format("<a style='margin-left: 10px;' class='delete-row' href='#' data-ref='{0}'><i class='fa fa-trash-o delete-icon'></i></a>", DevExpressHelper.GetUrl(new { Controller = "Zapisnik", Action = "Delete", id = keyValue })));
                });
            });

            return settings;
        }

        public static GridViewSettings GetEvidencijskaKarticaView(this HtmlHelper htmlHelper)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "evidencijskaKarticaGrid";
            settings.CallbackRouteValues = new { Controller = "EvidencijskaKartica", Action = "EvidencijskaKarticaGridView" };
            settings.Width = System.Web.UI.WebControls.Unit.Percentage(100);

            settings.KeyFieldName = "EvidencijskaKarticaId";

            settings.Settings.ShowFilterRow = true;

            settings.CommandColumn.Visible = true;
            settings.CommandColumn.ShowSelectCheckbox = true;

            settings.SettingsExport.ExportSelectedRowsOnly = true;
            settings.SettingsPager.Mode = GridViewPagerMode.ShowPager;
            settings.SettingsPager.PageSize = 100;

            settings.Columns.Add(column =>
            {
                column.FieldName = "EvidencijskaKarticaId";
                column.Caption = "ID";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "BrojEvidencijskeKartice";
                column.Caption = "Broj kartice";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "EvidencijskaKarticaTip.EvidencijskaKarticaTipNaziv";
                column.Caption = "Tip";

                column.ColumnType = MVCxGridViewColumnType.ComboBox;
                var comboBoxProperties = column.PropertiesEdit as ComboBoxProperties;
                comboBoxProperties.DataSource = FireSys.Helpers.DataProvider.GetEvidencijskeKarticeTip();
                comboBoxProperties.TextField = "Naziv";
                comboBoxProperties.ValueField = "Naziv";
                comboBoxProperties.ValueType = typeof(string);
                comboBoxProperties.DropDownStyle = DropDownStyle.DropDown;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Validna";
                column.Caption = "Validna";
                column.ColumnType = MVCxGridViewColumnType.CheckBox;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Obrisana";
                column.Caption = "Obrisana";
                column.ColumnType = MVCxGridViewColumnType.CheckBox;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "DatumZaduzenja";
                column.Caption = "Datum zaduzenja";
                column.PropertiesEdit.DisplayFormatString = "dd.MM.yyyy";

            });

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.Landscape = true;

            settings.SettingsExport.PageHeader.Center = "Evidencijske kartice";
            settings.SettingsExport.ReportHeader = "Vatrositemi";

            settings.CommandColumn.SelectAllCheckboxMode = GridViewSelectAllCheckBoxMode.Page;

            settings.Settings.ShowFilterRowMenu = true;
            settings.CommandColumn.ShowClearFilterButton = true;
            settings.SettingsPager.EnableAdaptivity = true;

            settings.CommandColumn.ShowNewButton = false;
            settings.CommandColumn.ShowDeleteButton = false;
            settings.CommandColumn.ShowEditButton = false;

            settings.Settings.ShowFilterRow = true;

            settings.Columns.Add(s =>
            {
                s.FieldName = "edit";
                s.Caption = "Akcije";
                s.Width = 70;
                s.Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;

                s.SetDataItemTemplateContent(c =>
                {
                    var keyValue = DataBinder.Eval(c.DataItem, "EvidencijskaKarticaId");

                    htmlHelper.ViewContext.Writer.Write(String.Format("<a href='{0}'><i class='fa fa-pencil-square-o edit-icon'></i></a>", DevExpressHelper.GetUrl(new { Controller = "EvidencijskaKartica", Action = "Edit", id = keyValue })));
                    htmlHelper.ViewContext.Writer.Write(String.Format("<a style='margin-left: 10px;' class='delete-row' href='#' data-ref='{0}'><i class='fa fa-trash-o delete-icon'></i></a>", DevExpressHelper.GetUrl(new { Controller = "EvidencijskaKartica", Action = "Delete", id = keyValue })));
                });
            });

            return settings;
        }

        public static GridViewSettings GetLokacijeView(this HtmlHelper htmlHelper)
        {
            var settings = new GridViewSettings();

            settings.Name = "lokacijaGrid";
            settings.CallbackRouteValues = new { Controller = "Lokacija", Action = "LokacijaGridView" };
            settings.Width = System.Web.UI.WebControls.Unit.Percentage(100);

            settings.KeyFieldName = "LokacijaId";

            settings.Settings.ShowFilterRow = true;

            settings.CommandColumn.Visible = true;
            settings.CommandColumn.ShowSelectCheckbox = true;

            settings.SettingsExport.ExportSelectedRowsOnly = true;
            settings.SettingsPager.Mode = GridViewPagerMode.ShowPager;
            settings.SettingsPager.PageSize = 100;

            settings.Columns.Add(column =>
            {
                column.FieldName = "LokacijaId";
                column.Caption = "ID";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Naziv";
                column.Caption = "Naziv";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Adresa";
                column.Caption = "Adresa";
            });

            //MjestoId
            settings.Columns.Add(column =>
            {
                column.FieldName = "Mjesto.Naziv";
                column.Caption = "Mjesto";

                column.ColumnType = MVCxGridViewColumnType.ComboBox;
                var comboBoxProperties = column.PropertiesEdit as ComboBoxProperties;
                comboBoxProperties.DataSource = FireSys.Helpers.DataProvider.GetMjesta();
                comboBoxProperties.TextField = "Naziv";
                comboBoxProperties.ValueField = "Naziv";
                comboBoxProperties.ValueType = typeof(string);
                comboBoxProperties.DropDownStyle = DropDownStyle.DropDown;
            });

            //RegijaId
            settings.Columns.Add(column =>
            {
                column.FieldName = "Regija.Naziv";
                column.Caption = "Regija";

                column.ColumnType = MVCxGridViewColumnType.ComboBox;
                var comboBoxProperties = column.PropertiesEdit as ComboBoxProperties;
                comboBoxProperties.DataSource = FireSys.Helpers.DataProvider.GetRegions();
                comboBoxProperties.TextField = "Naziv";
                comboBoxProperties.ValueField = "Naziv";
                comboBoxProperties.ValueType = typeof(string);
                comboBoxProperties.DropDownStyle = DropDownStyle.DropDown;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Kontakt";
                column.Caption = "Kontakt";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Komentar";
                column.Caption = "Komentar";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "DatumKreiranja";
                column.Caption = "Datum kreiranja";
                column.PropertiesEdit.DisplayFormatString = "dd.MM.yyyy";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "KorisnikKreiraoId";
                column.Caption = "Kreirao";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "DatumBrisanja";
                column.Caption = "Datum brisanja";
                column.PropertiesEdit.DisplayFormatString = "dd.MM.yyyy";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Obrisano";
                column.Caption = "Obrisano";
                column.ColumnType = MVCxGridViewColumnType.CheckBox;
            });

            //KlijentId
            settings.Columns.Add(column =>
            {
                column.FieldName = "Klijent.Naziv";
                column.Caption = "Klijent";

                column.ColumnType = MVCxGridViewColumnType.ComboBox;
                var comboBoxProperties = column.PropertiesEdit as ComboBoxProperties;
                comboBoxProperties.DataSource = FireSys.Helpers.DataProvider.GetClients();
                comboBoxProperties.TextField = "Naziv";
                comboBoxProperties.ValueField = "Naziv";
                comboBoxProperties.ValueType = typeof(string);
                comboBoxProperties.DropDownStyle = DropDownStyle.DropDown;
            });

            //LokacijaVrstaId
            settings.Columns.Add(column =>
            {
                column.FieldName = "LokacijaVrsta.Naziv";
                column.Caption = "Lokacija";

                column.ColumnType = MVCxGridViewColumnType.ComboBox;
                var comboBoxProperties = column.PropertiesEdit as ComboBoxProperties;
                comboBoxProperties.DataSource = FireSys.Helpers.DataProvider.GetLokacijaVrsta();
                comboBoxProperties.TextField = "Naziv";
                comboBoxProperties.ValueField = "Naziv";
                comboBoxProperties.ValueType = typeof(string);
                comboBoxProperties.DropDownStyle = DropDownStyle.DropDown;
            });


            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.Landscape = true;

            settings.SettingsExport.PageHeader.Center = "Powered by dostah";
            settings.SettingsExport.ReportHeader = "Vatrositemi";

            settings.CommandColumn.SelectAllCheckboxMode = GridViewSelectAllCheckBoxMode.Page;

            settings.Settings.ShowFilterRowMenu = true;
            settings.CommandColumn.ShowClearFilterButton = true;
            settings.SettingsPager.EnableAdaptivity = true;

            settings.CommandColumn.ShowNewButton = false;
            settings.CommandColumn.ShowDeleteButton = false;
            settings.CommandColumn.ShowEditButton = false;

            settings.Settings.ShowFilterRow = true;

            settings.Columns.Add(s =>
            {
                s.FieldName = "edit";
                s.Caption = "Akcije";
                s.Width = 70;
                s.Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;

                s.SetDataItemTemplateContent(c =>
                {
                    var keyValue = DataBinder.Eval(c.DataItem, "LokacijaId");
                    htmlHelper.ViewContext.Writer.Write(String.Format("<a href='{0}'><i class='fa fa-pencil-square-o edit-icon'></i></a>", DevExpressHelper.GetUrl(new { Controller = "Lokacija", Action = "Edit", id = keyValue })));
                    htmlHelper.ViewContext.Writer.Write(String.Format("<a style='margin-left: 10px;' class='delete-row' href='#' data-ref='{0}'><i class='fa fa-trash-o delete-icon'></i></a>", DevExpressHelper.GetUrl(new { Controller = "Lokacija", Action = "Delete", id = keyValue })));

                    //string imageEditUrl = "/Content/img/edit.png";
                    //string imageDeleteUrl = "/Content/img/delete.png";
                    //htmlHelper.ViewContext.Writer.Write(String.Format("<a href='{0}'><img src=\"{1}\" /></a>", DevExpressHelper.GetUrl(new { Controller = "Lokacija", Action = "Edit", id = keyValue }), imageEditUrl));
                    //htmlHelper.ViewContext.Writer.Write(String.Format("<a style='margin-left: 10px;' class='delete-row' href='{0}' onclick='return deleteRow()'><img src=\"{1}\" /></a>", DevExpressHelper.GetUrl(new { Controller = "Hidrants", Action = "Delete", id = keyValue }), imageDeleteUrl));
                    //htmlHelper.ViewContext.Writer.Write(String.Format("<a style='margin-left: 10px;' class='delete-row' href='#' data-ref='{0}'><img src=\"{1}\" /></a>", DevExpressHelper.GetUrl(new { Controller = "Lokacija", Action = "Delete", id = keyValue }), imageDeleteUrl));
                });
            });

            return settings;
        }

        public static GridViewSettings GetKlijentiView(this HtmlHelper htmlHelper)
        {
            var settings = new GridViewSettings();

            settings.Name = "klijentGrid";
            settings.CallbackRouteValues = new { Controller = "Klijenti", Action = "ClientGridView" };
            settings.Width = System.Web.UI.WebControls.Unit.Percentage(100);

            settings.KeyFieldName = "KlijentId";

            settings.Settings.ShowFilterRow = true;

            settings.CommandColumn.Visible = true;
            settings.CommandColumn.ShowSelectCheckbox = true;

            settings.SettingsExport.ExportSelectedRowsOnly = true;
            settings.SettingsPager.Mode = GridViewPagerMode.ShowPager;
            settings.SettingsPager.PageSize = 100;

            settings.Columns.Add(column =>
            {
                column.FieldName = "KlijentId";
                column.Caption = "ID";
                column.Visible = false;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Naziv";
                column.Caption = "Naziv";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Kontakt";
                column.Caption = "Kontakt";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "DatumKreiranja";
                column.Caption = "Datum kreiranja";
                column.PropertiesEdit.DisplayFormatString = "dd.MM.yyyy";

            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Obrisano";
                column.Caption = "Obrisano";
                column.ColumnType = MVCxGridViewColumnType.CheckBox;
                column.Visible = false;

            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "DatumBrisanja";
                column.Caption = "Datum brisanja";
                column.PropertiesEdit.DisplayFormatString = "dd.MM.yyyy";
                column.Visible = false;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "KorisnikKreiraoId";
                column.Caption = "Kreirao";
                column.Visible = false;
            });

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.Landscape = true;

            settings.SettingsExport.PageHeader.Center = "Powered by dostah";
            settings.SettingsExport.ReportHeader = "Vatrositemi - Klijenti";

            settings.CommandColumn.SelectAllCheckboxMode = GridViewSelectAllCheckBoxMode.Page;

            settings.Settings.ShowFilterRowMenu = true;
            settings.CommandColumn.ShowClearFilterButton = true;
            settings.SettingsPager.EnableAdaptivity = true;

            settings.CommandColumn.ShowNewButton = false;
            settings.CommandColumn.ShowDeleteButton = false;
            settings.CommandColumn.ShowEditButton = false;

            settings.Settings.ShowFilterRow = true;

            settings.Columns.Add(s =>
            {
                s.FieldName = "edit";
                s.Caption = "Akcije";
                s.Width = 70;
                s.Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;

                s.SetDataItemTemplateContent(c =>
                {
                    var keyValue = DataBinder.Eval(c.DataItem, "KlijentId");

                    htmlHelper.ViewContext.Writer.Write(String.Format("<a href='{0}'><i class='fa fa-pencil-square-o edit-icon'></i></a>", DevExpressHelper.GetUrl(new { Controller = "Klijenti", Action = "Edit", id = keyValue })));
                    htmlHelper.ViewContext.Writer.Write(String.Format("<a style='margin-left: 10px;' class='delete-row' href='#' data-ref='{0}'><i class='fa fa-trash-o delete-icon'></i></a>", DevExpressHelper.GetUrl(new { Controller = "Klijenti", Action = "Delete", id = keyValue })));
                });
            });

            return settings;

        }

        public static GridViewSettings GetAparatGridView(this HtmlHelper htmlHelper)
        {

            var settings = new GridViewSettings();

            settings.Name = "zapisnikGrid";
            settings.CallbackRouteValues = new { Controller = "VatrogasniAparat", Action = "AparatGridView" };
            settings.Width = System.Web.UI.WebControls.Unit.Percentage(100);

            settings.KeyFieldName = "VatrogasniAparatId";

            settings.Settings.ShowFilterRow = true;

            settings.CommandColumn.Visible = true;
            settings.CommandColumn.ShowSelectCheckbox = true;

            settings.SettingsExport.ExportSelectedRowsOnly = true;
            settings.SettingsPager.Mode = GridViewPagerMode.ShowPager;
            settings.SettingsPager.PageSize = 100;

            settings.Columns.Add(column =>
            {
                column.FieldName = "VatrogasniAparatId";
                column.Caption = "ID";
            });

            //VatrogasniAparatTipId
            settings.Columns.Add(column =>
            {
                column.FieldName = "VatrogasniAparatTip.Naziv";
                column.Caption = "Tip";

                column.ColumnType = MVCxGridViewColumnType.ComboBox;
                var comboBoxProperties = column.PropertiesEdit as ComboBoxProperties;
                comboBoxProperties.DataSource = FireSys.Helpers.DataProvider.GetTipAparata();
                comboBoxProperties.TextField = "Naziv";
                comboBoxProperties.ValueField = "Naziv";
                comboBoxProperties.ValueType = typeof(string);
                comboBoxProperties.DropDownStyle = DropDownStyle.DropDown;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "BrojaAparata";
                column.Caption = "Broj aparata";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "GodinaProizvodnje";
                column.Caption = "Godina proizvodnje";
            });

            //EvidencijskaKarticaId
            settings.Columns.Add(column =>
            {
                column.FieldName = "EvidencijskaKartica.Broj";
                column.Caption = "EK broj";

                column.ColumnType = MVCxGridViewColumnType.ComboBox;
                var comboBoxProperties = column.PropertiesEdit as ComboBoxProperties;
                comboBoxProperties.DataSource = FireSys.Helpers.DataProvider.GetEvidencijskeKartice();
                comboBoxProperties.TextField = "Broj";
                comboBoxProperties.ValueField = "Broj";
                comboBoxProperties.ValueType = typeof(string);
                comboBoxProperties.DropDownStyle = DropDownStyle.DropDown;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Napomena";
                column.Caption = "Napomena";
            });

            //LokacijaId
            settings.Columns.Add(column =>
            {
                column.FieldName = "Lokacija.Naziv";
                column.Caption = "Lokacija";

                column.ColumnType = MVCxGridViewColumnType.ComboBox;
                var comboBoxProperties = column.PropertiesEdit as ComboBoxProperties;
                comboBoxProperties.DataSource = FireSys.Helpers.DataProvider.GetLocations();
                comboBoxProperties.TextField = "Naziv";
                comboBoxProperties.ValueField = "Naziv";
                comboBoxProperties.ValueType = typeof(string);
                comboBoxProperties.DropDownStyle = DropDownStyle.DropDown;
            });

            //IspravnostId
            settings.Columns.Add(column =>
            {
                column.FieldName = "Ispravnost.Naziv";
                column.Caption = "Ispravnost";

                column.ColumnType = MVCxGridViewColumnType.ComboBox;
                var comboBoxProperties = column.PropertiesEdit as ComboBoxProperties;
                comboBoxProperties.DataSource = FireSys.Helpers.DataProvider.GetIspravnost();
                comboBoxProperties.TextField = "Naziv";
                comboBoxProperties.ValueField = "Naziv";
                comboBoxProperties.ValueType = typeof(string);
                comboBoxProperties.DropDownStyle = DropDownStyle.DropDown;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "IspitivanjeVrijediDo";
                column.Caption = "Ispitivanje vrijedi do";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "DatumKreiranja";
                column.Caption = "Datum kreiranja";
                column.PropertiesEdit.DisplayFormatString = "dd.MM.yyyy";

            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Obrisan";
                column.Caption = "Obrisan";
                column.ColumnType = MVCxGridViewColumnType.CheckBox;
            });

            //*Kreirao KorisnikKreiraoId

            //VatrogasniAparatVrstaId
            settings.Columns.Add(column =>
            {
                column.FieldName = "VatrogasniAparatVrsta.Naziv";
                column.Caption = "Vrsta aparata";

                column.ColumnType = MVCxGridViewColumnType.ComboBox;
                var comboBoxProperties = column.PropertiesEdit as ComboBoxProperties;
                comboBoxProperties.DataSource = FireSys.Helpers.DataProvider.GetVrstaAparata();
                comboBoxProperties.TextField = "Naziv";
                comboBoxProperties.ValueField = "Naziv";
                comboBoxProperties.ValueType = typeof(string);
                comboBoxProperties.DropDownStyle = DropDownStyle.DropDown;
            });



            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.Landscape = true;

            settings.SettingsExport.PageHeader.Center = "Powered by dostah";
            settings.SettingsExport.ReportHeader = "Vatrositemi";

            settings.CommandColumn.SelectAllCheckboxMode = GridViewSelectAllCheckBoxMode.Page;

            settings.Settings.ShowFilterRowMenu = true;
            settings.CommandColumn.ShowClearFilterButton = true;
            settings.SettingsPager.EnableAdaptivity = true;

            settings.CommandColumn.ShowNewButton = false;
            settings.CommandColumn.ShowDeleteButton = false;
            settings.CommandColumn.ShowEditButton = false;

            settings.Settings.ShowFilterRow = true;

            settings.Columns.Add(s =>
            {
                s.FieldName = "edit";
                s.Caption = "Akcije";
                s.Width = 70;
                s.Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;

                s.SetDataItemTemplateContent(c =>
                {
                    var keyValue = DataBinder.Eval(c.DataItem, "VatrogasniAparatId");

                    htmlHelper.ViewContext.Writer.Write(String.Format("<a href='{0}'><i class='fa fa-pencil-square-o edit-icon'></i></a>", DevExpressHelper.GetUrl(new { Controller = "VatrogasniAparat", Action = "Edit", id = keyValue })));
                    htmlHelper.ViewContext.Writer.Write(String.Format("<a style='margin-left: 10px;' class='delete-row' href='#' data-ref='{0}'><i class='fa fa-trash-o delete-icon'></i></a>", DevExpressHelper.GetUrl(new { Controller = "VatrogasniAparat", Action = "Delete", id = keyValue })));
                });
            });

            return settings;

        }

        public static string RenderRazorViewToString(this Controller controller, string viewName, object model, string htmlFieldPrefix = "")
        {
            controller.ViewData.Model = model;
            controller.ViewData.TemplateInfo = new TemplateInfo { HtmlFieldPrefix = htmlFieldPrefix };

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(controller.ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}