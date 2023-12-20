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
    public partial class FPCIInfo : Form
    {
        public DataSet datSetInformation;
        BindingSource bnsInformation;
        SqlDataAdapter sqlDatMaintenance, sqlDatMaintenanceDummy;
        FNewInfo fNewInfo;
        SqlCommandBuilder maintenanceBuilder;
        string _analge;
        SqlConnection mainConnection;

        public FPCIInfo(string anlage)
        {
            InitializeComponent();
            
            _analge = anlage;
            datSetInformation = new DataSet();
            bnsInformation = new BindingSource();
            sqlDatMaintenance = new SqlDataAdapter();
            sqlDatMaintenanceDummy = new SqlDataAdapter();
            mainConnection = new SqlConnection();
        }

        private void FPCIInfo_Load(object sender, EventArgs e)
        {
            mainConnection.ConnectionString = Properties.Settings.Default.LaPass_ConnectionString;

            FillData();
            //if (mainData.groupID < 99)
            //{
            //    btnDelete.Visible = false;
            //    btnEdit.Visible = false;
            //}
            if (_analge == "PCI")
            {
                sqlDatMaintenanceDummy.SelectCommand = new SqlCommand("SELECT * FROM [LaPass].[dbo].[PCI_Information]", mainConnection);
                labPCIInformation.Text = "PCI - Information";
            }
            else if (_analge == "Sewage")
            {
                sqlDatMaintenanceDummy.SelectCommand = new SqlCommand("SELECT * FROM [LaPass].[dbo].[Sewage_Information]", mainConnection);
                labPCIInformation.Text = "Wasserwärter - Information";
            }
            else if (_analge == "Crane")
            {
                sqlDatMaintenanceDummy.SelectCommand = new SqlCommand("SELECT * FROM [LaPass].[dbo].[Crane_Information]", mainConnection);
                labPCIInformation.Text = "Sandkran - Information";
            }
            maintenanceBuilder = new SqlCommandBuilder(sqlDatMaintenanceDummy);
            maintenanceBuilder.ConflictOption = ConflictOption.CompareAllSearchableValues;
            sqlDatMaintenance.UpdateCommand = maintenanceBuilder.GetUpdateCommand();
            sqlDatMaintenance.DeleteCommand = maintenanceBuilder.GetDeleteCommand();
            sqlDatMaintenance.InsertCommand = maintenanceBuilder.GetInsertCommand();

            try
            {
                bnsInformation.DataSource = datSetInformation;
                bnsInformation.DataMember = "Information";
            }
            catch
            {
            }

            dgvInformation.DataSource = bnsInformation;
            dgvInformation.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvInformation.ReadOnly = true;
            dgvInformation.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
            dgvInformation.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvInformation.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dgvInformation.Columns["InfoID"].Visible = false;
            dgvInformation.Columns["Completed"].Visible = false;
            dgvInformation.Columns["Date"].Visible = false;
            dgvInformation.Columns["Information"].HeaderText = "Info-Tafel";
            dgvInformation.Columns["Information"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvInformation.Columns["Information"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvInformation.Columns["Information"].FillWeight = 10;


            foreach (DataGridViewColumn c in dgvInformation.Columns)
            {
                c.DefaultCellStyle.Font = new Font("Arial", 14F, GraphicsUnit.Pixel);
            }
        }

        private void FillData()
        {
            datSetInformation.Clear();
            string sqlStatement = "";

            if (_analge == "PCI")
                sqlStatement = "SELECT * FROM [LaPass].[dbo].[PCI_Information] where Completed = 0 order by date asc";
            
            if (_analge == "Sewage")
                sqlStatement = "SELECT * FROM [LaPass].[dbo].[Sewage_Information] where Completed = 0 order by date asc";
            
            if (_analge == "Crane")
                sqlStatement = "SELECT * FROM [LaPass].[dbo].[Crane_Information] where Completed = 0 order by date asc";
            

            SqlCommand sqlCommand = new SqlCommand(sqlStatement, mainConnection);
            //sqlDatMaintenance = new System.Data.SqlClient.SqlDataAdapter();
            sqlDatMaintenance.SelectCommand = sqlCommand;

            try
            {
                sqlDatMaintenance.Fill(datSetInformation, "Information");
            }
            catch
            {
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnNeu_Click(object sender, EventArgs e)
        {
            foreach (Form mdiChilds in this.MdiChildren)
            {
                if (mdiChilds.GetType() == typeof(FNewInfo))
                {
                    mdiChilds.Focus();
                    return;
                }
            }
            fNewInfo = new FNewInfo(this);
            fNewInfo.Show();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            DialogResult result =  MessageBox.Show("Soll die ausgewählte Information wirklich abgeschlossen werden?", "Information", MessageBoxButtons.YesNo);
            if(result == DialogResult.Yes)
            {
                DataRowView currentRow = (DataRowView)bnsInformation.Current;
                currentRow["Completed"] = 1;
                saveChanges();
                FillData();
            }
        }

        public void saveChanges()
        {
            bnsInformation.EndEdit();
            sqlDatMaintenance.Update(datSetInformation, "Information");
            dgvInformation.Refresh();
        }
    }
}
