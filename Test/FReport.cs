using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Feuchte_Rapport
{
    public partial class FReport : Form
    {
        string _plant;
        SqlDataAdapter sqlData;
        LaPassDataSet datSet;
        ReportDataSource reportDataSource1 = new ReportDataSource();
        SqlConnection mainConnection;

        public FReport(string Plant, SqlConnection _mainConnection)
        {
            InitializeComponent();
            _plant = Plant;
            //datSet = new LaPassDataSet();
            
            dtTiPickerAb.Format = DateTimePickerFormat.Short;
            dtTiPickerBis.Format = DateTimePickerFormat.Short;

            dtTiPickerAb.Value = DateTime.Now.Date.AddDays(-7);
            dtTiPickerBis.Value = DateTime.Now.Date;
            mainConnection = _mainConnection;
        }

        private void FReport_Load(object sender, EventArgs e)
        {
            if (_plant == "Sewage")
            {
                radBtnHumidity.Visible = false;
                radBtnRapport.Visible = false;
            }
            this.reportViewer1.RefreshReport();
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            radBtnRapport.Checked = true;
            FillData();
        }

        void FillData()
        {
            datSet = new LaPassDataSet();
            //datSet.BeginInit();
            this.reportViewer1.LocalReport.DataSources.Clear();    

            if ( _plant == "PCI" && radBtnRapport.Checked ==true)
            {
                reportDataSource1 = new ReportDataSource();
                reportDataSource1.Name = "DatSetLaPass";
                reportDataSource1.Value = datSet.PCI_Maintenance;

                this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
                this.reportViewer1.LocalReport.ReportPath = "PCI_Maintenance.rdlc";

                //datSet.EndInit();
                if (txtBoxFilter.Text == "")
                    sqlData = new SqlDataAdapter("select [date], [Time], [shift], remark, reporter from PCI_Maintenance where date >= '" + dtTiPickerAb.Value.ToString("yyyy-MM-dd") + "' and date <= '" + dtTiPickerBis.Value.ToString("yyyy-MM-dd") + "' order by date desc, time desc", mainConnection);
                else
                    sqlData = new SqlDataAdapter("select [date], [Time], [shift], remark, reporter from PCI_Maintenance where date >= '" + dtTiPickerAb.Value.ToString("yyyy-MM-dd") + "' and date <= '" + dtTiPickerBis.Value.ToString("yyyy-MM-dd") + "' and Remark like '%" + txtBoxFilter.Text + "%' order by date desc, time desc", mainConnection);

                sqlData.Fill(datSet, "PCI_Maintenance");
                this.reportViewer1.RefreshReport();          
            }

            if (_plant == "PCI" && radBtnHumidity.Checked == true)
            {
                reportDataSource1 = new ReportDataSource();
                reportDataSource1.Name = "LaPassDataSet";
                reportDataSource1.Value = datSet.Humidity;

                this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
                this.reportViewer1.LocalReport.ReportPath = "Report1.rdlc";

                //datSet.EndInit();
                sqlData = new SqlDataAdapter("SELECT TOP 1000 [HumidityID],[ID],[DateTime],[Korngroeße60] ,[Korngroeße90],[Humidity] FROM [LaPass].[dbo].[Humidity] where [DateTime] between '" + dtTiPickerAb.Value.ToString("yyyy-MM-dd") + "' and '" + dtTiPickerBis.Value.ToString("yyyy-MM-dd") + "' and Transfered = 1 order by DateTime desc", mainConnection);

                sqlData.Fill(datSet, "Humidity");
                this.reportViewer1.RefreshReport();
            }


            if (_plant == "Sewage")
            {
                reportDataSource1 = new ReportDataSource();
                reportDataSource1.Name = "DatSetLaPass";
                reportDataSource1.Value = datSet.PCI_Maintenance;

                this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
                this.reportViewer1.LocalReport.ReportPath = "PCI_Maintenance.rdlc";

                //datSet.EndInit();

                if (txtBoxFilter.Text == "")
                    sqlData = new SqlDataAdapter("select [date], [Time], [shift], remark, reporter from Sewage_Maintenance where date between '" + dtTiPickerAb.Value.ToString("yyyy-MM-dd") + "' and '" + dtTiPickerBis.Value.ToString("yyyy-MM-dd") + "' order by date", mainConnection);
                else
                    sqlData = new SqlDataAdapter("select [date], [Time], [shift], remark, reporter from Sewage_Maintenance where date between '" + dtTiPickerAb.Value.ToString("yyyy-MM-dd") + "' and '" + dtTiPickerBis.Value.ToString("yyyy-MM-dd") + "' and Remark like '%" + txtBoxFilter.Text + "%' order by date", mainConnection);
                
                sqlData.Fill(datSet, "PCI_Maintenance");

                this.reportViewer1.RefreshReport();  
            }
            if (_plant == "Crane")
            {
                reportDataSource1 = new ReportDataSource();
                reportDataSource1.Name = "DatSetLaPass";
                reportDataSource1.Value = datSet.PCI_Maintenance;

                this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
                this.reportViewer1.LocalReport.ReportPath = "PCI_Maintenance.rdlc";

                //datSet.EndInit();

                if (txtBoxFilter.Text == "")
                    sqlData = new SqlDataAdapter("select [date], [Time], [shift], remark, reporter from Crane_Maintenance where date between '" + dtTiPickerAb.Value.ToString("yyyy-MM-dd") + "' and '" + dtTiPickerBis.Value.ToString("yyyy-MM-dd") + "' order by date", mainConnection);
                else
                    sqlData = new SqlDataAdapter("select [date], [Time], [shift], remark, reporter from Crane_Maintenance where date between '" + dtTiPickerAb.Value.ToString("yyyy-MM-dd") + "' and '" + dtTiPickerBis.Value.ToString("yyyy-MM-dd") + "' and Remark like '%" + txtBoxFilter.Text + "%' order by date", mainConnection);

                sqlData.Fill(datSet, "PCI_Maintenance");

                this.reportViewer1.RefreshReport();
            }
        }

        public void dtTiPicker_CloseUp(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            FillData();
        }

        private void radBtnHumidity_CheckedChanged(object sender, EventArgs e)
        {
            if( radBtnHumidity.Checked == true)
                radBtnRapport.Checked = false;
            else
                radBtnRapport.Checked = true;
        }

        private void radBtnRapport_CheckedChanged(object sender, EventArgs e)
        {
            if (radBtnRapport.Checked == true)
                radBtnHumidity.Checked = false;
            else
                radBtnHumidity.Checked = true;
        }
    }
}
