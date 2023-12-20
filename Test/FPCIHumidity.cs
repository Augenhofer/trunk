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
        FNewHumidityMaScontrol fHumidityMaScontrol;
        SqlCommandBuilder maintenanceBuilder;
        Excel.Workbook excWorkbook;
        int i;
        int count;
        bool checkLabor = false;

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
            //dgvMain.ReadOnly = true;
            dgvMain.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;

            //****** Neues Messgerät 

            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.Name = "Checked";
            checkColumn.HeaderText = "Auswahl";
            checkColumn.Width = 100;
            //checkColumn.ReadOnly = false;
            dgvMain.Columns.Add(checkColumn);

            dgvMain.Columns["MeasurementNumber"].HeaderText = "Nr.";
            dgvMain.Columns["Checked"].DisplayIndex = 0;
            dgvMain.Columns["Checked"].ReadOnly = false;
            dgvMain.Columns["Description"].HeaderText = "Beschreibung";
            dgvMain.Columns["DateTime"].HeaderText = "Erzeugt";

            //****** Altes Messgerät - LAPASS

            //dgvMain.Columns["Visible"].HeaderText = "sichtbar";
            //dgvMain.Columns["Visible"].Visible = false;
            //dgvMain.Columns["Principal"].HeaderText = "Benutzer";
            //dgvMain.Columns["Principal"].Visible = false;
            //dgvMain.Columns["MeasurementNumber"].HeaderText = "Nr.";
            //dgvMain.Columns["MeasurementNumber"].Visible = false;
            //dgvMain.Columns["Keyword1"].HeaderText = "Name1";
            //dgvMain.Columns["Description"].HeaderText = "Beschreibung";
            //dgvMain.Columns["DateTime"].HeaderText = "Erzeugt";
            //dgvMain.Columns["Humidity"].HeaderText = "Feuchte [%]";

            foreach (DataGridViewColumn c in dgvMain.Columns)
            {
                c.DefaultCellStyle.Font = new Font("Arial", 14F, GraphicsUnit.Pixel);
                c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (c.Name != "Checked")
                    c.ReadOnly = true;
            }
            dgvMain.Columns["Description"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;


            dgvHumidity.DataSource = bsMessHumidity;
            //dgvHumidity.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ////dgvHumidity.ReadOnly = true;
            //dgvHumidity.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;

            DataGridViewCheckBoxColumn checkLaborColumn = new DataGridViewCheckBoxColumn();
            checkLaborColumn.Name = "Checked";
            checkLaborColumn.HeaderText = "Auswahl";
            checkLaborColumn.Width = 100;
            checkLaborColumn.TrueValue = true;
            checkLaborColumn.FalseValue = false;

            dgvHumidity.Columns.Add(checkLaborColumn);
            dgvHumidity.Columns["Checked"].DisplayIndex = 0;
            dgvHumidity.Columns["Checked"].ReadOnly = false;

            dgvHumidity.Columns["HumidityID"].Visible = false;
            dgvHumidity.Columns["ID"].Visible = false;
            dgvHumidity.Columns["Created"].Visible = false;
            dgvHumidity.Columns["Korngroeße60"].HeaderText = "Durchgang 60 µm";
            dgvHumidity.Columns["Korngroeße90"].HeaderText = "Durchgang 90 µm";
            dgvHumidity.Columns["MeasurementNumber"].HeaderText = "Nr.";
            dgvHumidity.Columns["Name"].HeaderText = "Name";
            dgvHumidity.Columns["DateTime"].HeaderText = "Erzeugt";
            dgvHumidity.Columns["Humidity"].HeaderText = "Feuchtewert";
            dgvHumidity.Columns["Transfered"].HeaderText = "Werte übertragen";

            dgvHumidity.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //dgvHumidity.ReadOnly = true;
            dgvHumidity.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
            foreach (DataGridViewColumn c in dgvHumidity.Columns)
            {
                c.DefaultCellStyle.Font = new Font("Arial", 14F, GraphicsUnit.Pixel);
                c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (c.Name != "Checked")
                    c.ReadOnly = true;
            }
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


            sqlStatement = "SELECT TOP 100 a.Created as Datetime , a.[ID], a.Name, a.[MeasurementNumber],a.SOPName ,a.Attribute1 +'/'+a.Attribute2+'/'+a.Attribute3+'/'+a.Attribute4 as Description FROM [MeasurementFull] a where a.Created >= '" + ab + "' and a.Created <= '" + bis + "' order by a.[ID] desc";


            sqlStatement2 = "select b.HumidityID, b.DateTime, a.ID, a.[MeasurementNumber],a.[Name], a.[Created],b.Korngroeße60, b.Korngroeße90, b.Humidity, b.Transfered " +
                            "FROM [LaPass].[dbo].[MeasurementFull] a inner join " +
                                "[LaPass].[dbo].[Humidity] b on a.ID = b.ID " +
                                "where a.[Created] >= '" + ab + "' and a.[Created] <= '" + bis + "'" +
                                " order by b.DateTime desc";

            //****** Altes Messgerät - LAPASS
            
            //if (visible == 1)
            //{
            //    sqlStatement = "SELECT TOP 100 a.[DateTime] , a.[Visible],a.[ID],a.[Principal],a.[MeasurementNumber],a.[Keyword1], b.[Description] " +
            //                "FROM [LaPass].[dbo].[Measurements] a inner join " +
            //                "[LaPass].[dbo].[vw_MeasurementDesc] b on a.Id = b.id " +
            //                      "where a.[DateTime] >= '" + ab + "' and a.[DateTime] <= '" + bis + "'" +
            //                "and a.[Visible]  = " + visible + " order by a.[ID] desc";

            //    sqlStatement2 = "select b.HumidityID, b.DateTime, a.ID, a.[Principal],a.[MeasurementNumber],a.[Keyword1], a.[DateTime],b.Korngroeße60, b.Korngroeße90, b.Humidity, b.Transfered " +
            //                    "FROM [LaPass].[dbo].[Measurements] a inner join " +
            //                        "[LaPass].[dbo].[Humidity] b on a.ID = b.ID " +
            //                        "where a.[DateTime] >= '" + ab + "' and a.[DateTime] <= '" + bis + "'" +
            //                        "and a.[Visible]  = " + visible + " order by a.[ID] desc";
            //}
            //else
            //{
            //    sqlStatement = "SELECT TOP 100 a.[DateTime] , a.[Visible],a.[ID],a.[Principal],a.[MeasurementNumber],a.[Keyword1], b.[Description] " +
            //                "FROM [LaPass].[dbo].[Measurements] a inner join " +
            //                "[LaPass].[dbo].[vw_MeasurementDesc] b on a.Id = b.id " +
            //                      "where a.[DateTime] >= '" + ab + "' and a.[DateTime] <= '" + bis + "' order by a.[ID] desc";
                
            //    sqlStatement2 = "select b.HumidityID,b.DateTime, a.Id, a.[Principal],a.[MeasurementNumber],a.[Keyword1], a.[DateTime],b.Korngroeße60, b.Korngroeße90, b.Humidity, b.Transfered " +
            //                "FROM [LaPass].[dbo].[Measurements] a inner join " +
            //                     "[LaPass].[dbo].[Humidity] b on a.ID = b.ID " +
            //                     "where a.[DateTime] >= '" + ab + "' and a.[DateTime] <= '" + bis + "'" +
            //                 " order by a.[ID] desc";
            //}

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

        //private void dgvMain_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
           // int anzahl = 0;
           //// string id = "";
           // string test = "";
           // int[] id = new int[2];
           // int[] meas = new int[2]; 

           // for (int i = 0; i < dgvMain.RowCount - 1;i++ )
           // {
           //     if (Convert.ToBoolean(dgvMain.Rows[i].Cells["Checked"].Value) == true)
           //     {
           //         anzahl++;
           //         if (anzahl < 3)
           //         {
           //             id[anzahl-1] = Convert.ToInt32(dgvMain.Rows[i].Cells["ID"].Value);
           //             meas[anzahl-1] = Convert.ToInt32(dgvMain.Rows[i].Cells["MeasurementNumber"].Value);
           //         }
           //     }
           // }


           // if (anzahl > 0 && anzahl < 3)
           // {
           //     DataRowView currentRow = (DataRowView)bsHumidity.Current;

           //     foreach (Form mdiChilds in this.MdiChildren)
           //     {
           //         if (mdiChilds.GetType() == typeof(FNewHumidityMaScontrol))
           //         {
           //             mdiChilds.Focus();
           //             return;
           //         }
           //     }

           //     fHumidityMaScontrol = new FNewHumidityMaScontrol(this, currentRow, id, meas);
           //     fHumidityMaScontrol.StartPosition = FormStartPosition.CenterParent;
           //     fHumidityMaScontrol.Show();
           // }
           // else
           // {
           //     if (anzahl == 0)
           //         MessageBox.Show("Es muss mindestens eine Messunng ausgewählt sein!");
           //     else
           //         MessageBox.Show("Es dürfen nicht mehr als 2 Messungen ausgewählt sein!");
           // }

            //DataRowView currentRow = (DataRowView)bsHumidity.Current;

            //foreach (Form mdiChilds in this.MdiChildren)
            //{
            //    if (mdiChilds.GetType() == typeof(FNewHumidity))
            //    {
            //        mdiChilds.Focus();
            //        return;
            //    }
            //}

            //fHumidity = new FNewHumidity(this, currentRow);
            //fHumidity.StartPosition = FormStartPosition.CenterParent;
            //fHumidity.Show();
        //}

        public void insert_Humidity()
        {
            bsHumidity.EndEdit();
            sqlDatHumidity.Update(datSetMain, "Humidity");
            FillData();
            dgvMain.Refresh();
        }


        private void dgvHumidity_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dgvHumidity["Checked", e.RowIndex];
                if (Convert.ToBoolean(chk.Value) == false)
                {
                    if (Convert.ToBoolean(dgvHumidity["Transfered", e.RowIndex].Value) != true)
                        chk.Value = chk.TrueValue;
                    else
                    {
                        MessageBox.Show("Diese Messung wurde bereits ans Labor übertragen!", "Information");
                        chk.Value = chk.FalseValue;
                        dgvHumidity["Checked", e.RowIndex].Value = false;
                        dgvHumidity.EndEdit();
                    }
                }
                if (Convert.ToBoolean(chk.Value) == true)
                    chk.Value = chk.FalseValue;
            }
        }

        private void btnLabor_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Wollen Sie die Feuchtewerte ans Labor übertragen?", "Information", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                foreach (DataGridViewRow _row in dgvHumidity.Rows)
                {
                    if (Convert.ToBoolean(_row.Cells["Checked"].Value) == true)
                    {
                        int id = (int)_row.Cells["ID"].Value;
                        int humidityid = (int)_row.Cells["HumidityID"].Value;

                        for (int j = 1; j < datSetMain.Tables["Humidity"].Rows.Count; j++)
                            if (datSetMain.Tables["Humidity"].Rows[j]["HumidityID"].ToString() == humidityid.ToString())
                                datSetMain.Tables["Humidity"].Rows[j]["Transfered"] = 1;

                        //if (AddLinieToExcel((int)currentRow["ID"], currentRow["DateTime"].ToString(), currentRow["DateTime"].ToString(), Convert.ToDecimal(currentRow["Korngroeße60"]), Convert.ToDecimal(currentRow["Korngroeße90"]), Convert.ToDecimal(currentRow["Humidity"])) == true)
                        if (AddLinieToExcel((int)_row.Cells["ID"].Value, _row.Cells["DateTime"].Value.ToString(), _row.Cells["DateTime"].Value.ToString(), Convert.ToDecimal(_row.Cells["Korngroeße60"].Value), Convert.ToDecimal(_row.Cells["Korngroeße90"].Value), Convert.ToDecimal(_row.Cells["Humidity"].Value)) == true)
                        {
                            insert_Humidity();
                            MessageBox.Show("Die Daten wurden ans Labor übertragen", "Error");
                        }
                    }
                }
            }
        }
    
        private void button1_Click(object sender, EventArgs e)
        {
            int anzahl = 0;
            int[] id = new int[2];
            int[] meas = new int[2];

            for (int i = 0; i < dgvMain.RowCount - 1; i++)
            {
                if (Convert.ToBoolean(dgvMain.Rows[i].Cells["Checked"].Value) == true)
                {
                    anzahl++;
                    if (anzahl < 3)
                    {
                        id[anzahl - 1] = Convert.ToInt32(dgvMain.Rows[i].Cells["ID"].Value);
                        meas[anzahl - 1] = Convert.ToInt32(dgvMain.Rows[i].Cells["MeasurementNumber"].Value);
                    }
                }
            }

            if (anzahl > 0 && anzahl < 3)
            {
                DataRowView currentRow = (DataRowView)bsHumidity.Current;

                foreach (Form mdiChilds in this.MdiChildren)
                {
                    if (mdiChilds.GetType() == typeof(FNewHumidityMaScontrol))
                    {
                        mdiChilds.Focus();
                        return;
                    }
                }

                fHumidityMaScontrol = new FNewHumidityMaScontrol(this, currentRow, id, meas);
                fHumidityMaScontrol.StartPosition = FormStartPosition.CenterParent;
                fHumidityMaScontrol.Show();
            }
            else
            {
                if (anzahl == 0)
                    MessageBox.Show("Es muss mindestens eine Messunng ausgewählt sein!");
                else
                    MessageBox.Show("Es dürfen nicht mehr als 2 Messungen ausgewählt sein!");
            }
        }

        public bool AddLinieToExcel(int ID, string Datum, string Uhrzeit, decimal Value60, decimal Value90, decimal Humidity)
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

        //private void dgvHumidity_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
            //DialogResult result = MessageBox.Show("Wollen Sie die Feuchtewerte ans Labor übertragen?", "Information", MessageBoxButtons.YesNo);
            //if (result == DialogResult.Yes)
            //{
            //    DataRowView currentRow = (DataRowView)bsMessHumidity.Current;
            //    int id = (int)currentRow["ID"];
            //    int humidityid = (int)currentRow["HumidityID"];

            //    SqlCommand myCmdObject = new SqlCommand("SELECT count(ID) FROM [LaPass].[dbo].[Humidity] where ID = " + id + " and Transfered = 1");
            //    myCmdObject.Connection = mainConnection;

            //    try
            //    {
            //        count = (int)myCmdObject.ExecuteScalar();
            //    }
            //    catch { }
            //    if (count != 0)
            //    {
            //        MessageBox.Show("Für diese Messung wurden bereits Daten an das Labor übermittelt!", "Hinweis");
            //    }
            //    else
            //    {
            //        for (int j = 1; j < datSetMain.Tables["Humidity"].Rows.Count; j++)
            //            if (datSetMain.Tables["Humidity"].Rows[j]["HumidityID"].ToString() == humidityid.ToString())
            //                datSetMain.Tables["Humidity"].Rows[j]["Transfered"] = 1;

            //        if (AddLinieToExcel((int)currentRow["ID"], currentRow["DateTime"].ToString(), currentRow["DateTime"].ToString(), Convert.ToDecimal(currentRow["Korngroeße60"]), Convert.ToDecimal(currentRow["Korngroeße90"]), Convert.ToDecimal(currentRow["Humidity"])) == true)
            //        {
            //            insert_Humidity();
            //            MessageBox.Show("Die Daten wurden ans Labor übertragen", "Error");
            //        }
            //    }

            //}
        //}
    }
}
