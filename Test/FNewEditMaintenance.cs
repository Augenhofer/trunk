using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Feuchte_Rapport
{
    public partial class FNewEditMaintenance : Form
    {
        FPCI_Maintenance _fParent;
        bool _newEntry;
        DataRowView _currentRow;

        public FNewEditMaintenance(FPCI_Maintenance fPartent, bool NewEntry, DataRowView CurrentRow)
        {
            InitializeComponent();
            _fParent = fPartent;
            _newEntry = NewEntry;
            _currentRow = CurrentRow;
        }

        public FNewEditMaintenance(FPCI_Maintenance fPartent)
        {
            InitializeComponent();
            _fParent = fPartent;
            _newEntry = true;
        }

        private void FNewEditMaintenance_Load(object sender, EventArgs e)
        {
            dtTiPickerDate.Format = DateTimePickerFormat.Short;

            if (_newEntry == false)
            {
                dtTiPickerDate.Value = Convert.ToDateTime(_currentRow["Date"]);
                cmbBoxTime.Text = _currentRow["Time"].ToString();
                cmbBoxShift.Text = _currentRow["Shift"].ToString();
                txtBoxRemark.Text = _currentRow["Remark"].ToString();
                txtBoxReporter.Text = _currentRow["Reporter"].ToString();
            }
        }

        private void btnChancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
           if (cmbBoxShift.Text.ToString() != "" && cmbBoxTime.Text.ToString() != "" && txtBoxRemark.Text != "" && txtBoxReporter.Text != "")
           {
              if (_newEntry == true)
              {
                    DataRow row = _fParent.datSetRapport.Tables["Maintenance"].NewRow();
                    row["Date"] = dtTiPickerDate.Value.ToString("yyyy-MM-dd");
                    row["Time"] = cmbBoxTime.Text;
                    row["Shift"] = cmbBoxShift.Text;
                    row["Remark"] = txtBoxRemark.Text;
                    row["Reporter"] = txtBoxReporter.Text;
                    _fParent.datSetRapport.Tables["Maintenance"].Rows.Add(row);
              }
              else
              {
                  _currentRow["Date"] = dtTiPickerDate.Value.ToString("yyyy-MM-dd");
                  _currentRow["Time"] = cmbBoxTime.Text;
                  _currentRow["Shift"] = cmbBoxShift.Text;
                  _currentRow["Remark"] = txtBoxRemark.Text;
                  _currentRow["Reporter"] = txtBoxReporter.Text;
              }
              _fParent.saveCahnges();

              this.Close();
           }
           else
           {
               MessageBox.Show("Es müssen alle Eingabefelder befüllt sein.", "Information", MessageBoxButtons.OK);
           }
           
        }
    }
}
