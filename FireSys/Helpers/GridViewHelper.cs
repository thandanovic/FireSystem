using DevExpress.Web;
using DevExpress.Web.Mvc;
using DevExpress.Web.Mvc.UI;
using System;
using System.Collections.Generic;
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
                s.Width = 50;
                //s.Settings.AllowHeaderFilter = DevExpress.Utils.DefaultBoolean.False;
                s.Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;
                

                s.SetDataItemTemplateContent(c =>
                {
                    var keyValue = DataBinder.Eval(c.DataItem, "HidrantId");

                    string imageEditUrl = "/Content/img/edit.png";
                    string imageDeleteUrl = "/Content/img/delete.png";

                    htmlHelper.ViewContext.Writer.Write(String.Format("<a href='{0}'><img src=\"{1}\" /></a>", DevExpressHelper.GetUrl(new { Controller = "Hidrants", Action = "Edit", id = keyValue }), imageEditUrl));
                    //htmlHelper.ViewContext.Writer.Write(String.Format("<a style='margin-left: 10px;' class='delete-row' href='{0}' onclick='return deleteRow()'><img src=\"{1}\" /></a>", DevExpressHelper.GetUrl(new { Controller = "Hidrants", Action = "Delete", id = keyValue }), imageDeleteUrl));
                    htmlHelper.ViewContext.Writer.Write(String.Format("<a style='margin-left: 10px;' class='delete-row' href='#' data-ref='{0}'><img src=\"{1}\" /></a>", DevExpressHelper.GetUrl(new { Controller = "Hidrants", Action = "Delete", id = keyValue }), imageDeleteUrl));
                });
            });

            return settings;
        }

    }
}