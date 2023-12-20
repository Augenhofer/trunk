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
    public partial class FNewHumidity : Form
    {
        FPCIHumidity _fParent;
        int id;
        DateTime datum;
        DataSet datSetXML;
        string sqlStatementXML;
        decimal value90Min, value90Max, value90, value60Min, value60Max, value60;
        DataRowView _currentRow;
        SqlConnection mainConnection;

        public FNewHumidity(FPCIHumidity fPartent, DataRowView CurrentRow)
        {
            InitializeComponent();
            _currentRow = CurrentRow;
            datum = Convert.ToDateTime(_currentRow["DateTime"]);
            id = (int)_currentRow["ID"];
            datSetXML = new DataSet();
            _fParent = fPartent;
            mainConnection = new SqlConnection();
        }

        private void FNewHumidity_Load(object sender, EventArgs e)
        {
            dtTiPickerDatum.Value = datum;
            Korngroeßen(id);
            txtBoxKorn1.Text = value60.ToString();
            txtBoxKorn2.Text = value90.ToString();
            mainConnection.ConnectionString = Properties.Settings.Default.LaPass_ConnectionString;

        }

        private void Korngroeßen(int ID)
        {
            mainConnection.ConnectionString = Properties.Settings.Default.LaPass_ConnectionString;
            mainConnection.Open();
            //sqlStatementXML = "SELECT CAST(REPLACE(CAST(XMLSigned AS nvarchar(MAX)), 'utf-8', 'utf-16') AS XML) AS XMLSigned FROM [LaPass].[dbo].[Measurements] where ID = " + ID;
            sqlStatementXML = "SELECT CAST(REPLACE(CAST(XML AS nvarchar(MAX)), 'utf-8', 'utf-16') AS XML) AS XMLSigned FROM [LaPass].[dbo].[MeasurementFull] where ID = 125";
            SqlCommand sqlCommand = new SqlCommand(sqlStatementXML, mainConnection);
            string test = sqlCommand.ExecuteScalar().ToString();
            System.IO.StringReader swXML = new System.IO.StringReader(test);
            mainConnection.Close();
            bool old = false;
            try
            {
                datSetXML.ReadXml(swXML);

                foreach (DataRow row in datSetXML.Tables["SizeClasses"].Rows)
                {
                    if (row["SizeClassMax"].ToString() == "82.6")
                    {
                        string val = row["FrequencyCum"].ToString();
                        value90Min = Convert.ToDecimal(val.Replace(".", ","));
                    }

                    if (row["SizeClassMax"].ToString() == "90.8" || row["SizeClassMax"].ToString() == "91.0")
                    {
                        string val = row["FrequencyCum"].ToString();
                        value90Max = Convert.ToDecimal(val.Replace(".", ","));
                        if (row["SizeClassMax"].ToString() == "90.8")
                            old = true;
                    }
                    if (row["SizeClassMax"].ToString() == "56.0" || row["SizeClassMax"].ToString() == "56.2")
                    {
                        string val = row["FrequencyCum"].ToString();
                        value60Min = Convert.ToDecimal(val.Replace(".", ","));
                    }

                    if (row["SizeClassMax"].ToString() == "61.8")
                    {
                        string val = row["FrequencyCum"].ToString();
                        value60Max = Convert.ToDecimal(val.Replace(".", ","));
                    }
                }
                if (old == true)
                {
                    value90 = Math.Round(value90Min + (value90Max - value90Min) / (Convert.ToDecimal(90.8 - 82.6)) * (Convert.ToDecimal(90 - 82.6)), 3);
                    value60 = Math.Round(value60Min + (value60Max - value60Min) / (Convert.ToDecimal(61.8 - 56.0)) * (Convert.ToDecimal(60 - 56.0)), 3);
                }
                else
                {
                    value90 = Math.Round(value90Min + (value90Max - value90Min) / (Convert.ToDecimal(91.0 - 82.6)) * (Convert.ToDecimal(90 - 82.6)), 3);
                    value60 = Math.Round(value60Min + (value60Max - value60Min) / (Convert.ToDecimal(61.8 - 56.2)) * (Convert.ToDecimal(60 - 56.2)), 3);
                }
            }
            catch
            {

            }
            datSetXML.Clear();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtBoxHumidity.Text != "")
            {
                if (Convert.ToDecimal(txtBoxHumidity.Text) < 2 && Convert.ToDouble(txtBoxHumidity.Text) > 0.1)
                {
                    DialogResult result = MessageBox.Show("Wollen Sie die Änderungen speichern?", "Information", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        DataRow row = _fParent.datSetMain.Tables["Humidity"].NewRow();
                        row["ID"] = id;
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
