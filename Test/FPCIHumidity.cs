//using Microsoft.Office.Interop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;

namespace Feuchte_Rapport
{
    public partial class FPCIHumidity : Form
    {

        public DataSet datSetMain;
        BindingSource bsHumidity, bsMessHumidity;
        static int visible;
        static string ab, bis;
        SqlDataAdapter sqlDataAdapter, sqlDataMessHumidity;
        SqlDataAdapter sqlDatHumidity, sqlDatHumidityDummy;
        SqlDataAdapter sqlDatLabor;
        SqlConnection mainConnection;
        MainFormData data;
        FNewHumidity fHumidity;
        SqlCommandBuilder maintenanceBuilder;
        Excel.Workbook excWorkbook;
        int i;
        int count;

        public FPCIHumidity(MainFormData MainData)
        {
            InitializeComponent();
            datSetMain = new DataSet();
            bsHumidity = new BindingSource();
            bsMessHumidity = new BindingSource();
            mainConnection = new SqlConnection();
            data = MainData;
            sqlDataAdapter = new SqlDataAdapter();
            sqlDataMessHumidity = new SqlDataAdapter();
            sqlDatHumidity = new SqlDataAdapter();
            sqlDatHumidityDummy = new SqlDataAdapter();
            sqlDatLabor = new SqlDataAdapter();

        }
        private void FMainForm_Load(object sender, EventArgs e)
        {
            visible = 1;
            mainConnection.ConnectionString = Properties.Settings.Default.LaPass_ConnectionString;
            dtTiPickerAb.Format = DateTimePickerFormat.Short;
            dtTiPickerBis.Format = DateTimePickerFormat.Short;

            dtTiPickerAb.Value = DateTime.Now.Date.AddMonths(-1);
            dtTiPickerBis.Value = DateTime.Now.Date.AddDays(+1);

            ab = dtTiPickerAb.Value.ToString("yyyy-MM-dd");
            bis = dtTiPickerBis.Value.ToString("yyyy-MM-dd");

            sqlDatHumidityDummy.SelectCommand = new SqlCommand("SELECT * FROM [LaPass].[dbo].[Humidity]", mainConnection);
            sqlDatHumidity.SelectCommand = new SqlCommand("SELECT * FROM [LaPass].[dbo].[Humidity]", mainConnection);
            try
            {
               // mainConnection.Open();
                maintenanceBuilder = new SqlCommandBuilder(sqlDatHumidityDummy);
                sqlDatHumidity.InsertCommand = maintenanceBuilder.GetInsertCommand();
                sqlDatHumidity.UpdateCommand = maintenanceBuilder.GetUpdateCommand();
                //mainConnection.Close();
            }
            catch { };

            FillData();
            try
            {
                bsHumidity.DataSource = datSetMain;
                bsHumidity.DataMember = "LaPaSS";

                bsMessHumidity.DataSource = datSetMain;
                bsMessHumidity.DataMember = "MessHumidity";
            }
            catch
            { }

            dgvMain.DataSource = bsHumidity;
            dgvMain.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMain.ReadOnly = true;
            dgvMain.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;

            dgvMain.Columns["Visible"].HeaderText = "sichtbar";
            dgvMain.Columns["Visible"].Visible = false;
            dgvMain.Columns["Principal"].HeaderText = "Benutzer";
            dgvMain.Columns["Principal"].Visible = false;
            dgvMain.Columns["MeasurementNumber"].HeaderText = "Nr.";
            dgvMain.Columns["MeasurementNumber"].Visible = false;
            dgvMain.Columns["Keyword1"].HeaderText = "Name1";
            dgvMain.Columns["Description"].HeaderText = "Beschreibung";
            dgvMain.Columns["DateTime"].HeaderText = "Erzeugt";
            //dgvMain.Columns["Humidity"].HeaderText = "Feuchte [%]";

            foreach (DataGridViewColumn c in dgvMain.Columns)
            {
                c.DefaultCellStyle.Font = new Font("Arial", 14F, GraphicsUnit.Pixel);
                c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                
            }
            dgvMain.Columns["Description"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;


            dgvHumidity.DataSource = bsMessHumidity;
            dgvHumidity.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHumidity.ReadOnly = true;
            dgvHumidity.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
            dgvHumidity.Columns["HumidityID"].Visible = false;
            dgvHumidity.Columns["ID"].Visible = false;
            //dgvHumidity.Columns["DateTime"].Visible = false;
            dgvHumidity.Columns["DateTime1"].Visible = false;
            dgvHumidity.Columns["Korngroeße60"].HeaderText = "Korngröße < 60 µm";
            dgvHumidity.Columns["Korngroeße90"].HeaderText = "Korngröße < 90 µm";
            dgvHumidity.Columns["Principal"].HeaderText = "Benutzer";
            dgvHumidity.Columns["MeasurementNumber"].HeaderText = "Nr.";
            dgvHumidity.Columns["Keyword1"].HeaderText = "Name";
            dgvHumidity.Columns["DateTime"].HeaderText = "Erzeugt";
            dgvHumidity.Columns["Humidity"].HeaderText = "Feuchtewert";
            dgvHumidity.Columns["Transfered"].HeaderText = "Werte übertragen";
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void messungToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.IsMdiContainer = true;
        }

        private void FillData()
        {
            datSetMain.Clear();
            string sqlStatement = "";
            string sqlStatement2 = "";

            if (visible == 1)
            {
                sqlStatement = "SELECT TOP 100 a.[DateTime] , a.[Visible],a.[ID],a.[Principal],a.[MeasurementNumber],a.[Keyword1], b.[Description] " +
                            "FROM [LaPass].[dbo].[Measurements] a inner join " +
                            "[LaPass].[dbo].[vw_MeasurementDesc] b on a.Id = b.id " +
                                  "where a.[DateTime] >= '" + ab + "' and a.[DateTime] <= '" + bis + "'" +
                            "and a.[Visible]  = " + visible + " order by a.[ID] desc";

                sqlStatement2 = "select b.HumidityID, b.DateTime, a.ID, a.[Principal],a.[MeasurementNumber],a.[Keyword1], a.[DateTime],b.Korngroeße60, b.Korngroeße90, b.Humidity, b.Transfered " +
                                "FROM [LaPass].[dbo].[Measurements] a inner join " +
                                    "[LaPass].[dbo].[Humidity] b on a.ID = b.ID " +
                                    "where a.[DateTime] >= '" + ab + "' and a.[DateTime] <= '" + bis + "'" +
                                    "and a.[Visible]  = " + visible + " order by a.[ID] desc";
            }
            else
            {
                sqlStatement = "SELECT TOP 100 a.[DateTime] , a.[Visible],a.[ID],a.[Principal],a.[MeasurementNumber],a.[Keyword1], b.[Description] " +
                            "FROM [LaPass].[dbo].[Measurements] a inner join " +
                            "[LaPass].[dbo].[vw_MeasurementDesc] b on a.Id = b.id " +
                                  "where a.[DateTime] >= '" + ab + "' and a.[DateTime] <= '" + bis + "' order by a.[ID] desc";

                sqlStatement2 = "select b.HumidityID,b.DateTime, a.Id, a.[Principal],a.[MeasurementNumber],a.[Keyword1], a.[DateTime],b.Korngroeße60, b.Korngroeße90, b.Humidity, b.Transfered " +
                            "FROM [LaPass].[dbo].[Measurements] a inner join " +
                                 "[LaPass].[dbo].[Humidity] b on a.ID = b.ID " +
                                 "where a.[DateTime] >= '" + ab + "' and a.[DateTime] <= '" + bis + "'" +
                             " order by a.[ID] desc";
            }

            SqlCommand sqlCommand = new SqlCommand(sqlStatement, mainConnection);
            sqlDataAdapter.SelectCommand = sqlCommand;

            SqlCommand sqlCommand2 = new SqlCommand(sqlStatement2, mainConnection);
            sqlDataMessHumidity.SelectCommand = sqlCommand2;


            try
            {
                sqlDataAdapter.Fill(datSetMain, "LaPaSS");
                sqlDatHumidity.Fill(datSetMain, "Humidity");
                sqlDataMessHumidity.Fill(datSetMain, "MessHumidity");
            }
            catch
            {
            }
        }

        public void dtTiPicker_CloseUp(object sender, EventArgs e)
        {
            ab = dtTiPickerAb.Value.ToString("yyyy-MM-dd");
            bis = dtTiPickerBis.Value.ToString("yyyy-MM-dd");

            FillData();
        }

        private void chkBoxSichtbar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBoxSichtbar.Checked == true)
                visible = 0;
            else
                visible = 1;

            FillData();
        }

        private void dgvMain_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataRowView currentRow = (DataRowView)bsHumidity.Current;

            foreach (Form mdiChilds in this.MdiChildren)
            {
                if (mdiChilds.GetType() == typeof(FNewHumidity))
                {
                    mdiChilds.Focus();
                    return;
                }
            }

            fHumidity = new FNewHumidity(this, currentRow);
            fHumidity.StartPosition = FormStartPosition.CenterParent;
            fHumidity.Show();
        }

        public void insert_Humidity()
        {
            bsHumidity.EndEdit();
            sqlDatHumidity.Update(datSetMain, "Humidity");
            FillData();
            dgvMain.Refresh();
        }

        public bool AddLinieToExcel(int ID, string Datum, string Uhrzeit, decimal Value60, decimal Value90, decimal Humidity)
        //public void AddLinieToExcel(DataRowView currentRow)
        {
            // Arbeitsmappe öffnen
            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = true;

            string excelWBName = Properties.Settings.Default.excelFileName;
            string workbookPath = Properties.Settings.Default.excelPath + @"\" + excelWBName;

            //string workbookPathName = Application.StartupPath + @"\" + excelWBName;
            //string[,] results = new string[33, 2];
            //Excel.Workbook excWorkbook = new Excel.Workbook();
            try
            {
                excWorkbook = excelApp.Workbooks.Open(workbookPath,
                                                        0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "",
                                                        true, false, 0, true, false, false);

                Excel.Worksheet xlWorkSheetFocus = (Excel.Worksheet)excWorkbook.Worksheets.get_Item(excWorkbook.Worksheets.Count);
                xlWorkSheetFocus.Activate();
                //int lastRow = getRowCount(xlWorkSheetFocus);
                int lastRow = xlWorkSheetFocus.UsedRange.Rows.Count+1;

                (xlWorkSheetFocus.Cells[lastRow, 1] as Excel.Range).Value2 = Convert.ToDateTime(Datum);
                (xlWorkSheetFocus.Cells[lastRow, 1] as Excel.Range).NumberFormat = "DD.MM.YYYY";
                (xlWorkSheetFocus.Cells[lastRow, 2] as Excel.Range).Value2 = Convert.ToDateTime(Uhrzeit);
                (xlWorkSheetFocus.Cells[lastRow, 2] as Excel.Range).NumberFormat = "hh:mm";
                (xlWorkSheetFocus.Cells[lastRow, 3] as Excel.Range).Value2 = "Feinkohle";
                (xlWorkSheetFocus.Cells[lastRow, 4] as Excel.Range).Value2 = Value90;
                (xlWorkSheetFocus.Cells[lastRow, 5] as Excel.Range).Value2 = Value60; ;
                (xlWorkSheetFocus.Cells[lastRow, 6] as Excel.Range).Value2 = Humidity;
                (xlWorkSheetFocus.Cells[lastRow, 9] as Excel.Range).Value2 = ID;

                excWorkbook.Save();
                excWorkbook.Close();
            }
            catch 
            {
                MessageBox.Show("Die Daten konnten nicht in das Labor übertragen werden", "Error");
                excelApp.Quit();
                Marshal.ReleaseComObject(excWorkbook);
                Marshal.ReleaseComObject(excelApp);
                return false;
            }
                        
            excelApp.Quit();
            return true;
        }

        private static int getRowCount(Excel.Worksheet sheet)
        {
            int row = 0;
            string cellValue = "";

            for (int i = 0; i < sheet.UsedRange.Rows.Count; i++)
            {
                try
                {
                    cellValue = Convert.ToString((sheet.Cells[i, 1] as Excel.Range).Value);
                }
                catch
                {
                }
                if (cellValue == null)
                    return i;
            }

            return row;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvHumidity_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult result = MessageBox.Show("Wollen Sie die Feuchtewerte ans Labor übertragen?", "Information", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                DataRowView currentRow = (DataRowView)bsMessHumidity.Current;
                int id = (int)currentRow["ID"];
                int humidityid = (int)currentRow["HumidityID"];

                SqlCommand myCmdObject = new SqlCommand("SELECT count(ID) FROM [LaPass].[dbo].[Humidity] where ID = " + id + " and Transfered = 1");
                myCmdObject.Connection = mainConnection;

                try
                {
                    count = (int)myCmdObject.ExecuteScalar();
                }
                catch { }
                if (count != 0)
                {
                    MessageBox.Show("Für diese Messung wurden bereits Daten an das Labor übermittelt!", "Hinweis");
                }
                else
                {
                    for (int j = 1; j < datSetMain.Tables["Humidity"].Rows.Count; j++)
                        if (datSetMain.Tables["Humidity"].Rows[j]["HumidityID"].ToString() == humidityid.ToString())
                            datSetMain.Tables["Humidity"].Rows[j]["Transfered"] = 1;

                    if (AddLinieToExcel((int)currentRow["ID"], currentRow["DateTime1"].ToString(), currentRow["DateTime1"].ToString(), Convert.ToDecimal(currentRow["Korngroeße60"]), Convert.ToDecimal(currentRow["Korngroeße90"]), Convert.ToDecimal(currentRow["Humidity"])) == true)
                    {
                        insert_Humidity();
                        MessageBox.Show("Die Daten wurden ans Labor übertragen", "Error");
                    }
                }

            }
        }
    }
}
