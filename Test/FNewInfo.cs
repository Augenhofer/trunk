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
    public partial class FNewInfo : Form
    {
        FPCIInfo _fParent;
 
        public FNewInfo(FPCIInfo fParent)
        {
            InitializeComponent();
            _fParent = fParent;
        }

        private void FNewInfo_Load(object sender, EventArgs e)
        {
            txtBoxInfo.ReadOnly = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtBoxInfo.Text != "")
            {
                DataRow row = _fParent.datSetInformation.Tables["Information"].NewRow();
                row["Date"] = DateTime.Now.Date;
                row["Information"] = txtBoxInfo.Text;
                row["Completed"] = 0;
                _fParent.datSetInformation.Tables["Information"].Rows.Add(row);

                _fParent.saveChanges();

                this.Close();
            }
            else
                MessageBox.Show("Es muss ein Text eingegben werden", "Infromation", MessageBoxButtons.OK);
        }
    }
}
