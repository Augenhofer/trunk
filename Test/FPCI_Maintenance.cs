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
    public partial class FPCI_Maintenance : Form
    {
        MainFormData mainData;
        public DataSet datSetRapport;
        BindingSource bnsRapport;
        SqlDataAdapter sqlDatMaintenance, sqlDatMaintenanceDummy;
        static string ab, bis;
        FNewEditMaintenance fNewMaintenance;
        SqlCommandBuilder maintenanceBuilder;
        SqlConnection mainConnection;
        string _analge;

        public FPCI_Maintenance(MainFormData data, string anlage)
        {
            InitializeComponent();
            mainData = data;
            _analge = anlage;
            datSetRapport = new DataSet();
            bnsRapport = new BindingSource();
            sqlDatMaintenance = new SqlDataAdapter();
            sqlDatMaintenanceDummy = new SqlDataAdapter();
            mainConnection = new SqlConnection();
        }

        private void FPCI_Maintenance_Load(object sender, EventArgs e)
        {
            dtTiPickerAb.Format = DateTimePickerFormat.Short;
            dtTiPickerBis.Format = DateTimePickerFormat.Short;

            dtTiPickerAb.Value = DateTime.Now.Date.AddDays(-7);
            dtTiPickerBis.Value = DateTime.Now.Date;

            ab = dtTiPickerAb.Value.ToString("yyyy-MM-dd");
            bis = dtTiPickerBis.Value.ToString("yyyy-MM-dd");

            mainConnection.ConnectionString = Properties.Settings.Default.LaPass_ConnectionString;

            FillData();
            if (mainData.groupID < 99)
            {
                btnDelete.Visible = false;
                btnEdit.Visible = false;
            }
            if (_analge == "PCI")
            {
                sqlDatMaintenanceDummy.SelectCommand = new SqlCommand("SELECT * FROM [LaPass].[dbo].[PCI_Maintenance]", mainConnection);
                labPCIRapportbuch.Text = "Rapportbuch - PCI";
            }
            else if (_analge == "Sewage")
            {
                sqlDatMaintenanceDummy.SelectCommand = new SqlCommand("SELECT * FROM [LaPass].[dbo].[Sewage_Maintenance]", mainConnection);
                labPCIRapportbuch.Text = "Rapportbuch Hochofen Wasserwärter";
            }
            else if (_analge == "Crane")
            {
                sqlDatMaintenanceDummy.SelectCommand = new SqlCommand("SELECT * FROM [LaPass].[dbo].[Crane_Maintenance]", mainConnection);
                labPCIRapportbuch.Text = "Rapportbuch Sandkran";
            }
            maintenanceBuilder = new SqlCommandBuilder(sqlDatMaintenanceDummy);
            maintenanceBuilder.ConflictOption = ConflictOption.CompareAllSearchableValues;
            sqlDatMaintenance.UpdateCommand = maintenanceBuilder.GetUpdateCommand();
            sqlDatMaintenance.DeleteCommand = maintenanceBuilder.GetDeleteCommand();
            sqlDatMaintenance.InsertCommand = maintenanceBuilder.GetInsertCommand();

            try
            {
                bnsRapport.DataSource = datSetRapport;
                bnsRapport.DataMember = "Maintenance";
            }
            catch
            {
            }

            dgvRapport.DataSource = bnsRapport;
            dgvRapport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRapport.ReadOnly = true;
            dgvRapport.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
            dgvRapport.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvRapport.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dgvRapport.Columns["MaintenanceID"].Visible = false;
            dgvRapport.Columns["Date"].HeaderText = "Datum";
            dgvRapport.Columns["Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvRapport.Columns["Date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvRapport.Columns["Date"].FillWeight = 10; 
            dgvRapport.Columns["Time"].HeaderText = "Uhrzeit";
            dgvRapport.Columns["Time"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvRapport.Columns["Time"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvRapport.Columns["Time"].FillWeight = 10; 
            dgvRapport.Columns["Shift"].HeaderText = "Schicht";
            dgvRapport.Columns["Shift"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvRapport.Columns["Shift"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvRapport.Columns["Shift"].FillWeight = 10; 
            dgvRapport.Columns["Remark"].HeaderText = "Übergabe - Störung - Meldung";
            dgvRapport.Columns["Reporter"].HeaderText = "Gemeldet";
            dgvRapport.Columns["Reporter"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvRapport.Columns["Reporter"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvRapport.Columns["Reporter"].FillWeight = 10; 

            foreach (DataGridViewColumn c in dgvRapport.Columns)
            {
                c.DefaultCellStyle.Font = new Font("Arial",14F, GraphicsUnit.Pixel);
            }
        }

        private void FillData()
        {
            datSetRapport.Clear();
            string sqlStatement = "";

            if ( _analge == "PCI")
                if (txtBoxFilter.Text == "")
                    sqlStatement = "SELECT * FROM [LaPass].[dbo].[PCI_Maintenance] where Date >= '" + ab + "' and Date <= '" + bis + "' order by date desc, time desc";
                else
                    sqlStatement = "SELECT * FROM [LaPass].[dbo].[PCI_Maintenance] where Date >= '" + ab + "' and Date <= '" + bis + "' and Remark like '%" + txtBoxFilter.Text + "%'  order by date desc, time desc";

            if (_analge == "Sewage")
                if (txtBoxFilter.Text == "")
                    sqlStatement = "SELECT * FROM [LaPass].[dbo].[Sewage_Maintenance] where Date >= '" + ab + "' and Date <= '" + bis + "' order by date";
                else
                    sqlStatement = "SELECT * FROM [LaPass].[dbo].[Sewage_Maintenance] where Date >= '" + ab + "' and Date <= '" + bis + "' and Remark like '%" + txtBoxFilter.Text + "%' order by date";

            if (_analge == "Crane")
                if (txtBoxFilter.Text == "")
                    sqlStatement = "SELECT * FROM [LaPass].[dbo].[Crane_Maintenance] where Date >= '" + ab + "' and Date <= '" + bis + "' order by date";
                else
                    sqlStatement = "SELECT * FROM [LaPass].[dbo].[Crane_Maintenance] where Date >= '" + ab + "' and Date <= '" + bis + "' and Remark like '%" + txtBoxFilter.Text + "%' order by date";
                

            SqlCommand sqlCommand = new SqlCommand(sqlStatement, mainConnection);
            //sqlDatMaintenance = new System.Data.SqlClient.SqlDataAdapter();
            sqlDatMaintenance.SelectCommand = sqlCommand;

            try
            {
                sqlDatMaintenance.Fill(datSetRapport, "Maintenance");
            }
            catch
            {
            }
        }

        public void dtTiPicker_CloseUp(object sender, EventArgs e)
        {

        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            ab = dtTiPickerAb.Value.ToString("yyyy-MM-dd");
            bis = dtTiPickerBis.Value.ToString("yyyy-MM-dd");

            FillData();
        }

        private void btnNeu_Click(object sender, EventArgs e)
        {
            foreach (Form mdiChilds in this.MdiChildren)
            {
                if (mdiChilds.GetType() == typeof(FNewEditMaintenance))
                {
                    mdiChilds.Focus();
                    return;
                }
            }
            fNewMaintenance = new FNewEditMaintenance(this);
            fNewMaintenance.Show();
        }

        public void saveCahnges()
        {
            bnsRapport.EndEdit();
            sqlDatMaintenance.Update(datSetRapport, "Maintenance");
            dgvRapport.Refresh();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            DataRowView currentRow = (DataRowView)bnsRapport.Current;
            foreach (Form mdiChilds in this.MdiChildren)
            {
                if (mdiChilds.GetType() == typeof(FNewEditMaintenance))
                {
                    mdiChilds.Focus();
                    return;
                }
            }
            fNewMaintenance = new FNewEditMaintenance(this, false, currentRow);
            fNewMaintenance.Show();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Wollen Sie die ausgewählte Eintrag wirklich löschen?", "Inforamtion", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                DataRowView currentRow = (DataRowView)bnsRapport.Current;
                if (currentRow != null)
                {
                    for (int j = 0; j < datSetRapport.Tables["Maintenance"].Rows.Count; j++)
                    {
                        if (Convert.ToInt32(datSetRapport.Tables["Maintenance"].Rows[j]["MaintenanceID"]) == Convert.ToInt32(currentRow["MaintenanceID"]))
                        {
                            datSetRapport.Tables["Maintenance"].Rows[j].Delete();
                        }
                    }
                }
                saveCahnges();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
