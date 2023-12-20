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
    public partial class FNewHumidityMaScontrol : Form
    {
        FPCIHumidity _fParent;
        DateTime datum;
        DataSet datSetXML, datSetHumidity;
        BindingSource bsHumidity;
        string sqlStatementXML;
        DataRowView _currentRow;
        SqlConnection mainConnection;
        int[] _id = new int[2];
        int[] _meas = new int[2];
        double korn60=0;
        double korn90 = 0;

        public FNewHumidityMaScontrol(FPCIHumidity fPartent, DataRowView CurrentRow, int[] id, int[] meas)
        {
            InitializeComponent();
            _id = id;
            _meas = meas;
            datSetXML = new DataSet();
            datSetHumidity = new DataSet();
            bsHumidity = new BindingSource();

            _fParent = fPartent;
            mainConnection = new SqlConnection();
            _currentRow = CurrentRow;
            datum = Convert.ToDateTime(_currentRow["DateTime"]);
        }

        private void FNewHumidityMaScontrol_Load(object sender, EventArgs e)
        {
            dtTiPickerDatum.Value = datum;
            

            datSetHumidity.Tables.Add("Humidity");
            datSetHumidity.Tables["Humidity"].Columns.Add("ID");
            datSetHumidity.Tables["Humidity"].Columns.Add("DateTime");
            datSetHumidity.Tables["Humidity"].Columns.Add("Korngroeße60");
            datSetHumidity.Tables["Humidity"].Columns.Add("Korngroeße90");
            datSetHumidity.Tables["Humidity"].Columns.Add("MeasurementNumber");
            //datSetHumidity.Tables["Humidity"].Columns.Add("Transfered");

            Korngroeßen(_id);
            CalcAvg();

            bsHumidity.DataSource = datSetHumidity;
            bsHumidity.DataMember = "Humidity";

            dgvKorn.DataSource = bsHumidity;
            dgvKorn.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKorn.ReadOnly = true;
            dgvKorn.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;

            dgvKorn.Columns["ID"].Visible = false;
            dgvKorn.Columns["Korngroeße60"].HeaderText = "Durchgang 60 µm";
            dgvKorn.Columns["Korngroeße90"].HeaderText = "Durchgang 90 µm";
            dgvKorn.Columns["MeasurementNumber"].HeaderText = "Nr.";
            dgvKorn.Columns["MeasurementNumber"].DisplayIndex = 0;            
        }

        private void Korngroeßen(int[] ID)
        {
            mainConnection.ConnectionString = Properties.Settings.Default.LaPass_ConnectionString;
            mainConnection.Open();

            int j =0;
            foreach (int a in ID)
            {
                if(a > 0)
                {
                    sqlStatementXML = "SELECT CAST(REPLACE(CAST(XML AS nvarchar(MAX)), 'utf-8', 'utf-16') AS XML) AS XMLSigned FROM [LaPass].[dbo].[MeasurementFull] where ID = " +a;
                    SqlCommand sqlCommand = new SqlCommand(sqlStatementXML, mainConnection);
                    string test = sqlCommand.ExecuteScalar().ToString();
                    System.IO.StringReader swXML = new System.IO.StringReader(test);
                    
                    try
                    {
                        datSetXML.ReadXml(swXML);

                        DataRow row = datSetHumidity.Tables["Humidity"].NewRow();
                        row["ID"] = a;
                        row["DateTime"] = dtTiPickerDatum.Value.ToString();
                        int rowIndex = datSetXML.Tables["Value"].Rows.IndexOf(datSetXML.Tables["Value"].Select("size = '60'")[0]);

                        row["Korngroeße60"] = Math.Round(Convert.ToDouble(datSetXML.Tables["Value"].Rows[rowIndex]["freqcum"].ToString().Replace(".", ",")), 2);
                        rowIndex = datSetXML.Tables["Value"].Rows.IndexOf(datSetXML.Tables["Value"].Select("size = '90'")[0]);
                        row["Korngroeße90"] = Math.Round(Convert.ToDouble(datSetXML.Tables["Value"].Rows[rowIndex]["freqcum"].ToString().Replace(".", ",")), 2);
                        row["MeasurementNumber"] = _meas[j].ToString();

                        datSetHumidity.Tables["Humidity"].Rows.Add(row);
                        
                    }
                    catch(SyntaxErrorException e)
                    {
                        string error = e.Data.ToString();
                    }
                    datSetXML.Clear();
                }
                j++;
            }
            mainConnection.Close();
            //sqlStatementXML = "SELECT CAST(REPLACE(CAST(XML AS nvarchar(MAX)), 'utf-8', 'utf-16') AS XML) AS XMLSigned FROM [LaPass].[dbo].[MeasurementFull] where ID in (" + _id +" )";
            //SqlCommand sqlCommand = new SqlCommand(sqlStatementXML, mainConnection);
            //string test = sqlCommand.ExecuteScalar().ToString();
            //System.IO.StringReader swXML = new System.IO.StringReader(test);
            //mainConnection.Close();
            //bool old = false;
            //try
            //{
            //    datSetXML.ReadXml(swXML);

            //    foreach (DataRow row in datSetXML.Tables["SizeClasses"].Rows)
            //    {
            //        if (row["SizeClassMax"].ToString() == "82.6")
            //        {
            //            string val = row["FrequencyCum"].ToString();
            //            value90Min = Convert.ToDecimal(val.Replace(".", ","));
            //        }

            //        if (row["SizeClassMax"].ToString() == "90.8" || row["SizeClassMax"].ToString() == "91.0")
            //        {
            //            string val = row["FrequencyCum"].ToString();
            //            value90Max = Convert.ToDecimal(val.Replace(".", ","));
            //            if (row["SizeClassMax"].ToString() == "90.8")
            //                old = true;
            //        }
            //        if (row["SizeClassMax"].ToString() == "56.0" || row["SizeClassMax"].ToString() == "56.2")
            //        {
            //            string val = row["FrequencyCum"].ToString();
            //            value60Min = Convert.ToDecimal(val.Replace(".", ","));
            //        }

            //        if (row["SizeClassMax"].ToString() == "61.8")
            //        {
            //            string val = row["FrequencyCum"].ToString();
            //            value60Max = Convert.ToDecimal(val.Replace(".", ","));
            //        }
            //    }
            //    if (old == true)
            //    {
            //        value90 = Math.Round(value90Min + (value90Max - value90Min) / (Convert.ToDecimal(90.8 - 82.6)) * (Convert.ToDecimal(90 - 82.6)), 3);
            //        value60 = Math.Round(value60Min + (value60Max - value60Min) / (Convert.ToDecimal(61.8 - 56.0)) * (Convert.ToDecimal(60 - 56.0)), 3);
            //    }
            //    else
            //    {
            //        value90 = Math.Round(value90Min + (value90Max - value90Min) / (Convert.ToDecimal(91.0 - 82.6)) * (Convert.ToDecimal(90 - 82.6)), 3);
            //        value60 = Math.Round(value60Min + (value60Max - value60Min) / (Convert.ToDecimal(61.8 - 56.2)) * (Convert.ToDecimal(60 - 56.2)), 3);
            //    }
            //}
            //catch
            //{

            //}
            //datSetXML.Clear();
        }
        private void CalcAvg()
        {

            for (int i = 0; i < datSetHumidity.Tables["Humidity"].Rows.Count;i++ )
            {
                korn60 = korn60 + Convert.ToDouble(datSetHumidity.Tables["Humidity"].Rows[i]["Korngroeße60"].ToString().Replace(".", ","));
                korn90 = korn90 + Convert.ToDouble(datSetHumidity.Tables["Humidity"].Rows[i]["Korngroeße90"].ToString().Replace(".", ","));
            }

            //DataRow row = datSetHumidity.Tables["Humidity"].NewRow();

            txtBoxKorn1.Text = Math.Round(korn60 / datSetHumidity.Tables["Humidity"].Rows.Count, 2).ToString();
            txtBoxKorn2.Text = Math.Round(korn90 / datSetHumidity.Tables["Humidity"].Rows.Count, 2).ToString();

            //datSetHumidity.Tables["Humidity"].Rows.Add(row);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtBoxHumidity.Text != "")
            {
                if (Convert.ToDecimal(txtBoxHumidity.Text) < 3 && Convert.ToDouble(txtBoxHumidity.Text) > 0.1)
                {
                    DialogResult result = MessageBox.Show("Wollen Sie die Änderungen speichern?", "Information", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        DataRow row = _fParent.datSetMain.Tables["Humidity"].NewRow();
                        row["ID"] = _id[0];
                        row["DateTime"] = dtTiPickerDatum.Value.ToString();
                        row["Korngroeße60"] = txtBoxKorn1.Text;
                        row["Korngroeße90"] = txtBoxKorn2.Text;
                        row["Humidity"] = txtBoxHumidity.Text;
                        row["Transfered"] = 0;

                        _fParent.datSetMain.Tables["Humidity"].Rows.Add(row);

                        _fParent.insert_Humidity();

                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Der Wert ist nicht plausibel!");
                    txtBoxHumidity.Text = "";
                    this.ActiveControl = txtBoxHumidity;
                }
            }
            else
            {
                MessageBox.Show("Es muss ein Wert für die Feuchte eingegben werden!", "Inforamtion");
            }
        
        }

    }
}
