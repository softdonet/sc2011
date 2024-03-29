﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Scada.Client.VM.Modules.Query;
using Telerik.Windows.Controls;
using System.IO;

namespace Scada.Client.SL.Modules.Query
{
    public partial class UserEventQuery : UserControl
    {
        private static UserEventQuery instance;
        public static UserEventQuery GetInstance()
        {
            if (instance==null)
            {
                instance = new UserEventQuery();
            }
            return instance;
        }

        UserEventQueryViewModel userEventQueryViewModel;
        private UserEventQuery()
        {
            InitializeComponent();
            userEventQueryViewModel = new UserEventQueryViewModel();
            this.DataContext = userEventQueryViewModel;
            userEventQueryViewModel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(userEventQueryViewModel_PropertyChanged);

        }

        void userEventQueryViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "DeviceTreeSource")
            {
                this.comboBoxTreeView1.Source = userEventQueryViewModel.DeviceTreeSource;
            }
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            string extension = "xls"; ;
            ExportFormat format = ExportFormat.Html;
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.DefaultExt = extension;
            dialog.Filter = String.Format("{1} files (*.{0})|*.{0}|All files (*.*)|*.*", extension, "Excel");
            dialog.FilterIndex = 1;
            if (dialog.ShowDialog() == true)
            {
                using (Stream stream = dialog.OpenFile())
                {
                    GridViewExportOptions exportOptions = new GridViewExportOptions();
                    exportOptions.Format = format;
                    exportOptions.ShowColumnFooters = false;
                    exportOptions.ShowColumnHeaders = true;
                    exportOptions.ShowGroupFooters = true;
                    RadGridView1.Export(stream, exportOptions);
                }
            }
        }

        private void RadGridView1_Exporting(object sender, GridViewExportEventArgs e)
        {
            if (e.Element == ExportElement.HeaderRow)
            {
                e.FontWeight = FontWeights.Bold;
                e.TextAlignment = TextAlignment.Center;
                e.Background = Colors.Gray;
            }
        }

        
    }
}
