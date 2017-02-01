using DevExpress.Web;
using DevExpress.Web.Mvc;
using DevExpress.Web.Mvc.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;
using System.Xml;

namespace FireSys.Helpers
{
    public class ColumnSettings
    {
        public bool IsReferential { get; set; }
        public bool IsDate { get; set; }
        public string FieldName { get; set; }
        public string Caption { get; set; }

        public bool Visible { get; set; }

        public string ColumnType { get; set; }
        public string DataSource { get; set; }
        public string TextField { get; set; }
        public string ValueField { get; set; }

        public string ValueType { get; set; }
        public string DropDownStyle { get; set; }

        public int Width { get; set; }
        public bool AllowAutoFilter { get; set; }
    }

    public class GridSettings
    {
        public string Name { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }

        public int WidthPercentage { get; set; }
        public string KeyFieldName { get; set; }
        public bool ShowFilterRow { get; set; }

        public bool CommandColumnVisible { get; set; }
        public bool ShowSelectCheckBox { get; set; }

        public bool ExportSelectedRowsOnly { get; set; }
        public string ExportPageHeaderCenter { get; set; }
        public string ExportReportHeader { get; set; }
        public bool ExportLandscape { get; set; }
        public string SelectAllCheckboxMode { get; set; }

        public bool ShowFilterRowMenu { get; set; }
        public bool ShowClearFilterButton { get; set; }
        public bool EnableAdaptivity { get; set; }
        public bool ShowNewButton { get; set; }
        public bool ShowDeleteButton { get; set; }
        public bool ShowEditButton { get; set; }

        public string PagerMode { get; set; }
        public int PageSize { get; set; }

        public string ActionFieldName { get; set; }
        public string ActionCaption { get; set; }
        public int ActionWidth { get; set; }
        public bool ActionShowEdit { get; set; }
        public string ActionEdit { get; set; }
        public bool ActionShowDelete { get; set; }
        public string ActionDelete { get; set; }
        public bool ActionShowPrint { get; set; }
        public string ActionPrint { get; set; }

        public List<ColumnSettings> Columns { get; set; }

        public GridSettings()
        {
            ShowFilterRowMenu = true;
            ShowClearFilterButton = true;
            EnableAdaptivity = true;
            ShowNewButton = false;
            ShowEditButton = false;
            ShowDeleteButton = false;
            ExportLandscape = true;

            ActionFieldName = "edit";
            ActionCaption = "Akcije";
            ActionWidth = 70;
            ActionShowEdit = true;
            ActionEdit = "Edit";
            ActionShowDelete = true;
            ActionDelete = "Delete";
            ActionShowPrint = false;
        }
    }

    public static class GridViewHelper
    {
        public static GridSettings GetConfiguration(string gridName)
        {
            GridSettings settings = new GridSettings();
            settings.Columns = new List<ColumnSettings>();
            ColumnSettings column = new ColumnSettings();

            XmlDocument document = new XmlDocument();
            string path = String.Format("~/Content/grids/{0}.xml", gridName);
            document.Load(HostingEnvironment.MapPath(path));
            PropertyInfo property = null;

            foreach (XmlNode node in document.DocumentElement.ChildNodes)
            {
                if (node.ChildNodes.Count > 1)
                {

                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        column = new ColumnSettings();
                        foreach (XmlNode cNode in childNode.ChildNodes)
                        {
                            property = column.GetType().GetProperty(cNode.Name);

                            if (null != property)
                            {
                                dynamic value = cNode.InnerText;

                                if (string.IsNullOrEmpty(value))
                                    continue;

                                if (property.PropertyType == typeof(int) || property.PropertyType == typeof(Int32))
                                {
                                    value = Convert.ToInt32(value);
                                }
                                else if (property.PropertyType == typeof(bool) || property.PropertyType == typeof(Boolean))
                                {
                                    value = Convert.ToBoolean(value);
                                }
                                property.SetValue(column, value, null);
                            }
                        }
                        settings.Columns.Add(column);
                    }
                }
                else
                {
                    property = settings.GetType().GetProperty(node.Name);

                    if (null != property)
                    {
                        dynamic value = node.InnerText;

                        if (string.IsNullOrEmpty(value))
                            continue;

                        if (property.PropertyType == typeof(int) || property.PropertyType == typeof(Int32))
                        {
                            value = Convert.ToInt32(value);
                        }
                        else if (property.PropertyType == typeof(bool) || property.PropertyType == typeof(Boolean))
                        {
                            value = Convert.ToBoolean(value);
                        }

                        property.SetValue(settings, value, null);
                    }
                }
            }

            return settings;
        }

        public static GridViewSettings GetGridSettings(string gridName, HtmlHelper htmlHelper)
        {
            try
            {

                GridViewSettings gridSettings = new GridViewSettings();
                Type provider = typeof(DataProvider);

                GridSettings settings = GetConfiguration(gridName);

                gridSettings.Name = settings.Name;
                gridSettings.CallbackRouteValues = new { Controller = settings.Controller, Action = settings.Action };
                gridSettings.Width = System.Web.UI.WebControls.Unit.Percentage(settings.WidthPercentage);
                gridSettings.KeyFieldName = settings.KeyFieldName;
                gridSettings.Settings.ShowFilterRow = settings.ShowFilterRow;

                gridSettings.CommandColumn.Visible = settings.CommandColumnVisible;
                gridSettings.CommandColumn.ShowSelectCheckbox = settings.ShowSelectCheckBox;

                gridSettings.SettingsExport.ExportSelectedRowsOnly = settings.ExportSelectedRowsOnly;

                gridSettings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
                gridSettings.SettingsExport.Landscape = settings.ExportLandscape;

                if (!string.IsNullOrEmpty(settings.ExportPageHeaderCenter))
                    gridSettings.SettingsExport.PageHeader.Center = settings.ExportPageHeaderCenter;

                if (!string.IsNullOrEmpty(settings.ExportReportHeader))
                    gridSettings.SettingsExport.ReportHeader = settings.ExportReportHeader;

                if (!string.IsNullOrEmpty(settings.SelectAllCheckboxMode))
                    gridSettings.CommandColumn.SelectAllCheckboxMode = (GridViewSelectAllCheckBoxMode)System.Enum.Parse(typeof(GridViewSelectAllCheckBoxMode), settings.SelectAllCheckboxMode);

                gridSettings.Settings.ShowFilterRowMenu = settings.ShowFilterRowMenu;
                gridSettings.CommandColumn.ShowClearFilterButton = settings.ShowClearFilterButton;
                gridSettings.SettingsPager.EnableAdaptivity = settings.EnableAdaptivity;

                gridSettings.CommandColumn.ShowNewButton = settings.ShowNewButton;
                gridSettings.CommandColumn.ShowDeleteButton = settings.ShowDeleteButton;
                gridSettings.CommandColumn.ShowEditButton = settings.ShowEditButton;

                gridSettings.ClientSideEvents.BeginCallback = "GridBeginCallback";
                gridSettings.ClientSideEvents.EndCallback = "GridEndCallback";

                gridSettings.ClientSideEvents.SelectionChanged = "OnSelectionChanged";
                

                if (!string.IsNullOrEmpty(settings.PagerMode))
                    gridSettings.SettingsPager.Mode = (GridViewPagerMode)System.Enum.Parse(typeof(GridViewPagerMode), settings.PagerMode);

                if (gridSettings.SettingsPager.Mode.ToString() == settings.PagerMode)
                    gridSettings.SettingsPager.PageSize = settings.PageSize;

                foreach (ColumnSettings column in settings.Columns)
                {
                    if (column.IsReferential)
                    {
                        gridSettings.Columns.Add(c =>
                        {
                            c.FieldName = column.FieldName;
                            c.Caption = column.Caption;
                            c.Visible = column.Visible;

                            c.ColumnType = (MVCxGridViewColumnType)System.Enum.Parse(typeof(MVCxGridViewColumnType), column.ColumnType);
                            if (column.ColumnType == "ComboBox")
                            {
                                ComboBoxProperties properties = c.PropertiesEdit as ComboBoxProperties;
                                properties.DataSource = provider.GetMethod(column.DataSource).Invoke(null, null);
                                properties.TextField = column.TextField;
                                properties.ValueField = column.ValueField;
                                properties.ValueType = Type.GetType(column.ValueType);
                                properties.DropDownStyle = (DropDownStyle)System.Enum.Parse(typeof(DropDownStyle), column.DropDownStyle);
                            }
                            else if (column.ColumnType == "TextBox")
                            {

                            }

                            if(column.Caption == "Klijent")
                            {
                                c.SortIndex = 0;
                                c.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                            }

                            if (column.Width > 0)
                                c.Width = column.Width;

                            c.Settings.AllowAutoFilter = (DevExpress.Utils.DefaultBoolean)System.Enum.Parse(typeof(DevExpress.Utils.DefaultBoolean), column.AllowAutoFilter.ToString());
                        });
                    }
                    else
                    {
                        gridSettings.Columns.Add(c =>
                        {
                            c.FieldName = column.FieldName;
                            c.Caption = column.Caption;
                            c.Visible = column.Visible;

                            if (column.IsDate || column.ColumnType == "DateEdit")
                            {
                                c.PropertiesEdit.DisplayFormatString = "dd.MM.yyyy";
                                //c.Settings.AutoFilterCondition = AutoFilterCondition.Greater;
                                c.Settings.AllowHeaderFilter = DevExpress.Utils.DefaultBoolean.True;
                                c.Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;
                                c.SettingsHeaderFilter.Mode = GridHeaderFilterMode.DateRangePicker;
                            }
                        });
                    }
                }

                gridSettings.HtmlRowPrepared = (s, e) =>
                {
                    if (e.RowType != GridViewRowType.Data) return;

                    //bool fakturisano = Convert.ToBoolean(e.GetValue("Fakturisano").ToString());
                    int status = Convert.ToInt32(e.GetValue("StatusId").ToString());

                    switch (status)
                    {
                        case 1: //u obradi - yellow
                            e.Row.BackColor = System.Drawing.Color.Yellow;
                            break;
                        case 2: //zavrsen
                            e.Row.BackColor = System.Drawing.Color.LightGreen;
                            //e.Row.ForeColor = System.Drawing.Color.White;
                            break;
                        case 4: //kreiran
                            break;                      
                    }
                };

                gridSettings.Columns.Add(s =>
                {
                    s.FieldName = settings.ActionFieldName;
                    s.Caption = settings.ActionCaption;
                    s.Width = settings.ActionWidth;
                    s.Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;
                    s.Visible = settings.ActionWidth != 0;

                    s.SetDataItemTemplateContent(c =>
                    {
                        var keyValue = DataBinder.Eval(c.DataItem, settings.KeyFieldName);
                        var status = DataBinder.Eval(c.DataItem, "StatusId");

                        if (settings.ActionShowEdit)
                        {
                            if(Convert.ToInt16(status) == 2)
                            {
                                htmlHelper.ViewContext.Writer.Write(String.Format("<a href='{0}'><i class='fa fa-eye edit-icon'></i></a>", DevExpressHelper.GetUrl(new { Controller = settings.Controller, Action = settings.ActionEdit, id = keyValue })));
                            }
                            else
                            {
                                htmlHelper.ViewContext.Writer.Write(String.Format("<a href='{0}'><i class='fa fa-pencil-square-o edit-icon'></i></a>", DevExpressHelper.GetUrl(new { Controller = settings.Controller, Action = settings.ActionEdit, id = keyValue })));
                            }
                        }
                        if (settings.ActionShowDelete)
                            htmlHelper.ViewContext.Writer.Write(String.Format("<a style='margin-left: 10px;' class='delete-row' href='#' data-ref='{0}'><i class='fa fa-trash-o delete-icon'></i></a>", DevExpressHelper.GetUrl(new { Controller = settings.Controller, Action = settings.ActionDelete, id = keyValue })));
                        if (settings.ActionShowPrint)
                            htmlHelper.ViewContext.Writer.Write(String.Format("<a href='#' class='print-report' style='margin-left: 10px;' data-id='{0}' data-url='{1}'><i class='fa fa-print edit-icon'></i></a>", keyValue, DevExpressHelper.GetUrl(new { Controller = settings.Controller, Action = settings.ActionPrint })));
                    });
                });

                return gridSettings;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static GridViewSettings GetHidrantsView(this HtmlHelper htmlHelper)
        {
            GridViewSettings settings = GetGridSettings("Hidranti", htmlHelper);
            return settings;
        }

        public static GridViewSettings GetHidrantsView1(this HtmlHelper htmlHelper)
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

        public static GridViewSettings GetRadniNalogDolazeciView(this HtmlHelper htmlHelper)
        {
            GridViewSettings settings = GetGridSettings("RadniNalogDolazeci", htmlHelper);
            return settings;
        }

        public static GridViewSettings GetRadniNalogView(this HtmlHelper htmlHelper)
        {
            GridViewSettings settings = GetGridSettings("RadniNalozi", htmlHelper);
            return settings;
        }

        public static GridViewSettings GetRadniNalogView1(this HtmlHelper htmlHelper)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "radniNalogGrid";
            settings.CallbackRouteValues = new { Controller = "RadniNalog", Action = "RadniNalogGridView" };
            settings.Width = System.Web.UI.WebControls.Unit.Percentage(100);

            settings.KeyFieldName = "RadniNalogId";

            settings.Settings.ShowFilterRow = true;

            settings.SettingsPager.Mode = GridViewPagerMode.ShowPager;
            settings.SettingsPager.PageSize = 100;
            settings.CommandColumn.Visible = true;
            settings.CommandColumn.ShowSelectCheckbox = true;

            settings.SettingsExport.ExportSelectedRowsOnly = true;
            settings.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;

            settings.Columns.Add(column =>
            {
                column.FieldName = "RadniNalogId";
                column.Caption = "ID";
                column.Visible = false;
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
                column.Visible = false;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "BrojHidranata";
                column.Caption = "Broj hidranata";
                column.Visible = false;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "BrojAparata";
                column.Caption = "Broj aparata";
                column.Visible = false;
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
            GridViewSettings settings = GetGridSettings("Zapisnici", htmlHelper);
            return settings;
        }

        public static GridViewSettings GetZapisnikView1(this HtmlHelper htmlHelper)
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

            settings.Columns.Add(column =>
            {
                column.FieldName = "Lokacija.Klijent.Naziv";
                column.Caption = "Klijent";

                column.ColumnType = MVCxGridViewColumnType.TextBox;
                //var comboBoxProperties = column.PropertiesEdit as ComboBoxProperties;
                //comboBoxProperties.DataSource = FireSys.Helpers.DataProvider.GetLocations();
                //comboBoxProperties.TextField = "Naziv";
                //comboBoxProperties.ValueField = "Naziv";
                //comboBoxProperties.ValueType = typeof(string);
                //comboBoxProperties.DropDownStyle = DropDownStyle.DropDown;
            });

            //settings.Columns.Add(col =>
            //{
            //    col.Caption = "Broj zapisnika";
            //    col.Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.True;
            //    col.SetDataItemTemplateContent(dataTemplate =>
            //    {
            //        htmlHelper.DevExpress().Label(label =>
            //        {
            //            label.Text = String.Format("{0}-{1}/{2}",
            //                DataBinder.Eval(dataTemplate.DataItem, "BrojZapisnika"),
            //                DataBinder.Eval(dataTemplate.DataItem, "BrojZapisnikaMjesec"),
            //                DataBinder.Eval(dataTemplate.DataItem, "BrojZapisnikaGodina"));
            //        }).Render();
            //    });
            //});

            settings.Columns.Add(column =>
            {
                column.FieldName = "FullBrojZapisnika";
                column.Caption = "Broj zapisnika";
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "BrojZapisnikaMjesec";
                column.Caption = "Broj zapisnika mjesec";
            });

            //settings.Columns.Add(column => {
            //    column.FieldName = "BrojZapisnika";
            //    column.Caption = "Veritas";
            //    column.UnboundType = DevExpress.Data.UnboundColumnType.String;
            //    //column.PropertiesEdit.DisplayFormatString = "c";
            //});

            //settings.CustomUnboundColumnData = (sender, e) =>
            //{
            //    if (e.Column.FieldName == "BrojZapisnika")
            //    {
            //        string broj = e.GetListSourceFieldValue("BrojZapisnika").ToString();
            //        string mjesec = e.GetListSourceFieldValue("BrojZapisnikaMjesec").ToString();
            //        e.Value = string.Format("{0}-{1}", broj, mjesec);
            //    }
            //};


            settings.Columns.Add(column =>
            {
                column.FieldName = "ZapisnikId";
                column.Caption = "ID";
                column.Visible = false;
            });

            //settings.Columns.Add(column =>
            //{
            //    column.FieldName = "BrojZapisnika";
            //    column.Caption = "Broj zapisnika";
            //});



            settings.Columns.Add(column =>
            {
                column.FieldName = "BrojZapisnikaGodina";
                column.Caption = "Broj zapisnika godina";
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
                column.FieldName = "DatumZapisnika";
                column.Caption = "Datum zapisnika";
                column.PropertiesEdit.DisplayFormatString = "dd.MM.yyyy";

            });

            //ZapisnikTipId
            settings.Columns.Add(column =>
            {
                column.FieldName = "ZapisnikTip.Naziv";
                column.Caption = "Tip";

                column.ColumnType = MVCxGridViewColumnType.ComboBox;
                var comboBoxProperties = column.PropertiesEdit as ComboBoxProperties;
                comboBoxProperties.DataSource = FireSys.Helpers.DataProvider.GetZapisnikTip();
                comboBoxProperties.TextField = "Naziv";
                comboBoxProperties.ValueField = "Naziv";
                comboBoxProperties.ValueType = typeof(string);
                comboBoxProperties.DropDownStyle = DropDownStyle.DropDown;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "DatumKreiranja";
                column.Caption = "Datum kreiranja";
                column.PropertiesEdit.DisplayFormatString = "dd.MM.yyyy";

            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Validan";
                column.Caption = "Validan";
                column.ColumnType = MVCxGridViewColumnType.CheckBox;
                column.Visible = false;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Napomena";
                column.Caption = "Napomena";
                column.Visible = false;
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
                column.Visible = false;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "LokacijaHidranata";
                column.Caption = "Lokacija hidranata";
                column.Visible = false;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "HidrantPrikljucenNa";
                column.Caption = "Hidrant priključen na";
                column.Visible = false;
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
                s.Width = 100;
                s.Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;

                s.SetDataItemTemplateContent(c =>
                {
                    var keyValue = DataBinder.Eval(c.DataItem, "ZapisnikId");

                    htmlHelper.ViewContext.Writer.Write(String.Format("<a href='{0}'><i class='fa fa-pencil-square-o edit-icon'></i></a>", DevExpressHelper.GetUrl(new { Controller = "Zapisnik", Action = "Edit", id = keyValue })));
                    htmlHelper.ViewContext.Writer.Write(String.Format("<a style='margin-left: 10px;' class='delete-row' href='#' data-ref='{0}'><i class='fa fa-trash-o delete-icon'></i></a>", DevExpressHelper.GetUrl(new { Controller = "Zapisnik", Action = "Delete", id = keyValue })));
                    htmlHelper.ViewContext.Writer.Write(String.Format("<a href='#' class='print-report' style='margin-left: 10px;' data-id='{0}' data-url='{1}'><i class='fa fa-print edit-icon'></i></a>", keyValue, DevExpressHelper.GetUrl(new { Controller = "Zapisnik", Action = "Report" })));
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
                s.Width = 0;
                s.Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;

                s.SetDataItemTemplateContent(c =>
                {
                    var keyValue = DataBinder.Eval(c.DataItem, "EvidencijskaKarticaId");

                    //htmlHelper.ViewContext.Writer.Write(String.Format("<a href='{0}'><i class='fa fa-pencil-square-o edit-icon'></i></a>", DevExpressHelper.GetUrl(new { Controller = "EvidencijskaKartica", Action = "Edit", id = keyValue })));
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